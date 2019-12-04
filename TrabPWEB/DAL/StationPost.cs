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

        //Adicionado automáticamente ao editar uma estação de carregamento
        [Required]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }
        //-----------------------------------------------------------------

        [ForeignKey("TimeHourId")]
        public virtual ICollection<TimeHour> TimeHour { get; set; }

    }
}