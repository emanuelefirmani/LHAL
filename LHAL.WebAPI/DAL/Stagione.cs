//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LHAL.WebAPI.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stagione
    {
        public Stagione()
        {
            this.ClassificaManuale = new HashSet<ClassificaManuale>();
            this.GironeStagione = new HashSet<GironeStagione>();
            this.Partita = new HashSet<Partita>();
            this.Rosa = new HashSet<Rosa>();
        }
    
        public int ID { get; set; }
        public string Testo { get; set; }
        public int Ordine { get; set; }
    
        public virtual ICollection<ClassificaManuale> ClassificaManuale { get; set; }
        public virtual ICollection<GironeStagione> GironeStagione { get; set; }
        public virtual ICollection<Partita> Partita { get; set; }
        public virtual ICollection<Rosa> Rosa { get; set; }
    }
}