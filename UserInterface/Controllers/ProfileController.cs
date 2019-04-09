using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    public class ProfileController : Controller
    {
        ServiceUColaborateur.UColaborateurClient colabo = new ServiceUColaborateur.UColaborateurClient();
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (colabo.detaillerColaborateur(Convert.ToInt32(Session["ID"])) == null)
            {
                return HttpNotFound();
            }
            if (Session["ID"] != null)
            {
                if (Session["Role"].Equals("Colaborateur") && Session["ConnectionDate"] == null)
                {
                    return RedirectToAction("ChangePassword", "Login");
                }
                else
                {
                    return View(colabo.detaillerColaborateur(Convert.ToInt32(Session["ID"])));
                }
            }
            else
                return RedirectToAction("Index", "Login");

        }
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

            if (Session["ID"] != null)
            {
                if (Session["Role"].Equals("Colaborateur") && Session["ConnectionDate"] == null)
                {
                    return RedirectToAction("ChangePassword", "Login");
                }
                else
                {
                    return View(colabo.modifierColborateurById(id));
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCol,Nom,Prenom,Role,Cin,Email,Poste,Equipe,NombreDeplacement,Anciennete,DateValiditeVisa,DateFinVisa,Sexe,Login,Password")] Colaborateur colaborateur)
        {
            if (ModelState.IsValid)
            {
                colabo.modifierColaborateur(colaborateur);
                return RedirectToAction("Index");
            }
            if (Session["ID"] != null)
            {
                if (Session["Role"].Equals("Colaborateur") && Session["ConnectionDate"] == null)
                {
                    return RedirectToAction("ChangePassword", "Login");
                }
                else
                {
                    return View(colaborateur);
                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
    }
}