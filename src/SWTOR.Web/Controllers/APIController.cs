using System;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;

namespace SWTOR.Web.Controllers
{
    public class APIController : Controller
    {
        // This action is a remnant, an attempt to parse incoming file streams that has never 
        // been successful.  Still waiting to see an example which would work.
        [HttpPost]
        public ActionResult Parse()
        {
            object returnData = null;

            var parser = new Parser.Parser();
            if (Request.ContentLength > 0)
            {
                Stream stream = Request.InputStream;
                returnData = parser.Parse(new StreamReader(stream));
            }
            var ser = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            return new ContentResult { Content = ser.Serialize(returnData), ContentType = "application/json" };
        }

        [HttpPost]
        public ActionResult ParseText(string combatLog)
        {
            var parser = new Parser.Parser();
            var returnData = parser.Parse(new StringReader(combatLog));

            var ser = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            return new ContentResult { Content = ser.Serialize(returnData), ContentType = "application/json" };
        }
    }
}
