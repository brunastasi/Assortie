using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assortie.Models;

namespace Assortie.Controllers
{
    public class SortieAdherentsController : Controller
    {
        private AssortieEntities db = new AssortieEntities();

        // GET: SortieAdherents
        public ActionResult Index()
        {
            if (Session["Adherent"] != null)
            {
                Adherent adherent = (Adherent)Session["Adherent"];

                var sortieAdherents = db.SortieAdherents.Include(s => s.Adherent).Include(s => s.Association).Include(s => s.Sortie).Where(i => i.IdAssociation == adherent.IdAssociation);
                return View(sortieAdherents.ToList());
            }
            return RedirectToAction("Connexion", "Adherents");
        }

        // GET: SortieAdherents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SortieAdherent sortieAdherent = db.SortieAdherents.Find(id);
            if (sortieAdherent == null)
            {
                return HttpNotFound();
            }
            return View(sortieAdherent);
        }

        // GET: SortieAdherents/Create
        public ActionResult Create()
        {
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule");
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom");
            ViewBag.IdSortie = new SelectList(db.Sorties, "IdSortie", "Nom");
            return View();
        }

        // POST: SortieAdherents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSortie,IdAdherent,IdAssociation")] SortieAdherent sortieAdherent)
        {
            if (ModelState.IsValid)
            {
                db.SortieAdherents.Add(sortieAdherent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", sortieAdherent.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortieAdherent.IdAssociation);
            ViewBag.IdSortie = new SelectList(db.Sorties, "IdSortie", "Nom", sortieAdherent.IdSortie);
            return View(sortieAdherent);
        }

        // GET: SortieAdherents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SortieAdherent sortieAdherent = db.SortieAdherents.Find(id);
            if (sortieAdherent == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", sortieAdherent.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortieAdherent.IdAssociation);
            ViewBag.IdSortie = new SelectList(db.Sorties, "IdSortie", "Nom", sortieAdherent.IdSortie);
            return View(sortieAdherent);
        }

        // POST: SortieAdherents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSortie,IdAdherent,IdAssociation")] SortieAdherent sortieAdherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sortieAdherent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", sortieAdherent.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortieAdherent.IdAssociation);
            ViewBag.IdSortie = new SelectList(db.Sorties, "IdSortie", "Nom", sortieAdherent.IdSortie);
            return View(sortieAdherent);
        }

        // GET: SortieAdherents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SortieAdherent sortieAdherent = db.SortieAdherents.Find(id);
            if (sortieAdherent == null)
            {
                return HttpNotFound();
            }
            return View(sortieAdherent);
        }

        // POST: SortieAdherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SortieAdherent sortieAdherent = db.SortieAdherents.Find(id);
            db.SortieAdherents.Remove(sortieAdherent);
            db.SaveChanges();
            return RedirectToAction("Index");
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
