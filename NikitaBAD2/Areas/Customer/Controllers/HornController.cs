using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikitaBAD2.Data;
using NikitaBAD2.Migrations;
using NikitaBAD2.Models.PropBets;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HornController : Controller
    {
        public IActionResult Play()
        {
            PropBet hornBet = GenerateNewHornBet();
            return View(hornBet);
        }


        // Change later to get a model instead of 3 different numbers
        [HttpPost]
        public IActionResult Play(PropBet propBet)
        {
            // if answer is correct 
            if(propBet.Answer == CalculateCorrectAnswer(propBet.Bet, propBet.RolledNumber))
            {
                return RedirectToAction(nameof(Play));
            }
            // if answer is wrong
            else
            {
                propBet.ErrorMessage = "Wrong Payout!";
                return View(propBet);
            }

        }

        private PropBet GenerateNewHornBet()
        {
            PropBet hornBet = new();
            hornBet.Bet = GeneratateRandomBet();
            hornBet.RolledNumber = RollDice();

            return hornBet;
        }

        private int CalculateCorrectAnswer(int bet, int rolledNumber)
        {
            if (rolledNumber == 3 || rolledNumber == 11)
            {
                return bet * 3;
            }
            else
            {
                return (bet * 7) - (bet / 4);
            }
        }


        private int RollDice()
        {
            Random radnom = new Random();
            int[] outcome = { 2, 3, 11, 12 };
            return outcome[radnom.Next(0, outcome.Length)];
        }

        private int GeneratateRandomBet()
        {
            Random random = new Random();

            return random.Next(1, 51) * 4;
        }
        
    }

    
}
