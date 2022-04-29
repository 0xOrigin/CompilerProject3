using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class ReservedKeywordMatcher : IReservedKeywordMatcher
    {
        private ScannerResult result = ScannerResult.GetInstance();
        
        // Type
        public bool MatchClass(string sourceOfCode, int lineNum, string lexeme)
        {
            
            throw new System.NotImplementedException();
        }

        // Infer
        public bool MatchInheritance(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // If|Else
        public bool MatchCondition(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Endthis
        public bool MatchBreak(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // However|When
        public bool MatchLoop(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Respondwith
        public bool MatchReturn(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Srap
        public bool MatchStruct(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Scan|Conditionof
        public bool MatchSwitch(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Require
        public bool MatchInclusion(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }
    }
}
