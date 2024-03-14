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
        private const int MinBet = 4;
        private const int MaxBet = 200;
        public IActionResult Index()
        {
            var games = new CrapsGame();
            return View(games);
        }

        [HttpPost]
        public IActionResult Play(int userAnswer)
        {
            var game = new CrapsGame();


            game.RolledNumber = RollDice();
            game.Bet = GeneretateRandomBet();

            if(userAnswer == CalculateCorrectAnswer(game.Bet, game.RolledNumber))
            {
                game.CorrectAnswer = userAnswer;

            }
            else
            {
                game.ErrorMessage = "Incorrect answer. Try again!";
            }

            return View("Index", game);

        }


        private int CalculateCorrectAnswer(int bet, int rolledNumber)
        {
            if (rolledNumber == 3 || rolledNumber == 11)
            {
                return bet * 3;
            }
            else if (rolledNumber == 2 || rolledNumber == 12)
            {
                return (bet * 7) - (bet / 4);
            }
            else
            {
                return 0;
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
