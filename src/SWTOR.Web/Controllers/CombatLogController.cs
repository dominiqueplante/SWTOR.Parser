using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using SWTOR.Parser.Domain;

namespace SWTOR.Web.Controllers
{
    public class CombatController : Controller
    {
        private readonly IDocumentSession session;

        public CombatController(IDocumentSession session)
        {
            this.session = session;
        }

        [HttpPost]
        public ActionResult Parse(string combatLog)
        {
            var model = CombatLog.CreateCombatLog(combatLog);
            
            session.Store(model);
            session.SaveChanges();

            return RedirectToAction("Log", new { id = model.Id });
        }

        [HttpGet]
        public ActionResult Log(string id)
        {
            var model = session.Query<CombatLog>().FirstOrDefault(m => m.Id == id);
            if (model != null)
                return View(model);

            return RedirectToAction("Index", "Home");
        }
    }
}
