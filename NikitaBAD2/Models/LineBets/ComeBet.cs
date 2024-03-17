namespace NikitaBAD2.Models.LineBets
{
    public class ComeBet
    {
        public int FlatBet { get; set; }
        public int Odds { get; set; }
        public int RolledNumber { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public int Answer { get; set; }

    }
}
