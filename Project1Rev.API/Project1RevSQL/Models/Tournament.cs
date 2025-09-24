using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Project1RevSQL.Models;

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
    public string Storename { get; set; }
    [Required]
    [MaxLength(50)]
    public string Address { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    [Required]
    [MaxLength(50)]
    public string RuleType { get; set; }
    public string RoundType { get; set; }
    [Required]
    [MaxLength(50)]
    public string TcgName { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
}