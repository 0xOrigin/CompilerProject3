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
            System.Threading.Thread.Sleep(600);
            return View(new CompilerResults());
        }

        public void Scan(string sourceOfCode, string code)
        {
            new Scanner(sourceOfCode, code).Scan();
        }

        public void Parse(string sourceOfCode, string code)
        {
            new Scanner(sourceOfCode, code).Scan();
            new Parser().Parse();
        }
    }
}
