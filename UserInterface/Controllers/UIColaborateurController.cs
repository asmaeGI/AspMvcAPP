using System;
using BusinessComponents;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Xml.Linq;
using DataAccess;
using PagedList;
using CommonComponents;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace UserInterface.Controllers
{

    public class UIColaborateurController : Controller
    {
        ServiceUColaborateur.UColaborateurClient colabo = new ServiceUColaborateur.UColaborateurClient();
        // GET: Colaborateur
        /// <summary>
        /// Redirection to admin home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(colabo.afficherColab());
            }
            else
                return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// create collaborator formulaire 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
          /*if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {*/
                return View();
         //   }
           // return RedirectToAction("Index", "Login");

        }
        /// <summary>
        /// Send Collaborator informaions to Data Access Layer and add collaborator in database
        /// </summary>
        /// <param name="colaborateur"></param>
        /// <returns></returns>
        // POST: Colaborateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCol,Nom,Prenom,Role,Cin,Email,Poste,Equipe,NombreDeplacement,Anciennete,DateValiditeVisa,DateFinVisa,Sexe,Login,Password,DateConnection,IdD")] Colaborateur colaborateur)
        {
           IList<Colaborateur> colaborateurs = colabo.getColaborateurs();
            try
            {
                Colaborateur col = (from c in colaborateurs where c.Login.Equals(colaborateur.Login) select c).FirstOrDefault();
                if (ModelState.IsValid && col == null)
                {
                    colabo.ajouterColab(colaborateur);
                       string message = "Bonjour\nLogin:" + colaborateur.Login + "\nPassword" + colaborateur.Password;
                        colabo.sendMail(colaborateur.Email,"Création de compte collaborateur",message);
                    Log.Info("User have been created.....");
                    return RedirectToAction("Index");
                }
                else if (col != null)
                {
                    ViewBag.Message = "login existe déja ";
                }
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
            }
            return View(colaborateur);

        }
        /// <summary>
        /// Get :Supprimer un collaborateur par id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (colabo.supprimerColaborateur(id) == null)
                {
                    return HttpNotFound();
                }
                if (Session["ID"] != null && Session["Role"].Equals("Admin"))
                {
                    return View(colabo.supprimerColaborateur(id));
                }
                else
                    return RedirectToAction("Index", "Login");
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return RedirectToAction("Index");
            }

        }
        /// <summary>
        /// Post:supprimer un collaborateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (!colabo.supprimerColaborateurConfirmer(id))
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get:retourne les detailles d'un collaborateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (colabo.detaillerColaborateur(id) == null)
            {
                return HttpNotFound();
            }
            return View(colabo.detaillerColaborateur(id));
        }
        /// <summary>
        /// Get : Editer un collaborateur 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (colabo.modifierColborateurById(id) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                return View(colabo.modifierColborateurById(id));
            }
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// Post : Editer collaborateur
        /// </summary>
        /// <param name="colaborateur"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCol,Nom,Prenom,Role,Cin,Email,Poste,Equipe,NombreDeplacement,Anciennete,DateValiditeVisa,DateFinVisa,Sexe,Login,Password,DateConnection,IdD")] Colaborateur colaborateur)
        {
            IList<Colaborateur> colaborateurs = colabo.getColaborateurs();
            Colaborateur col = (from c in colaborateurs where c.IdCol!=(colaborateur.IdCol) where c.Login.Equals(colaborateur.Login) select c).FirstOrDefault();
            if (Session["ID"] != null && Session["Role"].Equals("Admin"))
            {
                if (ModelState.IsValid && col == null)
                {
                    colaborateur.DateConnection = new DateTime();
                    colabo.modifierColaborateur(colaborateur);
                    return RedirectToAction("Index");
                }
                else if (col != null )
                {
                    ViewBag.Message = "login existe déja ";
                    return View(colaborateur);

                }
            }
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// Exporter la liste des colaborateur 
        /// </summary>
        /// <param name="ExportType"></param>
        /// <returns></returns>
        public ActionResult Export(string ExportType)
        {
            String path = Server.MapPath("~/Reports/ListeColaborateur.rdlc");
            byte[] renderedByte = colabo.ExporterColaborateur(ExportType, path);
            string fileNameExtension = colabo.FileExtension(ExportType);
            Response.AddHeader("content-disposition", "attachment;file_name=colaborateur_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }

        public ActionResult Message(int? id)
        {
            Colaborateur colaborateur =colabo.getColaborateur(id);
            return View(colaborateur);
        }

        [HttpPost]
        public ActionResult Message(string colMail ,string objet, string message,int? IdCol)
        {
            Colaborateur colaborateur = colabo.getColaborateur(IdCol);

            ViewBag.Status = "Email Sent Successfully.";
            if (!colabo.sendMail(colMail,objet,message))
            {
                ViewBag.Status = "Problem while sending email, Please check details.";
            }

            return View(colaborateur);
        }
        public ActionResult CollaborateursReport()
        {
            try
            {
                ReportViewer reportViewer = new ReportViewer();
                ReportDataSource reportDataSource1 = new ReportDataSource();
                ReportDataSource reportDataSource2 = new ReportDataSource();
                ReportDataSource reportDataSource3 = new ReportDataSource();

                reportViewer.ProcessingMode = ProcessingMode.Local;

                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.Width = System.Web.UI.WebControls.Unit.Pixel(900);
                reportViewer.Height = System.Web.UI.WebControls.Unit.Pixel(700);
                reportViewer.AsyncRendering = true;

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/CollaborateursReport.rdlc";

                reportDataSource1.Name = "DataSetViewer";
                reportDataSource1.Value = colabo.afficherColab().ToList();
                reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                reportDataSource2.Name = "DataSetDeplacement";
                reportDataSource2.Value = colabo.getDeplacements().ToList();
                reportViewer.LocalReport.DataSources.Add(reportDataSource2);
                reportDataSource3.Name = "DataSetVisa";
                reportDataSource3.Value = colabo.afficherVisa().ToList();
                reportViewer.LocalReport.DataSources.Add(reportDataSource3);
                ViewBag.ReportViewer = reportViewer;
                return View();
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return RedirectToAction("Index");
            }
        }
    }

}