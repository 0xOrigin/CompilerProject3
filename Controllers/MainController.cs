using CompilerProject3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompilerProject3.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scan(string sourceOfCode, string code)
        {
            ScannerResult.GetInstance().ClearResults();

            return RedirectToAction("Scan", "Result", new { sourceOfCode = sourceOfCode, code = code });
        }

        [HttpPost]
        public IActionResult Parse(string sourceOfCode, string code)
        {
            ScannerResult.GetInstance().ClearResults();
            ParserResult.GetInstance().ClearResults();

            return RedirectToAction("Parse", "Result", new { sourceOfCode = sourceOfCode, code = code });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
