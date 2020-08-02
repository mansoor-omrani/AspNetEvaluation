using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetEvaluation.MvcCore.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace AspNetEvaluation.MvcCore.Controllers
{
    public class MyModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        async Task<ActionResult> GetContent(bool resetBodyStream)
        {
            var body = "";

            Request.EnableBuffering();

            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 10240,
                leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }
            
            if (resetBodyStream)
            {
                Request.Body.Position = 0;
                //Request.Body.Seek(0, SeekOrigin.Begin);
            }
            
            var content = $"<b>Body:</b><br/>{body}<br/><br/><b>Content-Type:</b> {Request.ContentType}<br/><br/><b>Form Count</b>: {Request.Form.Count}<br/>";
            var items = "";

            foreach (var item in Request.Form)
            {
                items += (string.IsNullOrEmpty(items) ? "" : "<br/>") + $"<b>{item.Key}</b> {item.Value}";
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
