﻿namespace NikitaBAD2.Models.PropBets
{
    public class ComeBet
    {
        public int FlatBet { get; set; }
        public int Odds { get; set; }
        public int RolledNumber { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public int CorrectAnswer { get; set; }

    }
}