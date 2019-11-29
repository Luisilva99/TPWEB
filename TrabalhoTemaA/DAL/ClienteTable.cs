namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteTable")]
    public partial class ClienteTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClienteTable()
        {
            ReservaTable = new HashSet<ReservaTable>();
        }

        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeCliente { get; set; }

        public int ContaBancaria { get; set; }

        [Required]
        [StringLength(30)]
        public string PasswordCliente { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImagemCliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaTable> ReservaTable { get; set; }
    }
}
