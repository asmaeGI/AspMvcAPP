using BusinessComponents;
using DataAccess;
using Entities;
using PagedList;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    public class UIDemandeVisaController : Controller
    {
        private Sqli_GD_Tarkett_Model db = new Sqli_GD_Tarkett_Model();
       // UDemandeVisaBusniss visa = new UDemandeVisaBusniss();
        ServiceUColaborateur.UColaborateurClient visa = new ServiceUColaborateur.UColaborateurClient();
        UColaborateurBusniss col = new UColaborateurBusniss();
        // GET: UIDemandeVisa
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(visa.afficherVisa());
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Create(int? idt)
        {
            ViewBag.Readonly = true;
            ViewBag.ReadonlyObservation = true;
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            { 
           
            if (idt != null)
            {
                    ViewBag.IdC = col.getCollaboratorsNames(idt);
                DemandeVisa visaExiste = (from v in visa.afficherVisa() where v.IdC.Equals(idt) select v).FirstOrDefault();
                if (visaExiste != null)
                {
                    return RedirectToAction("Edit", new { id = visaExiste.Id });
                }
                              
            }
            if (idt == null)
            {
                    ViewBag.IdC = col.getCollaboratorsNames(idt);
                }


                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Status,DateValiditeVisa,DateFinVisa,Observation,IdC")]DemandeVisa demandeVisa, int? idt)
        {
            ViewBag.Readonly = demandeVisa.Status.Equals("Acceptee") ? false : true;
            ViewBag.ReadonlyObservation = demandeVisa.Status.Equals("Refusee") ? false : true;
            ViewBag.IdC = col.getCollaboratorsNames(idt);
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                if (ModelState.IsValid)
                {
                    visa.ajouterDemandeVisa(demandeVisa);
                    return RedirectToAction("index");
                }
                else
                {
                    return View(demandeVisa);
                }
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (visa.supprimerDemandeVisa(id) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))

            {
                return View(visa.supprimerDemandeVisa(id));
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (!visa.supprimerDemandeVisaConfirmer(id))
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
         /*   TimeSpan duree = ((visa.detaillerDemandeVisa(id).DateFinVisa)-(visa.detaillerDemandeVisa(id).DateValiditeVisa)).Value;
            ViewBag.test = duree.Days;*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (visa.detaillerDemandeVisa(id) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(visa.detaillerDemandeVisa(id));
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (visa.modifierDemandeVisaById(id) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
           DemandeVisa demandeVisa = visa.modifierDemandeVisaById(id);
                ViewBag.IdC = col.getCollaboratorsNames(demandeVisa.IdC);
                ViewBag.Readonly = demandeVisa.Status.Equals("Acceptee") ? false : true;
           ViewBag.ReadonlyObservation = demandeVisa.Status.Equals("Refusee") ? false : true;
           return View(demandeVisa);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Status,DateValiditeVisa,DateFinVisa,Observation,IdC")]DemandeVisa demandeVisa)
        {
            ViewBag.Readonly = demandeVisa.Status.Equals("Acceptee") ? false : true;
            ViewBag.ReadonlyObservation = demandeVisa.Status.Equals("Refusee") ? false : true;

            if (ModelState.IsValid)
            {
                visa.modifierDemandeVisa(demandeVisa);
                              return RedirectToAction("Index");

            }
            ViewBag.IdC = col.getCollaboratorsNames(null); 
                if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(demandeVisa);
            }
            return RedirectToAction("Index", "Login");
        }
       

    }
}