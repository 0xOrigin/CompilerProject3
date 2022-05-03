namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface IDataTypeMatcher
    {
        bool MatchInteger(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

        bool MatchSInteger(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

        bool MatchCharacter(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchString(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchFloat(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchSFloat(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchVoid(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchBoolean(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

    }
}