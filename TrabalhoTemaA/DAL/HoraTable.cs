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
    
    public partial class HoraTable
    {
        public int IdHora { get; set; }
        public System.DateTime HoraInicio { get; set; }
        public System.DateTime HoraFinal { get; set; }
        public Nullable<int> EstadoHora { get; set; }
        public int IdPosto { get; set; }
    
        public virtual PostoTable PostoTable { get; set; }
    }
}
