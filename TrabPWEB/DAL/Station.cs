using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class Station
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StationId { get; set; }
        [Required]
        [Display(Name = "Estão de Carregamento")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres.")]
        public string StationName { get; set; }
        [Display(Name = "Região")]
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}