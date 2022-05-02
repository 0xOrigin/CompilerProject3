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

            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 0 && state != 9)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'S' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'e' ? 3 : 0);
                        i++;
                        break;
                    case 3:
                        state = (c == 'q' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'u' ? 5 : 0);
                        i++;
                        break;
                    case 5:
                        state = (c == 'e' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'n' ? 7 : 0);
                        i++;
                        break;
                    case 7:
                        state = (c == 'c' ? 8 : 0);
                        i++;
                        break;
                    case 8:
                        state = (c == 'e' ? 9 : 0);
                        i++;
                        break;
                }
            }

            if (state != 9) return false;
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
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 0 && state != 6)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'I' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'p' ? 3 : 0);
                        i++;
                        break;
                    case 3:
                        state = (c == 'o' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'k' ? 5 : 0);
                        i++;
                        break;
                    case 5:
                        state = (c == 'f' ? 6 : 0);
                        i++;
                        break;
                }
            }
            if (state != 6) return false;
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
            if (lexeme.Length == 0) return false;

            int state = 1, i = 0;
            char c;
            while (state != 0 && state != 7)
            {
                c = lexeme[i];
                switch (state)
                {
                    case 1:
                        state = (c == 'S' ? 2 : 0);
                        i++;
                        break;
                    case 2:
                        state = (c == 'i' ? 3 : 0);
                        i++;
                        break;
                    case 3:
                        state = (c == 'p' ? 4 : 0);
                        i++;
                        break;
                    case 4:
                        state = (c == 'o' ? 5 : 0);
                        i++;
                        break;
                    case 5:
                        state = (c == 'k' ? 6 : 0);
                        i++;
                        break;
                    case 6:
                        state = (c == 'f' ? 7 : 0);
                        i++;
                        break;
                }
            }
            if (state != 7) return false;
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
            throw new System.NotImplementedException();
        }

        // Rational
        public bool MatchBoolean(string sourceOfCode, int lineNum, string lexeme)
        {
            throw new System.NotImplementedException();
        }
    }
}
