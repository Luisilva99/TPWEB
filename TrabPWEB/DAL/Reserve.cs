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
        public string UserId { get; set; }


        [Required]
        [Display(Name = "Posto de Carregamento")]
        public int StationPostId { get; set; }

        [ForeignKey("StationPostId")]
        public virtual StationPost StationPost { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Data da reserva")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Date { get; set; }


        //0 - Canceled; 1 - onGoing; 2 - Completed;
        [Display(Name = "Estado da reserva")]
        [RegularExpression("[0-2]{1}$", ErrorMessage = "Não é um estado de reserva plausivel.")]
        public int Completed { get; set; }

    }
}