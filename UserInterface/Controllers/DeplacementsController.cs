using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataAccess;
using BusinessComponents;
using PagedList;

namespace UserInterface.Controllers
{
    public class DeplacementsController : Controller
    {
        ServiceUColaborateur.UColaborateurClient deplace = new ServiceUColaborateur.UColaborateurClient();
        UColaborateurBusniss colaborateur = new UColaborateurBusniss();
        DeplacementBusniss dep = new DeplacementBusniss();
        // GET: Deplacements
        /// <summary>
        /// Admin redirection to displacement home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(deplace.afficherDeplacement());
            }
            else
                return RedirectToAction("Index", "Login");

        }

        // GET: Deplacements/Details/5
        /// <summary>
        ///Get displacement details using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (deplace.detaillerDeplacement(id) == null)
            {
                return HttpNotFound();
            }
            return View(deplace.detaillerDeplacement(id));
        }

        // GET: Deplacements/Create
        /// <summary>
        /// Return formulaire to create new displacement
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.IdD = colaborateur.getCollaboratorsNames(null);
            ViewBag.IdSelected = new SelectList(Enumerable.Empty<SelectListItem>());
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Deplacements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Send displacement formulaire to Data Acces layer to add new displacement in database
        /// </summary>
        /// <param name="deplacement"></param>
        /// <param name="IdU"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Cout,Observation")] Deplacement deplacement, int?[] IdU)
        {
            ViewBag.IdT = colaborateur.getCollaboratorsNames(null);
            ViewBag.IdU = new SelectList(Enumerable.Empty<SelectListItem>());

            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                if (ModelState.IsValid)
                {
                    deplace.ajouterDeplacement(deplacement, IdU);
                    return RedirectToAction("Index");
                }

                return View(deplacement);
            }
            return RedirectToAction("Index", "Login");
        }


        // GET: Deplacements/Edit/5
        /// <summary>
        /// return the edit formulaire
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            ViewBag.IdD = colaborateur.getCollaboratorsNames(null);
            ViewBag.IdSelected = new SelectList(Enumerable.Empty<SelectListItem>());

            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(deplace.modifierDeplacementById(id));
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Deplacements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// send the updated informations to dataAccess Layer to edit displacement in database
        /// </summary>
        /// <param name="deplacement"></param>
        /// <param name="IdU"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Cout,Observation")] Deplacement deplacement, int?[] IdU)
        {
            ViewBag.IdT = colaborateur.getCollaboratorsNames(null);
            ViewBag.IdU = new SelectList(Enumerable.Empty<SelectListItem>());

            if (ModelState.IsValid)
            {
                deplace.modifierDeplacement(deplacement, IdU);
                return RedirectToAction("Index");
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(deplacement);
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Deplacements/Delete/5
        /// <summary>
        /// delete a displacement using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (deplace.supprimerDeplacement(id) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(deplace.supprimerDeplacement(id));
            }
            else
                return RedirectToAction("Index", "Login");
        }

        // POST: Deplacements/Delete/5
        /// <summary>
        /// confirme the delete from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (!deplace.supprimerDeplacementConfirmer(id))
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// propose two collaborators to the next displacement
        /// </summary>
        /// <returns></returns>
        public ActionResult PropositionColaborateurs()
        {
            return View(deplace.ProposeColaborateur().ToList().Take(2));
        }
        /// <summary>
        /// export displacement liste ss
        /// </summary>
        /// <param name="ExportType"></param>
        /// <returns></returns>
        public ActionResult Export(string ExportType)
        {
            String path = Server.MapPath("~/Reports/DeplacementReport.rdlc");
            byte[] renderedByte = dep.ExportCollaborators(ExportType, path);
            string fileNameExtension = dep.FileExtension(ExportType);
            Response.AddHeader("content-disposition", "attachment;file_name=colaborateur_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }

    }
}
