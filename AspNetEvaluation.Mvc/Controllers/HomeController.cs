using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetEvaluation.Mvc.Controllers
{
    public class MyModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        async Task<ActionResult> GetContent(bool resetBodyStream)
        {
            var body = "";

            using (var reader = new StreamReader(Request.InputStream))
            {
                body = await reader.ReadToEndAsync();
            }
            
            if (resetBodyStream)
            {
                Request.InputStream.Seek(0, SeekOrigin.Begin);
            }

            var content = $"<b>Body:</b><br/>{body}<br/><br/><b>Content-Type:</b> {Request.ContentType}<br/><br/><b>Form Count</b>: {Request.Form.Count}<br/>";
            var items = "";

            foreach (string key in Request.Form)
            {
                items += (string.IsNullOrEmpty(items) ? "" : "<br/>") + $"<b>{key}</b> {Request.Form[key]}";
            }

            content += $"<br/>{items}";

            return Content(content, "text/html");
        }
        public Task<ActionResult> Test1()
        {
            return GetContent(false);
        }
        public Task<ActionResult> Test2(MyModel model)
        {
            return GetContent(false);
        }
        public Task<ActionResult> Test1r()
        {
            return GetContent(true);
        }
        public Task<ActionResult> Test2r(MyModel model)
        {
            return GetContent(true);
        }
    }
}