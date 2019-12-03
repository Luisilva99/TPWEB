using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int RegionId { get; set; }
        [Required]
        [Display(Name = "Região")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres.")]
        public string RegionName { get; set; }
    }
}