//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrabalhoTemaA.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstacaoTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstacaoTable()
        {
            this.RedeTable = new HashSet<RedeTable>();
        }
    
        public int IdEstacao { get; set; }
        public string NomeEstacao { get; set; }
        public int IdPosto { get; set; }
        public int IdReserva { get; set; }
    
        public virtual PostoTable PostoTable { get; set; }
        public virtual ReservaTable ReservaTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RedeTable> RedeTable { get; set; }
    }
}
