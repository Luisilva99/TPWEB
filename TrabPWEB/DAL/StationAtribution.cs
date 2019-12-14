using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class StationAtribution
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StationAtributionId { get; set; }


        [Required]
        [Display(Name = "Id do User")]
        public string UserId { get; set; }


        [Required]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }
    }
}