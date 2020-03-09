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
            Adherent adherent = (Adherent)Session["Adherent"];

            var historiquePaiements = db.HistoriquePaiements.Include(h => h.Adherent).Include(h => h.Association).Where(i => i.IdAssociation == adherent.IdAssociation);
            return View(historiquePaiements.ToList());
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
