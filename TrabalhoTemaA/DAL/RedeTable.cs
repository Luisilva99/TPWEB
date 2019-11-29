namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RedeTable")]
    public partial class RedeTable
    {
        [Key]
        public int IdClienteRede { get; set; }

        [Required]
        [StringLength(30)]
        public string NomeRede { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        public int IdRede { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImagemRede { get; set; }

        [Required]
        [StringLength(30)]
        public string PasswordClienteRede { get; set; }
    }
}
