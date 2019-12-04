using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class TimeHour
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int TimeHourId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Tempo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Disponível")]
        public Boolean Status { get; set; }
        
    }
}