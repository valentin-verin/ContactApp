using System;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class SiteController : Controller
    {
        public SiteController()
        {
        }


        // Affichage de la liste des sites

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(Site.GetSites());
        }


        // Affichage d'un site

        [Route("afficher-site/{id?}")]
        public IActionResult DisplaySite(int id)
        {
            return View("site", Site.GetSite(id));
        }

        [Route("supprimer-site/{id?}")]
        public IActionResult DeleteSite(int id)
        {
            Site site = Site.GetSite(id);
            if (site != null)
            {
                site.DeleteSite();
            }
            return RedirectToAction("Index", "Site", new { message = "Site Supprimé avec succés" });
        }


        // Page d'ajout d'un site 
        public IActionResult FormSite()
        {
            return View("form");
        }

        // Ajout du site
        public IActionResult SubmitForm(Site site)
        {
            if(!site.CountSite())
            {
                if (site.AddSite())
                {
                    return RedirectToAction("Index", new { message = "Site ajouté" });
                }
                else
                {
                    return View("form");
                }
            }
            else
            {
                return View("Index", new { message = "Site déjà existant" });
            }
        }

        // Modification du site
        public IActionResult SubmitUpdateForm(Site site)
        {
            if(!site.CountSiteUPD())
            {
                if (site.UpdateSite())
                {
                    return RedirectToAction("Index", new { message = "Site modifié" });
                }
                else
                {
                    return View("site");
                }
            }
            else
            {
                return View("site", new { message = "Site déjà existant" });
            }
        }

    }
}
