using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    [Required(ErrorMessage = "A machine name is required!")]
    public string MachineName { get; set; }
    [Required(ErrorMessage = "An installation date is required!")]
    public DateOnly? InstallationDate { get; set; }
    [Required(ErrorMessage = "A description of the machine is required!")]
    public string Description { get; set; }
    public List<EngineerMachine> JoinEntities { get; }
  }
}