using Newtonsoft.Json;
using StatisticTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabPWEB.DAL;
using TrabPWEB.Models;

namespace TrabPWEB.Controllers
{
    public class StatisticsController : Controller
    {
        private Dictionary<string, List<StatisticTimeData>> OwnerPoints;
        private Dictionary<string, List<StatisticMoney>> OwnerMoneyPoints;
        private List<string> userOwnersKeys;

        [Authorize]
        // GET: Statistics
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            foreach (var item in db.Reserves)
            {

                DateTime date = item.Date;

                if (date.AddHours(1.0) < DateTime.Now && item.Completed == 1)
                {
                    //Completação da reserva
                    item.Completed = 2;
                    //-----------------------

                    //Reatribuição do horário livre ao Posto de Carregamento
                    TimeAtribuition time = db.TimeAtribuitions.Where(o => o.TimeData.Time.Hour == item.Date.Hour && o.StationPostId == item.StationPostId).Single();

                    TimeData timeData = db.TimeDatas.Where(o => o.Status == true && o.Time.Hour == time.TimeData.Time.Hour).Single();

                    db.TimeAtribuitions.Remove(time);

                    TimeAtribuition ta = new TimeAtribuition()
                    {
                        TimeData = timeData,
                        TimeDataId = timeData.TimeDataId,
                        StationPost = item.StationPost,
                        StationPostId = item.StationPost.StationPostId
                    };

                    db.TimeAtribuitions.Add(ta);
                    //------------------------------------------------------

                }
            }

            db.SaveChanges();

            Dictionary<string, int> timeCount = new Dictionary<string, int>();
            Dictionary<string, decimal> moneyCount = new Dictionary<string, decimal>();

            List<StatisticTimeData> dataPoints = new List<StatisticTimeData>();
            List<StatisticMoney> dataMoneyPoints = new List<StatisticMoney>();

            List<string> times = new List<string>();

