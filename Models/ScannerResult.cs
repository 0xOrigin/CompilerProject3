using System;
using System.Collections.Generic;

namespace CompilerProject3.Models
{
    public class ScannerResult
    {
        private static volatile ScannerResult _scannerInstance;
        private List<List<string>> Result;
        private ScannerResult()
        {
            this.Result = new List<List<string>>();
        }

        public static ScannerResult GetInstance()
        {
            if(_scannerInstance == null)
                _scannerInstance = new ScannerResult();

            return _scannerInstance;
        }


        public void AddToken(string sourceOfCode, int lineNum, string tokenText, string tokenType, string state, bool saveResult)
        {
            if(!saveResult) return;

            Result.Add(new List<string> { sourceOfCode, lineNum.ToString(), tokenText, tokenType, state });
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
