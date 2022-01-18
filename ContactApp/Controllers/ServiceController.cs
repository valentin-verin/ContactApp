using System;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class ServiceController : Controller
    {
        public ServiceController()
        {
        }


        // Affichage de la liste des services

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(Service.GetServices());
        }


        // Affichage d'un service

        [Route("afficher-service/{id?}")]
        public IActionResult DisplayService(int id)
        {
            return View("service", Service.GetService(id));
        }

        [Route("update-service/{id?}")]
        public IActionResult DisplayServiceUPD(int id)
        {
            return View("serviceUPD", Service.GetService(id));
        }

        [Route("supprimer-service/{id?}")]
        public IActionResult DeleteService(int id)
        {
            Service service = Service.GetService(id);
            if(!service.CountServiceDEL())
            {
                if (service != null)
                {
                    service.DeleteService();
                }
                return RedirectToAction("Index", "Service");
            }
            else
            {
                return RedirectToAction("Index", "Service", new { message = "Service occupé par un.e ou plusieurs employé.e.s" });
            }
        }


        // Page d'ajout d'un service 
        public IActionResult FormService()
        {
            return View("form");
        }

        // Ajout du service
        public IActionResult SubmitForm(Service service)
        {
            if(!service.CountService())
            {
                if (service.AddService())
                {
                    return RedirectToAction("Index", "Service");
                }
                else
                {
                    return View("form");
                }
            }
            else
            {
                return View("form", new { message = "Service déjà existant" });
            }
        }

        // Update du service
        public IActionResult SubmitUpdateForm(Service service)
        {
            if(!service.CountServiceUPD())
            {
                if (service.UpdateService())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("service");
                }
            }
            else
            {
                return View("service", new { message = "Service déjà existant" });
            }
        }
    }
}
