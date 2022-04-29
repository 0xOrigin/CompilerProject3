namespace CompilerProject3.Controllers.ScannerUtility
{
    public interface ISymbolMatcher
    {
        bool MatchStartSymbol(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchEndSymbol(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchArithmeticOperation(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchLogicOperators(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchRelationalOperators(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchAssignmentOperator(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchAccessOperator(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchBraces(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchQuotationMark(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchSingleLineComment(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchCommentStart(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchCommentEnd(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchLineDelimiter(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchTokenDelimiter(string sourceOfCode, int lineNum, string lexeme);
        
        bool MatchIdentifier(string sourceOfCode, int lineNum, string lexeme);

    }
}