using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabPWEB.DAL;

namespace TrabPWEB.ViewModels
{
    public class StationsIndexViewModel
    {
        public string Local { get; set; }

        public string Procura { get; set; }

        public IPagedList<Station> Stations { get; set; }

        public StationsIndexViewModel()
        {

        }

        public void OrdenarEstacoes(IQueryable<Station> station, int pag, int nreg)
        {
            Stations = station.OrderBy(p => p.Local.RegionId).ToPagedList(pag, nreg);
            return;
        }
    }
}