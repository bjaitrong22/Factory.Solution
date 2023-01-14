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
    [DataType(DataType.Date, ErrorMessage ="A hire date is required!")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
    public DateTime? HireDate { get; set;}
    public List<EngineerMachine> JoinEntities { get; }
  }
}