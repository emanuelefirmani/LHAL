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
    
    public partial class Tabellini
    {
        public int IDTabellino { get; set; }
        public int IDGiocatore { get; set; }
        public int IDRosa { get; set; }
        public int IDSquadra { get; set; }
        public string NomeSquadra { get; set; }
        public int IDPartita { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Assist { get; set; }
        public int Reti { get; set; }
        public int Penalita { get; set; }
        public int RetiSubite { get; set; }
        public int Stagione { get; set; }
        public int SottoStagione { get; set; }
        public string Ruolo { get; set; }
    }
}