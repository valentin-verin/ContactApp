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
        public IActionResult DisplaySite(int id, string message)
        {
            ViewBag.Message = message;
            return View("site", Site.GetSite(id));
        }

        [Route("update-site/{id?}")]
        public IActionResult DisplaySiteUPD(int id, string message)
        {
            ViewBag.Message = message;
            return View("siteUPD", Site.GetSite(id));
        }

        [Route("supprimer-site/{id?}")]
        public IActionResult DeleteSite(int id)
        {
            Site site = Site.GetSite(id);
            if (!site.CountSiteDEL())
            {
                if (site != null)
                {
                    site.DeleteSite();
                }
                return RedirectToAction("Index", "Site");
            }
            else
            {
                return RedirectToAction("Index", "Site", new { message = "Site occupé par un.e ou plusieurs employé.e.s" });
            }
        }


        // Page d'ajout d'un site 
        public IActionResult FormSite(string message)
        {
            ViewBag.Message = message;
            return View("form");
        }

        // Ajout du site
        public IActionResult SubmitForm(Site site)
        {
            if(!site.CountSite())
            {
                if (site.AddSite())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("FormSite", new { message = "Site déjà existant" });
                }
            }
            else
            {
                return RedirectToAction("FormSite", new { message = "Site déjà existant" });
            }
        }

        // Modification du site
        public IActionResult SubmitUpdateForm(Site site)
        {
            if(!site.CountSiteUPD())
            {
                if (site.UpdateSite())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("DisplaySiteUPD", new { message = "Une erreur s'est produite, veuillez réessayer" });
                }
            }
            else
            {
                return RedirectToAction("DisplaySiteUPD", new { message = "Site déjà existant" });
            }
        }

    }
}
