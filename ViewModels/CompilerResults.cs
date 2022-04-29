using CompilerProject3.Models;

namespace CompilerProject3.ViewModels
{
    public class CompilerResults
    {
        public readonly ScannerResult ScannerResult = ScannerResult.GetInstance();
        public readonly ParserResult ParserResult = ParserResult.GetInstance();
    }
}
