using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodingTest.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

      
        
        public IActionResult UploadFile()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile(FileModel model)
        {
            
            if (ModelState.IsValid)
            {
                string uploadFileName = null;
                if (model.File != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                    string filePath = Path.Combine(uploadsFolder, model.File.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create)) 
                    {
                        model.File.CopyTo(stream);
                    }



                    string text;
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        text = streamReader.ReadToEnd();
                    }
                    string[] fileText = text.Split(" ");
                    string mostCommon = fileText.GroupBy(v => v)
                    .OrderByDescending(g => g.Count())
                     .First()
                     .Key;
                    
                    
                    string result = String.Join(" ",text);
                    result = Regex.Replace(result, mostCommon, "foo" + mostCommon + "bar", RegexOptions.IgnoreCase);
                    ViewBag.Text = result;

                }
               
            }
          

            return View();
        }
    }
}
