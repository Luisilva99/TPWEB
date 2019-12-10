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
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int TimeAtribuitionId { get; set; }


        [Required]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int TimeDataId { get; set; }

        [ForeignKey("TimeDataId")]
        public virtual TimeData TimeData { get; set; }


        [Required]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int StationPostId { get; set; }

        [ForeignKey("StationPostId")]
        public virtual StationPost StationPost { get; set; }

    }
}