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
    
    public partial class Tabellino
    {
        public int ID { get; set; }
        public int IDPartita { get; set; }
        public int Reti { get; set; }
        public int Assist { get; set; }
        public int Penalita { get; set; }
        public System.DateTime UpdTMS { get; set; }
        public int RetiSubite { get; set; }
        public int IDRosa { get; set; }
    
        public virtual Partita Partita { get; set; }
        public virtual Rosa Rosa { get; set; }
    }
}
