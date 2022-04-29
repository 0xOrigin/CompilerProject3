namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface IDataTypeMatcher
    {
        bool MatchInteger(string sourceOfCode, int lineNum, string lexeme);

        bool MatchSInteger(string sourceOfCode, int lineNum, string lexeme);

        bool MatchCharacter(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchString(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchFloat(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchSFloat(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchVoid(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchBoolean(string sourceOfCode, int lineNum, string lexeme);

    }
}