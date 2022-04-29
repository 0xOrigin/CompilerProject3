using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class DataTypeMatcher : IDataTypeMatcher
    {
        private ScannerResult result = ScannerResult.GetInstance();

        // Ipok
        public bool MatchInteger(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Sipok
        public bool MatchSInteger(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Craf
        public bool MatchCharacter(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Sequence
        public bool MatchString(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Ipokf
        public bool MatchFloat(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Sipokf
        public bool MatchSFloat(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Valueless
        public bool MatchVoid(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Rational
        public bool MatchBoolean(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }
    }
}
