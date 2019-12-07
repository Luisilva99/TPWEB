using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class TimeAtribuition
    {
        public int StationPostId { get; set; }

        [ForeignKey("StationPostId")]
        public virtual StationPost StationPost { get; set; }


        public int TimeDataId { get; set; }

        [ForeignKey("TimeDataId")]
        public virtual TimeData TimeData { get; set; }

    }
}