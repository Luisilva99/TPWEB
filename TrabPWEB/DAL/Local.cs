using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabPWEB.Validation;

namespace TrabPWEB.DAL
{
    public class Local
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int LocalId { get; set; }

        [Required]
        [Display(Name = "Localidade")]
        [StringLength(150, ErrorMessage = "Tamanho máximo de 150 caracteres.")]
        //[Remote("LocalValidation", "Local", AdditionalFields = "LocalName", ErrorMessage = "Remote: O nome da localidade já existe.")]
        public string LocalName { get; set; }
        [Display(Name = "Região")]
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}