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

        //Horário de funcionamento
        [Required(ErrorMessage = "Obrigatório introduzir as horas iniciais de funcionamento.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de abertura")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Obrigatório introduzir as horas finais de funcionamento.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de fecho")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Finnish { get; set; }
        //------------------------

    }
}