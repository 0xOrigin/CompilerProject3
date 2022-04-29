namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface IReservedKeywordMatcher
    {
        bool MatchClass(string sourceOfCode, int lineNum, string lexeme);

        bool MatchInheritance(string sourceOfCode, int lineNum, string lexeme);

        bool MatchCondition(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchBreak(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchLoop(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchReturn(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchStruct(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchSwitch(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchInclusion(string sourceOfCode, int lineNum, string lexeme);

    }
}