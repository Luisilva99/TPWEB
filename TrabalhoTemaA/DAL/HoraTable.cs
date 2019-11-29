namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoraTable")]
    public partial class HoraTable
    {
        [Key]
        public int IdHora { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime HoraInicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime HoraFinal { get; set; }

        public bool EstadoHora { get; set; }

        public int IdPosto { get; set; }

        public virtual PostoTable PostoTable { get; set; }
    }
}
