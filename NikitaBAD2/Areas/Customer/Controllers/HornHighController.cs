﻿using Microsoft.AspNetCore.Mvc;
using NikitaBAD2.Models.PropBets;
using NuGet.Protocol;

namespace NikitaBAD2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HornHighController : Controller
    {
        public PropBet hornHigh;
        public IActionResult Play()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Play()
        {
            
        }

        private int CalculateCorrectAnse(int bet, int placedbet, int rolledNumber)
        {
            int result = 0;

            switch (placedbet)
            {
                case 2:
                    switch (rolledNumber)
                    {
                        case 2:
                            result = (bet * 11) + bet / 5 * 2;
                            break;
                        case 12:
                            result = (bet * 5) + bet / 5;
                            break;
                        case 3:
                        case 11:
                            result = (bet * 2) + bet / 5;
                            break;
                    }
                    break;
                case 12:
                    switch (rolledNumber)
                    {
                        case 2:
                            result = (bet * 5) + bet / 5;
                            break;
                        case 12:
                            result = (bet * 11) + bet / 5 * 2;
                            break;
                        case 3:
                        case 11:
                            result = (bet * 2) + bet / 5;
                            break;
                    }
                    break;
                case 3:
                    switch (rolledNumber)
                    {
                        case 2:
                        case 12:
                            result = (bet * 5) + bet / 5;
                            break;
                        case 3:
                            result = (bet * 5) + bet / 5 * 2;
                            break;
                        case 11:
                            result = (bet * 2) + bet / 5;
                            break;
                    }
                    break;
                case 11:
                    switch (rolledNumber)
                    {
                        case 2:
                        case 12:
                            result = (bet * 5) + bet / 5;
                            break;
                        case 3:
                            result = (bet * 2) + bet / 5;
                            break;
                        case 11:
                            result = (bet * 5) + bet / 5 * 2;
                            break;
                    }
                    break;
            }

            return result;
        }

        private int RollDice()
        {
            Random random = new Random();
            int[] outcome = { 2, 3, 11, 12 };
            return outcome[random.Next(0, outcome.Length)];
        }

        private int GenerateRandomBet()
        {
            Random random = new();
            return random.Next(1, 41) * 5;
        }

        private int GenerateRandomPlace()
        {
            Random random = new Random();
            int[] betPlaced = { 2, 3, 11, 12 };
            return betPlaced[random.Next(0, betPlaced.Length)];
        }
    }
}
