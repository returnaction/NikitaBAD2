using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikitaBAD2.Data;
using NikitaBAD2.Migrations;
using NikitaBAD2.Models;
using NikitaBAD2.Models.Enums;
using NikitaBAD2.Models.PropBets;
using System.Security.Claims;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    
    public class HornController : Controller
    {
        private const EGames currentGame = EGames.HornBet;
        

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HornController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Play()
        {
            PropBet hornBet = GenerateNewHornBet();
            return View(hornBet);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Play(PropBet propBet)
        {
            // check latter if there is null in here
            var identityUser = await _userManager.GetUserAsync(HttpContext.User);

            PlayerGames? hornGame = _context.PlayerGames.FirstOrDefault(g => g.UserId == identityUser.Id && g.GameType == currentGame);


            // if game is null it means we need to create a new game with empty count
            if(hornGame is null)
            {
                hornGame = new PlayerGames { GameType = currentGame, UserId = identityUser!.Id, BestResult = 0 , TempBestResult = 0};

                _context.PlayerGames.Add(hornGame);
                _context.SaveChanges();
            }
            // if not null we are going to user this game
            


            // if answer is correct 
            if (propBet.Answer == CalculateCorrectAnswer(propBet.Bet, propBet.RolledNumber))
            {
                //if(hornGame.BestResult)
                hornGame.TempBestResult++;
                if(hornGame.TempBestResult > hornGame.BestResult)
                {
                    hornGame.BestResult = hornGame.TempBestResult;
                    _context.SaveChanges();
                }
                
                return RedirectToAction(nameof(Play));
            }
            // if answer is wrong
            else
            {
                propBet.ErrorMessage = "Wrong Payout!";
                hornGame.TempBestResult = 0;
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
