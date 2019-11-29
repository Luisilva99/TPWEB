namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostoTable")]
    public partial class PostoTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PostoTable()
        {
            HoraTable = new HashSet<HoraTable>();
            ReservaTable = new HashSet<ReservaTable>();
        }

        [Key]
        public int IdPosto { get; set; }

        public float PrecoPosto { get; set; }

        public int IdEstacao { get; set; }

        public virtual EstacaoTable EstacaoTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoraTable> HoraTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaTable> ReservaTable { get; set; }
    }
}
