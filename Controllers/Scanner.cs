using System;
using System.Xml.Serialization;
using CompilerProject3.Controllers.ScannerUtility;
using CompilerProject3.Models;
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

                MatchStartSymbol();
                MatchSingleLineComment();
                MatchCommentStart();

                // Datatypes
                //MatchInteger();
                //MatchSInteger();
                //MatchCharacter();
                //MatchString();
                //MatchFloat();
                //MatchSFloat();
                //MatchVoid();
                //MatchBoolean();

                // ReservedKeywords
                //MatchClass();
                //MatchInheritance();
                MatchCondition();
                //MatchBreak();
                MatchLoop();
                //MatchReturn();
                //MatchStruct();
                MatchSwitch();
                //MatchInclusion();

                // Symbols
                MatchArithmeticOperation();
                MatchLogicOperators();
                MatchRelationalOperators();
                //MatchAssignmentOperator();
                //MatchAccessOperator();
                //MatchBraces();
                //MatchQuotationMark();

                //MatchIdentifier();
                //MatchConstant();

                MatchEndSymbol();
                MatchTokenDelimiter();
                MatchLineDelimiter();

                if(trackChange == i)
                    Increment(1);
            }
        }

        private int LengthToTheNearestDelimiter(int iterator)
        {
            bool IsTokenDelimiter = false;
            bool IsLineDelimiter = false;
            int current_i = iterator;

            while (!IsTokenDelimiter && !IsLineDelimiter)
            {
                IsTokenDelimiter = symbolMatcher.MatchTokenDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(TokenDelimiter)));
                IsLineDelimiter = symbolMatcher.MatchLineDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(LineDelimiter)));

                if (!IsTokenDelimiter && !IsLineDelimiter)
                    iterator++;
            }
            return iterator - current_i;
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

        private void MatchInteger()
        {
            bool IsMatch = dataTypeMatcher.MatchInteger(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Integer)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSInteger()
        {
            bool IsMatch = dataTypeMatcher.MatchSInteger(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(SInteger)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchCharacter()
        {
            bool IsMatch = dataTypeMatcher.MatchCharacter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Character)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchString()
        {
            bool IsMatch = dataTypeMatcher.MatchString(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Keyword.String)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchFloat()
        {
            bool IsMatch = dataTypeMatcher.MatchFloat(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Float)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSFloat()
        {
            bool IsMatch = dataTypeMatcher.MatchSFloat(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(SFloat)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchVoid()
        {
            bool IsMatch = dataTypeMatcher.MatchVoid(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Keyword.Void)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBoolean()
        {
            bool IsMatch = dataTypeMatcher.MatchBoolean(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Keyword.Boolean)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchClass()
        {
            bool IsMatch = reservedKeywordMatcher.MatchClass(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Class)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchInheritance()
        {
            bool IsMatch = reservedKeywordMatcher.MatchInheritance(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Inheritance)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchCondition()
        {
            bool IsMatch = reservedKeywordMatcher.MatchCondition(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Condition)));
            
            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBreak()
        {
            bool IsMatch = reservedKeywordMatcher.MatchBreak(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Break)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchLoop()
        {
            bool IsMatch = reservedKeywordMatcher.MatchLoop(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Loop)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchReturn()
        {
            bool IsMatch = reservedKeywordMatcher.MatchReturn(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Return)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchStruct()
        {
            bool IsMatch = reservedKeywordMatcher.MatchStruct(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Struct)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSwitch()
        {
            bool IsMatch = reservedKeywordMatcher.MatchSwitch(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Switch)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchInclusion()
        {
            bool IsMatch = reservedKeywordMatcher.MatchInclusion(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Inclusion)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchArithmeticOperation()
        {
            bool IsMatch = symbolMatcher.MatchArithmeticOperation(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(ArithmeticOperation)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchLogicOperators()
        {
            bool IsMatch = symbolMatcher.MatchLogicOperators(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(LogicOperators)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchRelationalOperators()
        {
            bool IsMatch = symbolMatcher.MatchRelationalOperators(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(RelationalOperators)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchAssignmentOperator()
        {
            bool IsMatch = symbolMatcher.MatchAssignmentOperator(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(AssignmentOperator)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchAccessOperator()
        {
            bool IsMatch = symbolMatcher.MatchAccessOperator(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(AccessOperator)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBraces()
        {
            bool IsMatch = symbolMatcher.MatchBraces(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Braces)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchQuotationMark()
        {
            bool IsMatch = symbolMatcher.MatchQuotationMark(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(QuotationMark)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchIdentifier()
        {
            bool IsMatch = symbolMatcher.MatchIdentifier(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchConstant()
        {
            bool IsMatch = symbolMatcher.MatchConstant(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)));

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
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
