using System;
using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class SymbolMatcher : ISymbolMatcher
    {
        private readonly ScannerResult result = ScannerResult.GetInstance();

        // @|^
        public bool MatchStartSymbol(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '@' || c == '^' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state != 2) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // $|#
        public bool MatchEndSymbol(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '$' || c == '#' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state != 2) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // +|-|*|/
        public bool MatchArithmeticOperation(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // &&,||,~
        public bool MatchLogicOperators(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // ==|<|>|!=|<=|>=
        public bool MatchRelationalOperators(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // =
        public bool MatchAssignmentOperator(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // ->
        public bool MatchAccessOperator(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // {|}|[|]
        public bool MatchBraces(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // "|'
        public bool MatchQuotationMark(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // ***
        public bool MatchSingleLineComment(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 6 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '*' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == '*' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == '*' ? 6 : 0);
                        i++;
                        break;
                }
            }

            if (state != 6) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // </
        public bool MatchCommentStart(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 4 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '<' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == '/' ? 4 : 0);
                        i++;
                        break;
                }
            }

            if (state != 4) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // />
        public bool MatchCommentEnd(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 4 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '/' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == '>' ? 4 : 0);
                        i++;
                        break;
                }
            }

            if (state != 4) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // ;|\n
        public bool MatchLineDelimiter(string sourceOfCode, int lineNum, string lexeme)
        {
            if(lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == ';' || c == '\n' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state != 2) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // \t| 
        public bool MatchTokenDelimiter(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == ' '  || c == '\t' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state != 2) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // [a-zA-Z_]+[a-zA-Z_0-9]*
        public bool MatchIdentifier(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }
    }
}
