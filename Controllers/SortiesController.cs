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
    public class SortiesController : Controller
    {
        private AssortieEntities db = new AssortieEntities();

        // GET: Sorties
        public ActionResult Index()
        {
            if (Session["Adherent"] != null)
            {
                Adherent adherent = (Adherent)Session["Adherent"];

                var sorties = db.Sorties.Include(s => s.Association).Where(i => i.IdAssociation == adherent.IdAssociation);
                return View(sorties.ToList());
            }

            return RedirectToAction("Connexion", "Adherents");
        }

        public ActionResult ListeSortiesAsso()
        {
            if (Session["Adherent"] != null)
            {
                Adherent adherent = (Adherent)Session["Adherent"];

                var sorties = db.Sorties.Include(s => s.Association).Where(i => i.IdAssociation == adherent.IdAssociation);
                return View(sorties.ToList());
            }

            return RedirectToAction("Connexion", "Adherents");
        }

        // GET: Sorties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sortie sortie = db.Sorties.Find(id);
            if (sortie == null)
            {
                return HttpNotFound();
            }
            return View(sortie);
        }

        [HttpGet, ActionName("Participer")]
        public ActionResult Participer(int idSortie, int? idAdherent)
        {
            Sortie sortie = db.Sorties.Find(idSortie);
            sortie.Inscription = true;

            Adherent adherent = db.Adherents.Find(idAdherent);
            adherent.Solde -= sortie.Prix;

            SortieAdherent sortieAdherent = new SortieAdherent();
            sortieAdherent.Association = adherent.Association;
            sortieAdherent.Adherent = adherent;
            sortieAdherent.Sortie = sortie;
            db.SortieAdherents.Add(sortieAdherent);

            HistoriquePaiement historiquePaiement = new HistoriquePaiement();
            historiquePaiement.Adherent = adherent;
            historiquePaiement.Association = adherent.Association;
            historiquePaiement.Paiement = sortie.Prix;
            historiquePaiement.Date = DateTime.Now;

            db.HistoriquePaiements.Add(historiquePaiement);

            db.SaveChanges();
            return RedirectToAction("Details/" + idSortie);
        }

        [HttpGet, ActionName("Annuler")]
        public ActionResult Annuler(int idSortie, int? idAdherent, int? idHistoriquePaiement)
        {
            idHistoriquePaiement = 4;
            Sortie sortie = db.Sorties.Find(idSortie);
            sortie.Inscription = false;

            Adherent adherent = db.Adherents.Find(idAdherent);
            adherent.Solde += sortie.Prix;

            SortieAdherent sortieAdherent = db.SortieAdherents.Find(idSortie, idAdherent, adherent.IdAssociation);
            db.SortieAdherents.Remove(sortieAdherent);

            //HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Find(idHistoriquePaiement);
            //db.HistoriquePaiements.Remove(historiquePaiement);

            db.SaveChanges();
            return RedirectToAction("Details/" + idSortie);
        }

        // GET: Sorties/Create
        public ActionResult Create()
        {
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom");
            return View();
        }

        // POST: Sorties/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSortie,IdAssociation,Nom,Prix,Description,Photo,Date,CapaciteMinimum,CapaciteMaximum")] Sortie sortie)
        {
            if (ModelState.IsValid)
            {
                db.Sorties.Add(sortie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortie.IdAssociation);
            return View(sortie);
        }

        // GET: Sorties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sortie sortie = db.Sorties.Find(id);
            if (sortie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortie.IdAssociation);
            return View(sortie);
        }

        // POST: Sorties/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSortie,IdAssociation,Nom,Prix,Description,Photo,Date,CapaciteMinimum,CapaciteMaximum")] Sortie sortie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sortie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAssociation = new SelectList(db.Associations, "IdAssociation", "Nom", sortie.IdAssociation);
            return View(sortie);
        }

        // GET: Sorties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sortie sortie = db.Sorties.Find(id);
            if (sortie == null)
            {
                return HttpNotFound();
            }
            return View(sortie);
        }

        // POST: Sorties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sortie sortie = db.Sorties.Find(id);
            db.Sorties.Remove(sortie);
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
