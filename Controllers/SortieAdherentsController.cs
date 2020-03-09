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
