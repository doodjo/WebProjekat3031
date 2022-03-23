using System;
using System.ComponentModel.DataAnnotations;

public class Termin{
    [Key]
    public int ID { get; set; }
    [Required]
    public DateTime Vreme { get; set; }
    [Required]
    public Destinacija Destinacija { get; set; }
    
}