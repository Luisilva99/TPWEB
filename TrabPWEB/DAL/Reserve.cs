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
        [Display(Name = "Estação de Carregamento")]
        public int StationPostId { get; set; }

        [Required]
        [Display(Name = "Local")]
        public int LocalId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horário da reserva")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime date { get; set; }

        [Display(Name = "Estado da reserva")]
        public Boolean status { get; set; }
    }
}