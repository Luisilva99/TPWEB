namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservaTable")]
    public partial class ReservaTable
    {
        [Key]
        public int IdReserva { get; set; }

        public int IdCliente { get; set; }

        public int IdPosto { get; set; }

        public float PrecoFinal { get; set; }

        public int EstadoReserva { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime DataReservaInicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime DataReservaFinal { get; set; }

        public virtual ClienteTable ClienteTable { get; set; }

        public virtual PostoTable PostoTable { get; set; }
    }
}
