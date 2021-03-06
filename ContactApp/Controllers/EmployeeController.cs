using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactApp.Controllers
{
    public class EmployeeController : Controller
    {
        // Affichage de la liste des employés

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(Employee.GetEmployees());
        }


        // Affichage d'un employé

        [Route("afficher-employee/{id?}")]
        public IActionResult DisplayEmployee(int id)
        {
            return View("employeeInfo", Employee.GetEmployee(id));
        }

        [Route("modifier-employee/{id?}")]
        public IActionResult UpdateEmployee(int id)
        {
            return View("employee", Employee.GetEmployee(id));
        }

        [Route("supprimer-employee/{id?}")]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = Employee.GetEmployee(id);
            if (employee != null)
            {
                employee.DeleteEmployee();
            }
            return RedirectToAction("Index", "Employee", new { message = "Employé supprimé avec succés" });
        }


        // Page d'ajout d'un employé 
        public IActionResult FormEmployee()
        {
            Service.GetServices();
            Site.GetSites();
            return View("form");
        }

        // Ajout du employé
        public IActionResult SubmitForm(Employee employee)
        {
            if(!employee.CountEmployee())
            {
                if (employee.AddEmployee())
                {
                    return RedirectToAction("Index", "Employee", new { message = "Employé ajouté" });
                }
                else
                {
                    return View("form");
                }
            }
            else
            {
                return View("form");
            }
        }

        public IActionResult SubmitFormSearch(string name)
        {
            if (Employee.GetEmployeesSearch(name).Count > 0)
            {
                return View("Index", Employee.GetEmployeesSearch(name)); ;
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
            


        // Update du service
        public IActionResult SubmitUpdateForm(Employee employee)
        {
            if (!employee.CountEmployeeUPD())
            {
                if (employee.UpdateEmployee())
                {
                    return RedirectToAction("Index", new { message = "Employé modifié" });
                }
                else
                {
                    return RedirectToAction("SubmitUpdateForm", new { message = "Une erreur s'est produite, veuillez réessayer" });
                }
            }
            else
            {
                return RedirectToAction("DisplayEmployee", new { message = "Employé déjà existant" });
            }
        }
    }
}
