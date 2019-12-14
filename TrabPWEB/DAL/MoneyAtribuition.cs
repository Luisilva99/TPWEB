using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class MoneyAtribuition
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MoneyAtribuitionId { get; set; }


        [Required]
        [Display(Name = "Id do Utilizador")]
        public string UserId { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C#######.##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Saldo")]
        public decimal Cash { get; set; }
    }
}