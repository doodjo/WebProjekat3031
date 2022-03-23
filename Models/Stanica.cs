using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Stanica{
    [Key]
    public int ID { get; set; }
    [Required] 
    public string Ime { get; set; }
    [Required]
    [JsonIgnore] 
    public List<Destinacija> Destinacije {get;set;} 
}