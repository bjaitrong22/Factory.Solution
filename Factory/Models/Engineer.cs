using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "A first name is required!")]
    public string FirstName { get; set;}
    [Required(ErrorMessage = "A last name is required!")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "A hire date is required!")]
    public DateOnly HireDate { get; set;}
    public List<EngineerMachine> JoinEntities { get; }
  }
}