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
    
    public partial class PartitePerSquadra
    {
        public int ID { get; set; }
        public string Squadra { get; set; }
        public System.DateTime Data { get; set; }
        public Nullable<int> RetiFatte { get; set; }
        public Nullable<int> RetiSubite { get; set; }
        public int Rigori { get; set; }
        public string Risultato { get; set; }
        public Nullable<int> Punti { get; set; }
        public int Stagione { get; set; }
        public int SottoStagione { get; set; }
    }
}
