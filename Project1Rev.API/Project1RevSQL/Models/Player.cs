using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Project1RevSQL.Models;

public class Player
{
    //Fields
    [Key]
    [Column("PlayerId")]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    [Required]
    [MaxLength(50)]
    public double WinLoss { get; set; } = 0;
    [Required]
    [MaxLength(50)]
    public int TotalRounds { get; set; } = 0;
    public string City { get; set; }

    public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    
    
}