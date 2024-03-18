using NikitaBAD2.Models.Enums;
using System.ComponentModel.DataAnnotations;
using NikitaBAD2.Models.ApplicationUser;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NikitaBAD2.Models
{
    public class PlayerGames
    {
        [Key]
        public int Id { get; set; }

        public EGames GameType { get; set; }

        public int LongestCorrectAsnwerStreak { get; set; }

        public int TempBestResult { get; set; } = 0;

        public int TotalAnswers { get; set; }

        public string UserId { get; set; } = null!;

        [ForeignKey("UserGameId")]
        public IdentityUser ApplicationUser { get; set; }

     
    }
}
