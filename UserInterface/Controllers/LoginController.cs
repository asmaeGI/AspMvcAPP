using BusinessComponents;
using CommonComponents;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    public class LoginController : Controller
    {

        UColaborateurBusniss colab = new UColaborateurBusniss();
        ServiceUColaborateur.UColaborateurClient colb = new ServiceUColaborateur.UColaborateurClient();

        // GET: Login
        /// <summary>
        /// return the login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["Nom"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return RedirectToAction("Index", "UIColaborateur");
                }
                else
                    return RedirectToAction("Index", "Colaborateur");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Colaborateur col)
        {
            Log.Info("Login-page started...");

            ViewBag.Message = null;
            Colaborateur colaborateur = colab.getCollaborator(col);
            if (colaborateur != null)
            {
                Session["DateConnection"] = colaborateur.DateConnection; 
                Session["ID"] = colaborateur.IdCol;
                Session["Role"] = colaborateur.Role;
                Session["Nom"] = colaborateur.Nom == null ? "User" : colaborateur.Nom;
                Session["Prenom"] = colaborateur.Prenom == null ? " " : colaborateur.Prenom;

                if (colaborateur.Role.Equals("Admin"))
                {
                    Log.Info("Login succeeded to Admin Account:" + Session["ID"] + ":" + Session["Nom"] + " " + Session["Prenom"] + "...");
                    Log.Info("Redirection to Admin Account....");
                    return RedirectToAction("Index", "UIColaborateur");
                }
                else
                {
                    Session["Role"] = colaborateur.Role;
                    Session["ConnectionDate"] = colaborateur.DateConnection;
                    if (colaborateur.DateConnection == null)
                    {
                        Log.Info("First Login succeeded Redirection to changePassword Page...");

                        return RedirectToAction("ChangePassword", "Login", new { id = colaborateur.IdCol });
                    }
                    else
                    {
                        Log.Info("Login succeeded to Colaborateur Account...");
                        return RedirectToAction("Index", "Colaborateur");
                    }
                }
            }
            else
            {
                ViewBag.Message = "login ou password incorrect";
                Log.Info("Login Faild:incorrect username or password...");
                return View("Index");
            }

        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int? id, string Password, string CPassword)
        {
            Colaborateur colabo = colb.modifierColborateurById((int)Session["ID"]);
            if (CPassword.Equals(Password))
            {
                colabo.DateConnection = DateTime.Today;
                colabo.Password = Password;
                colb.modifierColaborateur(colabo);
                Log.Info("password has been changed ...");
                return RedirectToAction("LogOut");
            }
            else
            {
                ViewBag.Message = "Le mot de passe et le mot de passe de confirmation ne correspondent pas";
                Log.Info("password and passwordConfirmation doesn't match ...");
                return View();
            }
        }
        public ActionResult LogOut()
        {
            Log.Info("User Logout ...");
            Session["Nom"] = null;
            Session["Prenom"] = null;
            Session["Role"] = null;
            Session["ID"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(string login, string email)
        {
            IList<Colaborateur> colaborateurs = colab.getCollaborators();
            Colaborateur colaborateur = (from c in colaborateurs where c.Login.Equals(login) where c.Email.Equals(email) select c).FirstOrDefault();
            if (colaborateur == null)
            {
                ViewBag.Message = "login ou email incorrecte";
                return View();

            }
            else
            {
               /* string message = "Bonjour "+colaborateur.Nom+" "+colaborateur.Prenom+"\n"
                                 +" Les informations de connexion vers votre compte : \nLogin :"+colaborateur.Login+"\nPassword"+colaborateur.Password  ;
                colb.sendMail(email, "Password recover", message);*/
                TempData["Message"] = "Mail de récupération à été envoyé a " + email;
                return RedirectToAction("Index");

            }
        }
    }
}