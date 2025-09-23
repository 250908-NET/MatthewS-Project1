using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Project1Rev.Models;

public class Tournament
{
    [Key]
    [Column("TournamentId")]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public int SizeLimit { get; set; }
    [Required]
    [MaxLength(50)]
    public string Address { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    [Required]
    [MaxLength(50)]
    public string RuleType { get; set; }
    [Required]
    [MaxLength(50)]
    public string Rounds { get; set; }
    [Required]
    [MaxLength(50)]
    public string TcgName { get; set; }

    public List<Player> Players { get; set; } = new();
}