using System;
using System.Xml.Serialization;
using CompilerProject3.Controllers.ScannerUtility;
using static CompilerProject3.Controllers.ScannerUtility.Utility;
using static CompilerProject3.Models.Keyword;


namespace CompilerProject3.Controllers
{
    public class Scanner
    {
        private IDataTypeMatcher dataTypeMatcher;
        private IReservedKeywordMatcher reservedKeywordMatcher;
        private ISymbolMatcher symbolMatcher;
        private readonly string sourceOfCode;
        private readonly string code;
        private int lengthOfCode;
        private int lineNumber;
        private int i;

        public Scanner(string sourceOfCode, string code)
        {
            this.sourceOfCode = sourceOfCode;
            this.code = code.Substring(0, code.Length-1); // For _ entered before to the end of code to fix ajax request new line deletion
            this.lineNumber = 1;
            this.i = 0;
            this.dataTypeMatcher = new DataTypeMatcher();
            this.reservedKeywordMatcher = new ReservedKeywordMatcher();
            this.symbolMatcher = new SymbolMatcher();

            CalculateMaxLengths();
        }

        public void Increment(int value)
        {
            i += value;
        }

        public void Scan()
        {
            lengthOfCode = GetLength(code);

            while(i < lengthOfCode)
            {
                int trackChange = i;

                // Add calls here:
                MatchStartSymbol();
                MatchSingleLineComment();
                MatchCommentStart();
                

                // ----------------------
                MatchEndSymbol();
                MatchTokenDelimiter();
                MatchLineDelimiter();

                if(trackChange == i)
                    Increment(1);
            }
        }

        private void MatchStartSymbol()
        {
            bool IsMatch = symbolMatcher.MatchStartSymbol(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(StartSymbol)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchEndSymbol()
        {
            bool IsMatch = symbolMatcher.MatchEndSymbol(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(EndSymbol)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }



        private void MatchSingleLineComment()
        {
            if (symbolMatcher.MatchSingleLineComment(sourceOfCode, lineNumber, GetSlice(code, i, i + GetMaxLengthOf(SingleLineComment))))
            {
                Increment(LenOfLastMatchedKeyword);

                while (i < lengthOfCode)
                {
                    if (code[i] != '\n')
                    {
                        Increment(1);
                    }
                    else
                    {
                        lineNumber++;
                        Increment(1);
                        break;
                    }
                }
            }
        }

        private void MatchCommentStart()
        {
            if(symbolMatcher.MatchCommentStart(sourceOfCode, lineNumber, GetSlice(code, i, i + GetMaxLengthOf(CommentStart))))
            {
                Increment(LenOfLastMatchedKeyword);

                int j = i;
                int copyLineNumber = lineNumber;
                while (j < lengthOfCode && !symbolMatcher.MatchCommentEnd(sourceOfCode, lineNumber, GetSlice(code, j, j + GetMaxLengthOf(CommentEnd))))
                {
                    if (code[j] == '\n')
                    {
                        copyLineNumber++;
                    }

                    j++;
                }

                if (j < lengthOfCode)
                {
                    j += LenOfLastMatchedKeyword;
                    Increment(j-i);
                    lineNumber = copyLineNumber;
                }

            }
        }

        private void MatchLineDelimiter()
        {
            bool IsMatch = symbolMatcher.MatchLineDelimiter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(LineDelimiter)));

            if (!IsMatch) return;

            if (GetSlice(code, i, i + GetMaxLengthOf(LineDelimiter)) == "\n")
                lineNumber++;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchTokenDelimiter()
        {
            bool IsMatch = symbolMatcher.MatchTokenDelimiter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(TokenDelimiter)));
            
            if(!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

    }
}
