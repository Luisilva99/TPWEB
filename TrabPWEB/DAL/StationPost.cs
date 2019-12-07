using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class StationPost
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StationPostId { get; set; }

        [Required]
        [Display(Name = "Nome do Posto")]
        public string StationPostName { get; set; }

        [Required]
        [Display(Name = "Tipo de Carregamento")]
        public int RechargeTypeId { get; set; }

        [ForeignKey("RechargeTypeId")]
        public virtual RechargeType RechargeType { get; set; }

        ////Adicionado automáticamente ao editar uma estação de carregamento
        //[Display(Name = "Estação")]
        //public int StationId { get; set; }

        //[ForeignKey("StationId")]
        //public virtual Station Station { get; set; }
        ////-----------------------------------------------------------------

    }
}