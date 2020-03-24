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

        public ActionResult MesSorties()
        {
            if (Session["Adherent"] != null)
            {
                Adherent adherent = (Adherent)Session["Adherent"];

                var sortieAdherents = db.SortieAdherents.Include(s => s.Adherent).Include(s => s.Association).Include(s => s.Sortie).Where(i => i.IdAssociation == adherent.IdAssociation && i.IdAdherent == adherent.IdAdherent);
                return View(sortieAdherents.ToList());
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
        public ActionResult Participer(int idSortie, int? idAdherent, int? idAssociation, int? idHistoriquePaiement)
        {
            Sortie sortie = db.Sorties.Find(idSortie);
            if (sortie.CapaciteActuelle != sortie.CapaciteMaximum)
            {
                sortie.CapaciteActuelle++;
            }

            Adherent adherent = db.Adherents.Find(idAdherent);
            adherent.Solde -= sortie.Prix;

            SortieAdherent existSortieAdherent = db.SortieAdherents.Find(idSortie, idAdherent, idAssociation);
            if (existSortieAdherent == null)
            {
                SortieAdherent sortieAdherent = new SortieAdherent();
                sortieAdherent.IdAssociation = adherent.Association.IdAssociation;
                sortieAdherent.IdAdherent = adherent.IdAdherent;
                sortieAdherent.IdSortie = sortie.IdSortie;
                db.SortieAdherents.Add(sortieAdherent);
            }

            HistoriquePaiement existePaiement = db.HistoriquePaiements.Where(a => a.IdAdherent == adherent.IdAdherent && a.IdSortie == idSortie).SingleOrDefault();
            if (existePaiement == null)
            {
                HistoriquePaiement historiquePaiement = new HistoriquePaiement();
                historiquePaiement.Adherent = adherent;
                historiquePaiement.Association = adherent.Association;
                historiquePaiement.Paiement = sortie.Prix;
                historiquePaiement.Date = DateTime.Now;
                historiquePaiement.IdSortie = idSortie;
                db.HistoriquePaiements.Add(historiquePaiement);
            }

            db.SaveChanges();
            return RedirectToAction("Details/" + idSortie);
        }

        [HttpGet, ActionName("Annuler")]
        public ActionResult Annuler(int idSortie, int? idAdherent,int? idAssociation, int? idHistoriquePaiement)
        {
            Sortie sortie = db.Sorties.Find(idSortie);
            if (sortie.CapaciteActuelle > 0)
            {
                sortie.CapaciteActuelle--;
            }

            Adherent adherent = db.Adherents.Find(idAdherent);

            SortieAdherent sortieAdherent = db.SortieAdherents.Find(idSortie, idAdherent, idAssociation);
            if (sortieAdherent != null)
            {
                db.SortieAdherents.Remove(sortieAdherent);
            }

            HistoriquePaiement historiquePaiement = db.HistoriquePaiements.Where(a => a.IdAdherent == adherent.IdAdherent && a.IdSortie == idSortie).SingleOrDefault();
            if (historiquePaiement != null)
            {
                db.HistoriquePaiements.Remove(historiquePaiement);
                adherent.Solde += sortie.Prix;
            }

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
