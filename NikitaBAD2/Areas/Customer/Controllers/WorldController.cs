using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikitaBAD2.Data;
using NikitaBAD2.Models;
using NikitaBAD2.Models.Enums;
using NikitaBAD2.Models.PropBets;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class WorldController : Controller
    {
        private const EGames currentGame = EGames.WorldBet;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public WorldController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Play()
        {
            PropBet worldBet = GenerateWorlBet();
            return View(worldBet);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Play(PropBet propBet)
        {

            var identityUser = await _userManager.GetUserAsync(HttpContext.User);

            PlayerGames? worldBetGame = await _context.PlayerGames.FirstOrDefaultAsync(g => g.UserId == identityUser!.Id && g.GameType == currentGame);

            if(worldBetGame is null)
            {
                worldBetGame = new PlayerGames { GameType = currentGame, UserId = identityUser!.Id, TempBestResult = 0, LongestCorrectAsnwerStreak = 0, TotalAnswers = 0 };

                await _context.PlayerGames.AddAsync(worldBetGame);
                await _context.SaveChangesAsync();
            }

            if(propBet.Answer == CalculateCorrectAnswer(propBet.Bet, propBet.RolledNumber))
            {
                worldBetGame.TempBestResult++;
                worldBetGame.TotalAnswers++;
                if(worldBetGame.TempBestResult > worldBetGame.LongestCorrectAsnwerStreak)
                {
                    worldBetGame.LongestCorrectAsnwerStreak = worldBetGame.TempBestResult;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Play));
            }
            else
            {
                propBet.ErrorMessage = "Wrong Payout!";
                worldBetGame.TempBestResult = 0;
                worldBetGame.TotalAnswers++;
                await _context.SaveChangesAsync();
                return View(propBet);
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
