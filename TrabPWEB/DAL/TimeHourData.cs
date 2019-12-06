using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class TimeHourData
    {
        private static int TimeHourDataId = 0;

        [Display(Name = "HoraId")]
        public int TimeHourId { get; set; }
        
        [DataType(DataType.Time)]
        [Display(Name = "Hora")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Time { get; set; }
        
        [Display(Name = "Disponibilidade")]
        public Boolean Status { get; set; }

        public TimeHourData(DateTime time, bool status)
        {
            Time = time;
            Status = status;
            TimeHourId = TimeHourDataId++;
        }

        public Boolean getStatus()
        {
            return Status;
        }
        public DateTime getTime()
        {
            return Time;
        }
    }
}