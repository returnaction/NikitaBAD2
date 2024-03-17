﻿namespace NikitaBAD2.Models.PropBets
{
    public class PropBet
    {
        public int Bet { get; set; }
        public int RolledNumber { get; set; }
        public int? PlacedBet { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public int Answer { get; set; }
        
    }
}
