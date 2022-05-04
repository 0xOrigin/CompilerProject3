using System.Collections.Generic;

namespace CompilerProject3.Models
{
    public class ParserResult
    {
        private static volatile ParserResult _parserInstance;
        private List<List<string>> Result;

        private ParserResult()
        {
            this.Result = new List<List<string>>();
        }

        public static ParserResult GetInstance()
        {
            if (_parserInstance == null)
                _parserInstance = new ParserResult();

            return _parserInstance;
        }


        public void AddLine(string sourceOfCode, int lineNum, string status, string ruleUsed)
        {
            Result.Add(new List<string> { sourceOfCode, lineNum.ToString(), status, ruleUsed });
        }

        public List<List<string>> GetResultsList()
        {
            return Result;
        }

        public void ClearResults()
        {
            Result.Clear();
        }
    }
}
