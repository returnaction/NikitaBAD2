using Microsoft.AspNetCore.Mvc;
using NikitaBAD2.Models.PropBets;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class WorldController : Controller
    {
        public PropBet worldBet;
        public IActionResult Play()
        {
            worldBet = GenerateWorlBet();
            return View(worldBet);
        }

        [HttpPost]
        public IActionResult Play(int userAnswer, int rolledNumber, int bet)
        {
            if(userAnswer == CalculateCorrectAnswer(bet, rolledNumber))
            {
                return RedirectToAction(nameof(Play));
            }
            else
            {
                worldBet = new PropBet();
                worldBet.Bet = bet;
                worldBet.RolledNumber = rolledNumber;
                worldBet.ErrorMessage = "Wrong Payout!";

                return View(worldBet);
            }
        }

        private int CalculateCorrectAnswer(int bet, int rolledNumber)
        {
            int result = 0;

            switch (rolledNumber)
            {
                case 3:
                case 11:
                    result = (bet * 2) + bet / 5;
                    break;

                case 2:
                case 12:
                    result = (bet * 2) + bet / 5;
                    break;
                case 7:
                    result = 0;
                    break;

            }

            return result;
        }

        private PropBet GenerateWorlBet()
        {
            PropBet worldBet = new();
            worldBet.Bet = GenerateRandomBet();
            worldBet.RolledNumber = RollDice();

            return worldBet;
        }

        private int RollDice()
        {
            Random random = new Random();
            int[] outcome = { 2, 3, 11, 12, 7 };
            return outcome[random.Next(0, outcome.Length)];
        }

        private int GenerateRandomBet()
        {
            Random random = new();
            return random.Next(1, 41) * 5;
        }

        
    }
}
