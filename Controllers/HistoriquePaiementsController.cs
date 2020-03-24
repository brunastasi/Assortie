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

            var historiquePaiements = db.HistoriquePaiements.Include(h => h.Adherent).Include(h => h.Association).Include(h => h.Sortie).Where(i => i.IdAssociation == adherent.IdAssociation);
            return View(historiquePaiements.ToList());
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
