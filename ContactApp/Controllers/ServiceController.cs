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

        [Route("supprimer-service/{id?}")]
        public IActionResult DeleteService(int id)
        {
            Service service = Service.GetService(id);
            if (service != null)
            {
                service.DeleteService();
            }
            return RedirectToAction("Index", "Service", new { message = "Service supprimé avec succés" });
        }


        // Page d'ajout d'un service 
        public IActionResult FormService()
        {
            return View("form");
        }

        // Ajout du service
        public IActionResult SubmitForm(Service service)
        {
            if(!service.CountSite())
            {
                if (service.AddService())
                {
                    return RedirectToAction("Index", "Service", new { message = "Service ajouté" });
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
            if(!service.CountSiteUPD())
            {
                if (service.UpdateSite())
                {
                    return RedirectToAction("Index", new { message = "Service modifié" });
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
