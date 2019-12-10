using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabPWEB.DAL
{
    public class StationPostsAtribuition
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int StationPostsAtribuitionId { get; set; }


        [Required]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int StationPostId { get; set; }

        [ForeignKey("StationPostId")]
        public virtual StationPost StationPost { get; set; }


        [Required]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }

    }
}