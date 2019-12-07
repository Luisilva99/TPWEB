using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class RechargeMod
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int RechargeModId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Descrição do modo de carregamento não pode ultrapassar 30 caracteres.")]
        [Display(Name = "Modo de Carregamento")]
        public string RechargeModName { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Descrição do modo de carregamento não pode ultrapassar 1000 caracteres.")]
        public string RechargeModDescription { get; set; }

    }
}