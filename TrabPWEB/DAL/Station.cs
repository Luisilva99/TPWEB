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
        [Display(Name = "Estação de Carregamento")]
        [StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres.")]
        public string StationName { get; set; }

        [Display(Name = "Localidade")]
        public int LocalId { get; set; }

        [ForeignKey("LocalId")]
        public virtual Local Local { get; set; }

        //Horário de funcionamento
        [Required(ErrorMessage = "Obrigatório introduzir as horas de início de funcionamento.")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Obrigatório introduzir as horas de início de funcionamento.")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public DateTime Finnish { get; set; }
        //------------------------

    }
}