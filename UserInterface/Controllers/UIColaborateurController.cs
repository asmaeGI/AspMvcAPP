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



namespace UserInterface.Controllers
{

    public class UIColaborateurController : Controller
    {
        ServiceUColaborateur.UColaborateurClient colabo =new ServiceUColaborateur.UColaborateurClient();
        Colab test = new Colab();
        private Sqli_GD_Tarkett_Model db = new Sqli_GD_Tarkett_Model();

        // GET: Colaborateur
        public ActionResult Index(string sortOrder, string searchString,string currentFilter,int? page,int? pageSize)
        {
            int defaSize = (pageSize ?? 5);

            ViewBag.psize = defaSize;

            //Dropdownlist code for PageSize selection  
            //In View Attach this  
            ViewBag.PageSize = new List<SelectListItem>()
         {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" },
         };

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nom_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "prenom" ? "prenom_desc" : "prenom";
            ViewBag.MailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.TeamSortParm = sortOrder == "equipe" ? "equipe_desc" : "equipe";
            ViewBag.PosteSortParm = sortOrder == "poste" ? "poste_desc" : "poste";
            return View(test.afficherColab(sortOrder, searchString,currentFilter,page,defaSize));
        }
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colaborateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Cin,Email,Poste,Equipe,NombreDeplacement,Anciennete,DateValiditeVisa,DateFinVisa,Sexe,Login,Password")] Colaborateur colaborateur)
        {
            if (ModelState.IsValid)
            {
                colabo.ajouterColab(colaborateur);
                return RedirectToAction("Index");
            }
            return View(colaborateur);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (colabo.supprimerColaborateur(id)== null)
            {
                return HttpNotFound();
            }
            return View(colabo.supprimerColaborateur(id));
        }

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (colabo.modifierColborateur(id) == null)
            {
                return HttpNotFound();
            }
            return View(colabo.modifierColborateur(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Cin,Email,Poste,Equipe,NombreDeplacement,Anciennete,DateValiditeVisa,DateFinVisa,Sexe,Login,Password")] Colaborateur colaborateur)
        {
            if (ModelState.IsValid)
            {
                colabo.modifierColaborateur(colaborateur);
                return RedirectToAction("Index");
            }
            return View(colaborateur);
        }
      public ActionResult Export(string ExportType)
        {
            String path = Server.MapPath("~/Reports/ListeColaborateur.rdlc");
            byte[] renderedByte = colabo.ExporterColaborateur(ExportType, path);
            string fileNameExtension = colabo.FileExtension(ExportType);
            Response.AddHeader("content-disposition", "attachment;file_name=colaborateur_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }

    }


}