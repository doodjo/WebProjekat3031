using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Destinacija{
    [Key]
    public int ID { get; set; }
    [Required] 
    public string Grad { get; set; }
    [Required] 
    [JsonIgnore]
    public List<Termin> Termini { get; set;}
    [Required] 
    [JsonIgnore]
    public List<Putovanje> Putovanja { get; set;}
    [Required] 
    [JsonIgnore]
    public Stanica Stanica { get; set; }
}