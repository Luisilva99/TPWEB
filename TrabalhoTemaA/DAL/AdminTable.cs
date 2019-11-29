namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTable")]
    public partial class AdminTable
    {
        [Key]
        public int IdAdmin { get; set; }

        [Required]
        [StringLength(30)]
        public string UserNameAdmin { get; set; }

        [Required]
        [StringLength(30)]
        public string PasswordAdmin { get; set; }

        [Required]
        [StringLength(10)]
        public string PasswordConfirmation { get; set; }
    }
}
