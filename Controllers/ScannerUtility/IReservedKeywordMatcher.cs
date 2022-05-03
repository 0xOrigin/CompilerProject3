namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface IReservedKeywordMatcher
    {
        bool MatchClass(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

        bool MatchInheritance(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

        bool MatchCondition(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchBreak(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchLoop(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchReturn(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchStruct(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchSwitch(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchInclusion(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

    }
}