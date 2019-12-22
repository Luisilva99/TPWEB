using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class Reserve
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ReserveId { get; set; }


        [Required]
        [Display(Name = "Id do User")]
        public string UserId { get; set; }


        [Required]
        [Display(Name = "Posto de Carregamento")]
        public int StationPostId { get; set; }

        [ForeignKey("StationPostId")]
        public virtual StationPost StationPost { get; set; }


        [Required]
        [Display(Name = "Tipo de Tomada")]
        public int RechargeModId { get; set; }

        [ForeignKey("RechargeModId")]
        public virtual RechargeMod RechargeMod { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data da reserva")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime Date { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço da reserva")]
        [DisplayFormat(DataFormatString = "{0:#######.##}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }


        //0 - Canceled; 1 - onGoing; 2 - Completed;
        [Display(Name = "Estado da reserva")]
        [RegularExpression("[0-2]{1}$", ErrorMessage = "Não é um estado de reserva plausivel.")]
        public int Completed { get; set; }

    }
}