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
    public class HistoriquePaiementsController : Controller
    {
        private AssortieEntities db = new AssortieEntities();

        // GET: HistoriquePaiements
        public ActionResult Index()
        {
            var historiquePaiements = db.HistoriquePaiements.Include(h => h.Adherent).Include(h => h.Association);
            return View(historiquePaiements.ToList());
        }

        // GET: HistoriquePaiements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Find(id);
            if (historiquePaiement == null)
            {
                return HttpNotFound();
            }
            return View(historiquePaiement);
        }

        // GET: HistoriquePaiements/Create
        public ActionResult Create()
        {
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule");
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom");
            return View();
        }

        // POST: HistoriquePaiements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHistoriquePaiement,IdAdherent,IdAssociation,Paiement,Date")] HistoriquePaiement historiquePaiement)
        {
            if (ModelState.IsValid)
            {
                db.HistoriquePaiements.Add(historiquePaiement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", historiquePaiement.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", historiquePaiement.IdAssociation);
            return View(historiquePaiement);
        }

        // GET: HistoriquePaiements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Find(id);
            if (historiquePaiement == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", historiquePaiement.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", historiquePaiement.IdAssociation);
            return View(historiquePaiement);
        }

        // POST: HistoriquePaiements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHistoriquePaiement,IdAdherent,IdAssociation,Paiement,Date")] HistoriquePaiement historiquePaiement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiquePaiement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAdherent = new SelectList(db.Adherents, "IdAdherent", "Matricule", historiquePaiement.IdAdherent);
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", historiquePaiement.IdAssociation);
            return View(historiquePaiement);
        }

        // GET: HistoriquePaiements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Find(id);
            if (historiquePaiement == null)
            {
                return HttpNotFound();
            }
            return View(historiquePaiement);
        }

        // POST: HistoriquePaiements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Find(id);
            db.HistoriquePaiements.Remove(historiquePaiement);
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
