using System;
using System.ComponentModel.DataAnnotations;

public class Putovanje{
    [Key]
    public int ID { get; set; }
    [Required] 
    public Destinacija Destinacija { get; set; }
    [Required] 
    public Putnik Putnik { get; set; }
    [Required] 
    public string tipSedista { get; set; }
    [Required] 
    public int Cena { get; set; }
    [Required] 
    public DateTime Vreme{ get; set;}
}