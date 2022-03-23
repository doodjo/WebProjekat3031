using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Putnik{
    [Key]
    public int ID{get;set;}
    [Required] 
    public string eMail { get; set; }
    [Required] 
    public string Ime { get; set; }
    [Required] 
    public string Prezime { get; set; }
    [Required]
    [JsonIgnore]
    public List<Putovanje>  Putovanja { get; set; }
}