using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class MachinesController: Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Machines.ToList());
    }

    public ActionResult Create()
    {
    
      string messageFromAssignAMachine = TempData["NoMachineMessage"] as string;

      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      if (!ModelState.IsValid)
      {
        return View(machine);
      }
      else
      {
        _db.Machines.Add(machine);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines.Include(machine => machine.JoinEntities).ThenInclude(join => join.Engineer).FirstOrDefault(machine => machine.MachineId == id);

      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);

      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      if (!ModelState.IsValid)
      {
        return View(machine);
      }
      else
      {
        _db.Machines.Update(machine);
        _db.SaveChanges();

        return RedirectToAction("Index");
      } 
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult AssignAnEngineer(int id)
    {
      
      List<Engineer> model = _db.Engineers.ToList();
        
        if(model.Count == 0)
        {
          TempData["NoEngineerMessage"]="There are no engineers in the system to assign. Please add an engineer !";
        
          return RedirectToAction("Create", "Engineers");
        }
        else
        {
          Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);

          IQueryable<SelectListItem> DbEngineer = _db.Engineers.Select(engineer => new SelectListItem
                {
                    Value = engineer.EngineerId.ToString(),
                    Text = engineer.FirstName +" " + engineer.LastName
                });

          ViewBag.EngineerID = new SelectList(DbEngineer, "Value", "Text");
  
          // ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId","FirstName");

          return View(thisMachine);
        }
    }

    [HttpPost]
    public ActionResult AssignAnEngineer(Machine machine, int engineerId)
    {
      #nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault( join => (join.EngineerId == engineerId && join.MachineId == machine.MachineId));
      #nullable disable

      if (joinEntity == null && engineerId != 0)
        {
          _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
          _db.SaveChanges();
        }

      return RedirectToAction("Details", new { id = machine.MachineId});
    }
  }
}