            if (User.IsInRole("Client"))
            {
                //Impedimento de o owner ver as reservas dos clientes da base de dados
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You don't have authorization to go to webpage.");
                //--------------------------------------------------------------------
            }
            else if (User.IsInRole("Owner"))
            {
                string userId = db.Users.Where(o => o.UserName.Equals(User.Identity.Name)).Select(o => o.Id).Single();

                List<Reserve> reservasOwner = new List<Reserve>();
                List<int> reservasnumbers = new List<int>();
                List<int> postsnumbers = new List<int>();

                foreach (var item in db.StationAtributions)
                {
                    if (item.UserId.Equals(userId))
                    {
                        reservasnumbers.Add(item.StationId);
                    }
                }

                for (int i = 0; i < reservasnumbers.Count(); i++)
                {
                    int val = reservasnumbers[i];
                    var guardar = db.StationPostsAtribuition.Where(o => o.StationId == val).Select(o => o.StationPostId).ToList();
                    postsnumbers.Add(guardar.ToList()[0]);
                }

                int j = 0;

                for (int i = 0; i < db.Reserves.Count() && j < postsnumbers.Count(); i++)
                {
                    List<Reserve> all = db.Reserves.ToList();
                    if(all[i].StationPostId == postsnumbers[j])
                    {
                        reservasOwner.Add(all[i]);
                        j++;
                    }
                }

                foreach (var item in reservasOwner)
                {
                    if (!times.Contains(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed)))
                    {
                        times.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed));
                        int ccc = db.Reserves.Where(o => o.Date.Month == item.Date.Month && o.Date.Year == item.Date.Year && o.Completed == item.Completed).Count();
                        decimal totalmoney = db.Reserves.Where(o => o.Date.Month == item.Date.Month && o.Date.Year == item.Date.Year && o.Completed == item.Completed).Select(o => o.Price).Sum();
                        timeCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), ccc);
                        if (item.Completed == 0)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney / 2);
                        }
                        else if(item.Completed == 1)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney);
                        }
                        else if(item.Completed == 2)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in db.Reserves)
                {
                    if (!times.Contains(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed)))
                    {
                        times.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed));
                        int ccc = db.Reserves.Where(o => o.Date.Month == item.Date.Month && o.Date.Year == item.Date.Year && o.Completed == item.Completed).Count();
                        decimal totalmoney = db.Reserves.Where(o => o.Date.Month == item.Date.Month && o.Date.Year == item.Date.Year && o.Completed == item.Completed).Select(o => o.Price).Sum();
                        timeCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), ccc);
                        if (item.Completed == 0)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney / 2);
                        }
                        else if (item.Completed == 1)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney);
                        }
                        else if (item.Completed == 2)
                        {
                            moneyCount.Add(item.Date.Month + "/" + item.Date.Year + " Status: " + getDefinition(item.Completed), totalmoney);
                        }
                    }
                }

                foreach (string item in times)
                {
                    dataPoints.Add(new StatisticTimeData(item, timeCount[item], item));
                    dataMoneyPoints.Add(new StatisticMoney(item.Count(), moneyCount[item], item));
                }

                ViewBag.DataTimeReservePoints = JsonConvert.SerializeObject(dataPoints);
                ViewBag.DataMoneyPoints = JsonConvert.SerializeObject(dataMoneyPoints);

                //Tentativa de obter informação sobre cada Owner

                times = new List<string>();

                OwnerPoints = new Dictionary<string, List<StatisticTimeData>>();
                OwnerMoneyPoints = new Dictionary<string, List<StatisticMoney>>();
                userOwnersKeys = new List<string>();

                foreach (var item in db.Users)
                {

                    string nomeRole = db.Roles.Where(o => o.Name.Equals("Owner")).Select(o => o.Id).Single();

                    if (item.Roles.Select(o => o.RoleId).Contains(nomeRole))
                    {
                        string userId = item.Id;

                        OwnerPoints.Add(item.UserName, new List<StatisticTimeData>());
                        OwnerMoneyPoints.Add(item.UserName, new List<StatisticMoney>());
                        userOwnersKeys.Add(item.UserName);

                        List<Reserve> reservasOwner = new List<Reserve>();
                        List<int> reservasnumbers = new List<int>();
                        List<int> postsnumbers = new List<int>();

                        foreach (var items in db.StationAtributions)
                        {
                            if (items.UserId.Equals(userId))
                            {
                                reservasnumbers.Add(items.StationId);
                            }
                        }

                        for (int i = 0; i < reservasnumbers.Count(); i++)
                        {
                            int val = reservasnumbers[i];
                            var guardar = db.StationPostsAtribuition.Where(o => o.StationId == val).Select(o => o.StationPostId).ToList();
                            postsnumbers.Add(guardar.ToList()[0]);
                        }

                        int j = 0;

                        for (int i = 0; i < db.Reserves.Count() && j < postsnumbers.Count(); i++)
                        {
                            List<Reserve> all = db.Reserves.ToList();
                            if (all[i].StationPostId == postsnumbers[j])
                            {
                                reservasOwner.Add(all[i]);
                                j++;
                            }
                        }

                        foreach (var items in reservasOwner)
                        {
                            if (!times.Contains(items.Date.Month + "/" + items.Date.Year + " Status: " + getDefinition(items.Completed)))
                            {
                                times.Add(items.Date.Month + "/" + items.Date.Year + " Status: " + getDefinition(items.Completed));
                                int ccc = db.Reserves.Where(o => o.Date.Month == items.Date.Month && o.Date.Year == items.Date.Year && o.Completed == items.Completed).Count();
                                decimal totalmoney = db.Reserves.Where(o => o.Date.Month == items.Date.Month && o.Date.Year == items.Date.Year && o.Completed == items.Completed).Select(o => o.Price).Sum();

                                string keysss = items.Date.Month + "/" + items.Date.Year + " Status: " + getDefinition(items.Completed);

                                OwnerPoints[item.UserName].Add(new StatisticTimeData(keysss, ccc, keysss));

                                if (items.Completed == 0)
                                {
                                    OwnerMoneyPoints[item.UserName].Add(new StatisticMoney(keysss.Count() + (int) items.Date.Ticks - 1, totalmoney / 2, keysss));
                                }
                                else if(items.Completed == 1)
                                {
                                    OwnerMoneyPoints[item.UserName].Add(new StatisticMoney(keysss.Count() + (int)items.Date.Ticks, totalmoney, keysss));
                                }
                                else if(items.Completed == 2)
                                {
                                    OwnerMoneyPoints[item.UserName].Add(new StatisticMoney(keysss.Count() + (int)items.Date.Ticks + 1, totalmoney, keysss));
                                }

                            }

                        }

                    }

                }

                ViewBag.AllOwnersNames = userOwnersKeys;
                ViewBag.AllOwnerSize = userOwnersKeys.Count();

                return View();
                //----------------------------------------------
            }


            foreach (string item in times)
            {
                dataPoints.Add(new StatisticTimeData(item, timeCount[item], item));
                dataMoneyPoints.Add(new StatisticMoney(item.Count(), moneyCount[item], item));
            }

            ViewBag.DataTimeReservePoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.DataMoneyPoints = JsonConvert.SerializeObject(dataMoneyPoints);

            return View();
        }

        private string getDefinition(int completed)
        {
            if (completed == 0)
            {
                return "Cancelada";
            }
            else if (completed == 1)
            {
                return "Em Reserva";
            }
            return "Finalizada ";
        }

        public string getMoneyUser(string username)
        {
            return JsonConvert.SerializeObject(OwnerMoneyPoints[username]);
        }

        public string getTimeUser(string username)
        {
            return JsonConvert.SerializeObject(OwnerPoints[username]);
        }

        public List<String> getListUser()
        {
            return userOwnersKeys;
        }
    }

    

}