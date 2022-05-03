using CompilerProject3.Models;
using static CompilerProject3.Models.Keyword;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public class DataTypeMatcher : IDataTypeMatcher
    {
        private ScannerResult result = ScannerResult.GetInstance();

        // Ipok
        public bool MatchInteger(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Sipok
        public bool MatchSInteger(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Craf
        public bool MatchCharacter(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }

        // Sequence
        public bool MatchString(string sourceOfCode, int lineNum, string lexeme)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 16 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'S' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'e' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'q' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'u' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'e' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'n' ? 12 : 0);
                        i++;
                        break;
                    case 12:
                        state = (c == 'c' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 'e' ? 16 : 0);
                        i++;
                        break;
                }
            }

            if (state != 16) return false;
            if (LengthOfKeyword(lexeme) > i)
            {
                //result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched);
                return false;
            }
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;

        }
        // Ipokf
        public bool MatchFloat(string sourceOfCode, int lineNum, string lexeme)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 10 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'I' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'p' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'o' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'k' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'f' ? 10 : 0);
                        i++;
                        break;
                }
            }
            
            if (state != 10) return false;
            if (LengthOfKeyword(lexeme) > i)
            {
                //result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched);
                return false;
            }
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Sipokf
        public bool MatchSFloat(string sourceOfCode, int lineNum, string lexeme)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 12 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'S' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'i' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'p' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'o' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'k' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'f' ? 12 : 0);
                        i++;
                        break;
                }
            }
            
            if (state != 12) return false;
            if (LengthOfKeyword(lexeme) > i)
            {
                //result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched);
                return false;
            }
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched);
            return true;
        }

        // Valueless
        public bool MatchVoid(string sourceOfCode, int lineNum, string lexeme)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 18 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'V' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'a' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'l' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'u' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'e' ? 10 : 0);
                        i++;
                        break;
                    case 10:
                        state = (c == 'l' ? 12 : 0);
                        i++;
                        break;
                    case 12:
                        state = (c == 'e' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 's' ? 16 : 0);
                        i++;
                        break;
                    case 16:
                        state = (c == 's' ? 18 : 0);
                        i++;
                        break;
                }
            }

            if (state != 18) return false;
            if (LengthOfKeyword(lexeme) > i)
            {
                //result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched, saveResult);
                return false;
            }
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }

        // Rational
        public bool MatchBoolean(string sourceOfCode, int lineNum, string lexeme)
        {
            if (LengthOfKeyword(lexeme) == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 16 && state != 0)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'R' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'a' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 't' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'i' ? 8 : 0);
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
                        state = (c == 'a' ? 14 : 0);
                        i++;
                        break;
                    case 14:
                        state = (c == 'l' ? 16 : 0);
                        i++;
                        break;
                }
            }

            if (state != 16) return false;
            if (LengthOfKeyword(lexeme) > i)
            {
                //result.AddToken(sourceOfCode, lineNum, lexeme, "", NotMatched, saveResult);
                return false;
            }
            LenOfLastMatchedKeyword = LengthOfKeyword(lexeme);
            result.AddToken(sourceOfCode, lineNum, lexeme, GetReturnToken(lexeme), Matched, saveResult);
            return true;
        }
    }
}
