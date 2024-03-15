using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikitaBAD2.Data;
using NikitaBAD2.Migrations;
using NikitaBAD2.Models;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HornController : Controller
    {
        public HornBet hornBet;

        public IActionResult Play()
        {
            hornBet = GenerateNewHornBet();
            return View(hornBet);
        }


        // Change later to get a model instead of 3 different numbers
        [HttpPost]
        public IActionResult Play(int userAnswer, int bet, int rolledNumber)
        {
            // if answer is correct 
            if(userAnswer == CalculateCorrectAnswer(bet, rolledNumber))
            {
                return RedirectToAction(nameof(Play));
            }
            // if answer is wrong
            else
            {
                hornBet = new HornBet();
                hornBet.Bet = bet;
                hornBet.RolledNumber = rolledNumber;
                hornBet.ErrorMessage = "Wrong payout";
                return View(hornBet);
            }

        }

        private HornBet GenerateNewHornBet()
        {
            HornBet hornBet = new();
            hornBet.Bet = GeneretateRandomBet();
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

        private int GeneretateRandomBet()
        {
            Random random = new Random();

            return random.Next(1, 51) * 4;
        }
        
    }

    
}
