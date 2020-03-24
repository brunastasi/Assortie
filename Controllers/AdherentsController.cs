using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assortie.Models;

namespace Assortie.Controllers
{
    public class AdherentsController : Controller
    {
        private AssortieEntities db = new AssortieEntities();

        public ActionResult Connexion()
        {
            return View();
        }

        // POST: Utilisateurs/Connexion
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "Login,Password")] Adherent adherentView)
        {
            if (ModelState.IsValid)
            {
                Adherent adherent = db.Adherents.FirstOrDefault(a => a.Login == adherentView.Login && a.Password == adherentView.Password);

                if (adherent != null)
                {

                    Session["Adherent"] = adherent;

                    return RedirectToAction("Index", "Sorties");
                }
                else
                {
                    ViewBag.Erreur = "Erreur de connexion !";
                }
            }

            return View();
        }

        public ActionResult Deconnexion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = db.Adherents.Find(id);

            if (id != null)
            {
                if (Session["Adherent"] != null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Adherents/Create
        public ActionResult Create()
        {
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom");
            return View();
        }

        // POST: Adherents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdherent,IdAssociation,Matricule,Nom,Prenom,Email,Telephone,Cotisation,Login,Password,Responsable,Solde")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", adherent.IdAssociation);
            return View(adherent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
