using BusinessComponents;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.ServiceUColaborateur;

namespace UserInterface.Controllers
{
    public class ColaborateurController : Controller
    {
        private UDemandeVisaBusniss visa = new UDemandeVisaBusniss();
        private ServiceUColaborateur.UColaborateurClient colabo = new ServiceUColaborateur.UColaborateurClient();
        // GET: Colaborateur
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["ConnectionDate"] != null && Session["Role"].Equals("Colaborateur"))
            {
                Colaborateur colaborateur = colabo.getColaborateur((int?) Session["ID"]);
                return RedirectToAction("DemandeVisa");
            }
            else if (Session["ConnectionDate"] == null)
            {
                return RedirectToAction("ChangePassword", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Deplacement()
        {
            IList<Deplacement> deplacementData = colabo.afficherDeplacement();
            IList<Deplacement> deplacement = (from d in deplacementData where d.Id.Equals(colabo.getColaborateur((int?)Session["ID"]).IdD) select d).ToList();
            if (Session["ID"] != null && Session["ConnectionDate"] != null && Session["Role"].Equals("Colaborateur"))
            {
                ViewBag.message = deplacement.Count==0 ? "message" : null;
                return View(deplacement);
            }
            else if (Session["ConnectionDate"] == null)
            {
                return RedirectToAction("ChangePassword", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
        // GET: Colaborateur/Details/5
        public ActionResult DemandeVisa()
        {
            IList<DemandeVisa> visaData = visa.showVisaRequest();
            DemandeVisa demandeVisa = (from v in visaData where v.IdC.Equals(Session["ID"]) select v).FirstOrDefault();
            if (Session["ID"] != null && Session["ConnectionDate"] != null && Session["Role"].Equals("Colaborateur"))
            {
                ViewBag.message = demandeVisa==null ? "message" : null;
                return View(demandeVisa);
            }
            else if (Session["ConnectionDate"] == null)
            {
                return RedirectToAction("ChangePassword", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ContacterAdmin()
        {
            if (Session["ID"] != null && Session["ConnectionDate"] != null && Session["Role"].Equals("Colaborateur"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult ContacterAdmin(string objet,string message)
        {
          //  colabo.sendMail("a.bouhmidi@ump.ac", objet, message);
            return View();
        }
    }
}