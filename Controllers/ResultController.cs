using System;
using System.Collections.Generic;
using CompilerProject3.Models;
using CompilerProject3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CompilerProject3.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View(new CompilerResults());
        }

        public IActionResult Scan(string sourceOfCode, string code)
        {
            new Scanner(sourceOfCode, code).Scan();

            return RedirectToAction("Index");
        }

        public IActionResult Parse(string sourceOfCode, string code)
        {
            new Scanner(sourceOfCode, code).Scan();
            new Parser().Parse();

            return RedirectToAction("Index");
        }
    }
}
