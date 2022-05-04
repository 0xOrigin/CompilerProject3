using System;
using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class SymbolMatcher : ISymbolMatcher
    {
        private readonly ScannerResult result = ScannerResult.GetInstance();

        // @|^
        public bool MatchStartSymbol(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // $|#
        public bool MatchEndSymbol(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // +|-|*|/
        public bool MatchArithmeticOperation(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 4 && state != 6 && state != 8 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        if (c == '+') state = 2;
                        else if (c == '-') state = 4;
                        else if (c == '*') state = 6;
                        else if (c == '/') state = 8;
                        else state = 0;
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // &&,||,~
        public bool MatchLogicOperators(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 5 && state != 9 && state != 11 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        if (c == '&') state = 3;
                        else if (c == '|') state = 7;
                        else if (c == '~') state = 11;
                        else state = 0;
                        i++;
                        break;
                    case 3:
                        state = (c == '&' ? 5 : 0);
                        i++;
                        break;
                    case 7:
                        state = (c == '|' ? 9 : 0);
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            if (state == 11)
                lexeme = lexeme.Substring(0, 1);
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // ==|<|>|!=|<=|>=
        public bool MatchRelationalOperators(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 5 && state != 9 && state != 13 && state != 17 && state != 19 && state != 21 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        if (c == '=') state = 3;
                        else if (c == '!') state = 7;
                        else if (c == '>') state = 11;
                        else if (c == '<') state = 15;
                        else state = 0;
                        i++;
                        break;
                    case 3:
                        state = (c == '=' ? 5 : 0);
                        i++;
                        break;
                    case 7:
                        state = (c == '=' ? 9 : 0);
                        i++;
                        break;
                    case 11:
                        state = (c == '=' ? 13 : 19);
                        i++;
                        break;
                    case 15:
                        state = (c == '=' ? 17 : 21);
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            if (state == 19 || state == 21)
                lexeme = lexeme.Substring(0, 1);
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // =
        public bool MatchAssignmentOperator(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '=' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // ->
        public bool MatchAccessOperator(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 4 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == '-' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == '>' ? 4 : 0);
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // {|}|[|]|(|)
        public bool MatchBraces(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 4 && state != 6 && state != 8 && state != 10 && state != 12 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        if (c == '{') state = 2;
                        else if (c == '[') state = 4;
                        else if (c == '(') state = 6;
                        else if (c == '}') state = 8;
                        else if (c == ']') state = 10;
                        else if (c == ')') state = 12;
                        else state = 0;
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // "|'
        public bool MatchQuotationMark(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 2 && state != 4 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        if (c == '"') state = 2;
                        else if (c == '\'') state = 4;
                        else state = 0;
                        i++;
                        break;
                }
            }
            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // ***
        public bool MatchSingleLineComment(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // </
        public bool MatchCommentStart(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // />
        public bool MatchCommentEnd(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // ;|\n
        public bool MatchLineDelimiter(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if(LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // \t| 
        public bool MatchTokenDelimiter(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

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

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // [a-zA-Z_]+[a-zA-Z_0-9]*
        public bool MatchIdentifier(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            int c;
            while (i < LengthOfKeyword(lexeme) && state != 0)
            {
                c = (int)lexeme[i];
                switch (state)
                {
                    case 1:
                        state = ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_') ? 2 : 0;
                        i++;
                        break;
                    case 2:
                        state = ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || (c >= '0' && c <= '9')) ? 2 : 0;
                        i++;
                        break;
                }
            }

            if (state == 0)
            {
                LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
                result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched, saveResult);
            }
            else
            {
                LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
                result.AddToken(sourceOfCode, lineNum, lexeme, Identifier, Matched, saveResult);
            }
            return true;
        }

        // [0-9]+
        public bool MatchConstant(string sourceOfCode, int lineNum, string lexeme, bool saveResult)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (i < LengthOfKeyword(lexeme) && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c >= '0' && c <= '9' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c >= '0' && c <= '9' ? 2 : 0);
                        i++;
                        break;
                }
            }

            if (state == 0) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, Constant, Matched, saveResult);
            return true;
        }
    }
}
