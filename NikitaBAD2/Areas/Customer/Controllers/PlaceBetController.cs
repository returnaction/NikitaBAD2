using Microsoft.AspNetCore.Mvc;
using NikitaBAD2.Models.LineBets;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PlaceBetController : Controller
    {
        public IActionResult Play()
        {
            PlaceBet placeBet = GeneratePlaceBet();
            return View(placeBet);
        }

        [HttpPost]
        public IActionResult Play(int userAnswer, int rolledNumber, int bet)
        {
            PlaceBet placeBet = new();

            switch (rolledNumber)
            {
                case 6:
                case 8:
                    if (userAnswer == CaluclateCorrectAnswerFor6or8(bet))
                        return RedirectToAction(nameof(Play));
                    else
                    {
                        placeBet.RolledNumber = rolledNumber;
                        placeBet.Bet = bet;
                        placeBet.ErrorMessage = "Wrong Payout!";
                    }
                    break;
            }

            return View(placeBet);
        }

        private PlaceBet GeneratePlaceBet()
        {
            PlaceBet placeBet = new();
            placeBet.RolledNumber = RollDice();

            switch (placeBet.RolledNumber)
            {
                case 6:
                case 8:
                    placeBet.Bet = GenerateRandomBetFor6or8();
                    break;
            }

            return placeBet;
        }

        private int GenerateRandomBetFor6or8()
        {
            Random random = new();
            int bet = random.Next(1, 101) * 6;
            return bet;
        }

        private int CaluclateCorrectAnswerFor6or8(int bet)
        {
            return bet + (bet / 6);
        }

        private int RollDice()
        {
            Random random = new();
            int[] outcome = { 6, 8 };
            return outcome[random.Next(0, outcome.Length)];
        }
    }
}
