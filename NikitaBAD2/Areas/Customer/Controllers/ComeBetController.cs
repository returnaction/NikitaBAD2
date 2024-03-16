using Microsoft.AspNetCore.Mvc;
using NikitaBAD2.Models.PropBets;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    public class ComeBetController : Controller
    {
        public IActionResult Index()
        {
            ComeBet comeBet = GenerateComeBet();
            return View(comeBet);
        }

        private ComeBet GenerateComeBet()
        {
            int[] bet = new int[2];
            ComeBet comeBet = new();
            comeBet.RolledNumber = RollDice();

            switch (comeBet.RolledNumber)
            {
                
                case 4:
                case 10:
                    bet = GenerateRandomBetFor4or10();
                    comeBet.FlatBet = bet[0];
                    comeBet.Odds = bet[1];
                    break;
                case 5:
                case 9:
                    bet = GenerateRandomBetFor5or9();
                    comeBet.FlatBet = bet[0];
                    comeBet.Odds = bet[1];
                    break;
                case 6:
                case 8:
                    bet = GenerateRandomBetFor6or8();
                    comeBet.FlatBet = bet[0];
                    comeBet.Odds = bet[1];
                    break;

            }

            return comeBet;
        }

        private int RollDice()
        {
            Random random = new();
            int[] outcome = { 4, 5, 6, 8, 9, 10 };
            return outcome[random.Next(0, outcome.Length)];
        }

        // The array will have 2 numbers: 1st for flatBet and 2nd for Odds
        private int[] GenerateRandomBetFor4or10()
        {
            Random random = new Random();

            int flatBet = random.Next(1, 100) * 5;
            int odds = random.Next(1, (flatBet * 3 / 5) + 1) * 5;

            return [flatBet, odds];
        }

        private int CaluclateCorrectAnswerFor4or10(int flatBet, int odds)
        {
            return odds * 2 + flatBet;
        }

        private int[] GenerateRandomBetFor5or9()
        {
            Random random = new Random();

            int flatBet = random.Next(1, 100) * 5;
            int odds = random.Next(1, (flatBet * 4 / 10) + 1) * 10;

            return [flatBet, odds];
        }

        private int CaluclateCorrectAnswerFor5or9(int flatBet, int odds)
        {
            return odds + (odds / 2) + flatBet;
        }

        private int[] GenerateRandomBetFor6or8()
        {
            Random random = new Random();
            int flatBet = random.Next(1, 100) * 5;
            int odds = random.Next(1, (flatBet * 5 / 5) + 1) * 5;

            return [flatBet, odds];
        }

        private int CaluclateCorrectAnswerFor6or10(int flatBet, int odds)
        {
            return (odds + (odds / 5)) + flatBet;
        }

    }
}
