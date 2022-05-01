using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class ReservedKeywordMatcher : IReservedKeywordMatcher
    {
        private ScannerResult result = ScannerResult.GetInstance();
        
        // Type
        public bool MatchClass(string sourceOfCode, int lineNum, string lexeme)
        {
            
            throw new System.NotImplementedException();
        }

        // Infer
        public bool MatchInheritance(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // If|Else
        public bool MatchCondition(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 4 && state != 12 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'I' ? 2 : (c == 'E' ? 6 : 0));
                        i++;
                        break;
                    case 2:
                        state = (c == 'f' ? 4 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'l' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 's' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'e' ? 12 : 0);
                        i++;
                        break;
                }
            }
            
            if (state != 4 && state != 12) return false;
            if (state == 4)
                lexeme = lexeme.Substring(0, 2);
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Endthis
        public bool MatchBreak(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // However|When
        public bool MatchLoop(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 8 && state != 22 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'W' ? 2 : (c == 'H' ? 10 : 0));
                        i++;
                        break;
                    case 2:
                        state = (c == 'h' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'e' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'n' ? 8 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'o' ? 12 : 0);
                        i++;
                        break;
                    case 12:
                        state = (c == 'w' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 'e' ? 16 : 0);
                        i++;
                        break;
                    case 16:
                        state = (c == 'v' ? 18 : 0);
                        i++;
                        break;
                    case 18:
                        state = (c == 'e' ? 20 : 0);
                        i++;
                        break;
                    case 20:
                        state = (c == 'r' ? 22 : 0);
                        i++;
                        break;
                }
            }

            if (state != 8 && state != 22) return false;
            if (state == 8)
                lexeme = lexeme.Substring(0, 4);
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Respondwith
        public bool MatchReturn(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 22 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'R' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'e' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 's' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'p' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'o' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'n' ? 12 : 0);
                        i++;
                        break;
                    case 12:
                        state = (c == 'd' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 'w' ? 16 : 0);
                        i++;
                        break;
                    case 16:
                        state = (c == 'i' ? 18 : 0);
                        i++;
                        break;
                    case 18:
                        state = (c == 't' ? 20 : 0);
                        i++;
                        break;
                    case 20:
                        state = (c == 'h' ? 22 : 0);
                        i++;
                        break;
                }
            }

            if (state != 22) return false;
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Srap
        public bool MatchStruct(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Scan|Conditionof
        public bool MatchSwitch(string sourceOfCode, int lineNum, string lexeme)
        {
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 22 && state != 30 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'C' ? 2 : (c == 'S' ? 24 : 0));
                        i++;
                        break;
                    case 2:
                        state = (c == 'o' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'n' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'd' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'i' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 't' ? 12 : 0);
                        i++;
                        break;
                    case 12:
                        state = (c == 'i' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 'o' ? 16 : 0);
                        i++;
                        break;
                    case 16:
                        state = (c == 'n' ? 18 : 0);
                        i++;
                        break;
                    case 18:
                        state = (c == 'o' ? 20 : 0);
                        i++;
                        break;
                    case 20:
                        state = (c == 'f' ? 22 : 0);
                        i++;
                        break;
                    case 24:
                        state = (c == 'c' ? 26 : 0);
                        i++;
                        break;
                    case 26:
                        state = (c == 'a' ? 28 : 0);
                        i++;
                        break;
                    case 28:
                        state = (c == 'n' ? 30 : 0);
                        i++;
                        break;
                }
            }

            if (state != 22 && state != 30) return false;
            if (state == 30)
                lexeme = lexeme.Substring(0, 4);
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Require
        public bool MatchInclusion(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }
    }
}
