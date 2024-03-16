namespace NikitaBAD2.Models.LineBets
{
    public class PlaceBet
    {
        public int Bet { get; set; }
        public int? Cap { get; set; } // might not going to use it let's see
        public int RolledNumber { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public int CorrectAnswer { get; set; }


    }
}
