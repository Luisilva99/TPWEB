namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstacaoTable")]
    public partial class EstacaoTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstacaoTable()
        {
            PostoTable = new HashSet<PostoTable>();
        }

        [Key]
        public int IdEstacao { get; set; }

        [StringLength(30)]
        public string NomeEstacao { get; set; }

        public int IdRede { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostoTable> PostoTable { get; set; }
    }
}
