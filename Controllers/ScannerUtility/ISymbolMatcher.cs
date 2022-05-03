namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface ISymbolMatcher
    {
        bool MatchStartSymbol(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchEndSymbol(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchArithmeticOperation(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchLogicOperators(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchRelationalOperators(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchAssignmentOperator(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchAccessOperator(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchBraces(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchQuotationMark(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchSingleLineComment(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchCommentStart(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchCommentEnd(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchLineDelimiter(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchTokenDelimiter(string sourceOfCode, int lineNum, string lexeme, bool saveResult);
        
        bool MatchIdentifier(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

        bool MatchConstant(string sourceOfCode, int lineNum, string lexeme, bool saveResult);

    }
}