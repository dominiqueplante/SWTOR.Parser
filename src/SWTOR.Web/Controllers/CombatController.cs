using System.Linq;
using System.Web.Mvc;
using SWTOR.Parser.Domain;
using SWTOR.Parser;
using SWTOR.Web.Data;

namespace SWTOR.Web.Controllers
{
    public class CombatController : Controller
    {
        private readonly IRepository<CombatLog> repo;
        private readonly IHashCreator hasher;
        private readonly IStringParser parser;
        private readonly ICombatParser combatParser;

        public CombatController(IRepository<CombatLog> repo, IHashCreator hasher, IStringParser parser, ICombatParser combatParser)
        {
            this.repo = repo;
            this.hasher = hasher;
            this.parser = parser;
            this.combatParser = combatParser;
        }

        [HttpPost]
        public ActionResult Parse(string combatLog)
        {
            var hash = hasher.CreateHash(combatLog);

            // Determine if combat log has already been parsed, that is, strings hash to the same value, if so, retrieve it
            var model = repo.Query().FirstOrDefault(m => m.Id == hash);
            if (model != null)
                return RedirectToAction("Log", new { id = model.Id });

            // This is a new combat log, so parse it and store it
            var log = parser.ParseString(combatLog);
            model = combatParser.Parse(log);

            combatParser.Clean(model);
            model.Id = hash;
            repo.Store(model);
            repo.SaveChanges();

            return RedirectToAction("Log", new { id = model.Id });
        }

        [HttpGet]
        public ActionResult Log(string id)
        {
            var model = repo.Query().FirstOrDefault(m => m.Id == id);
            if (model != null)
                return View(model);

            return RedirectToAction("Index", "Home");
        }
    }
}
