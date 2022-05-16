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
            this.code = RemoveChar(RemoveLastChar(code), '\r'); // For _ entered before to the end of code to fix ajax request new line deletion
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
                MatchInteger();
                MatchSInteger();
                MatchCharacter();
                MatchString();
                MatchFloat();
                MatchSFloat();
                MatchVoid();
                MatchBoolean();

                // ReservedKeywords
                MatchClass();
                MatchInheritance();
                MatchCondition();
                MatchBreak();
                MatchLoop();
                MatchReturn();
                MatchStruct();
                MatchSwitch();
                MatchInclusion();

                // Symbols
                MatchAccessOperator();
                MatchArithmeticOperation();
                MatchLogicOperators();
                MatchRelationalOperators();
                MatchAssignmentOperator();
                MatchBraces();
                MatchQuotationMark();

                MatchConstant();
                MatchIdentifier();

                MatchEndSymbol();
                MatchTokenDelimiter();
                MatchLineDelimiter();

                if(trackChange == i)
                    Increment(1);
            }
        }

        private int LengthToTheNearestDelimiter(int iterator)
        {
            bool MatchDelimiter = false;
            bool IsEndOfCode = false;
            int current_i = iterator;
            int lengthToNearestDelimiter = 0;

            while (!MatchDelimiter)
            {
                bool IsTokenDelimiter = symbolMatcher.MatchTokenDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(TokenDelimiter)), false);
                bool IsLineDelimiter = symbolMatcher.MatchLineDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(LineDelimiter)), false);

                MatchDelimiter = IsTokenDelimiter || IsLineDelimiter;

                if (!MatchDelimiter)
                    iterator++;

                if (iterator >= lengthOfCode)
                {
                    IsEndOfCode = true;
                    break;
                }
            }

            lengthToNearestDelimiter = iterator - current_i;

            if (IsEndOfCode)
            {
                lengthToNearestDelimiter = lengthOfCode - current_i;
            }

            return lengthToNearestDelimiter;
        }

        private int LengthToTheNearestSymbol(int iterator)
        {
            bool MatchDelimiter = false;
            bool IsEndOfCode = false;
            int current_i = iterator;
            int lengthToNearestDelimiter = 0;

            while (!MatchDelimiter)
            {
                bool IsTokenDelimiter = symbolMatcher.MatchTokenDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(TokenDelimiter)), false);

                bool IsLineDelimiter = symbolMatcher.MatchLineDelimiter(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(LineDelimiter)), false);
                
                bool IsAssignmentOperator = symbolMatcher.MatchAssignmentOperator(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(AssignmentOperator)), false);
                
                bool IsArithmeticOperation = symbolMatcher.MatchArithmeticOperation(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(ArithmeticOperation)), false);
                
                bool IsRelationalOperators = symbolMatcher.MatchRelationalOperators(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(RelationalOperators)), false);
                
                bool IsLogicOperators = symbolMatcher.MatchLogicOperators(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(LogicOperators)), false);
                
                bool IsAccessOperator = symbolMatcher.MatchAccessOperator(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(AccessOperator)), false);
                
                bool IsStartSymbol = symbolMatcher.MatchStartSymbol(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(StartSymbol)), false);
                
                bool IsEndSymbol = symbolMatcher.MatchEndSymbol(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(EndSymbol)), false);
                
                bool IsBraces = symbolMatcher.MatchBraces(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(Braces)), false);
                
                bool IsSingleLineComment = symbolMatcher.MatchSingleLineComment(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(SingleLineComment)), false);
                
                bool IsCommentStart = symbolMatcher.MatchCommentStart(sourceOfCode, lineNumber,
                    GetSlice(code, iterator, iterator + GetMaxLengthOf(CommentStart)), false);

                MatchDelimiter = IsTokenDelimiter || IsLineDelimiter || IsArithmeticOperation || IsAccessOperator || IsRelationalOperators
                    || IsLogicOperators || IsAssignmentOperator || IsBraces || IsSingleLineComment || IsCommentStart || IsStartSymbol
                    || IsEndSymbol;

                if (!MatchDelimiter)
                    iterator++;

                if (iterator >= lengthOfCode)
                {
                    IsEndOfCode = true;
                    break;
                }
            }

            lengthToNearestDelimiter = iterator - current_i;

            if (IsEndOfCode)
            {
                lengthToNearestDelimiter = lengthOfCode - current_i;
            }

            return lengthToNearestDelimiter;
        }

        private void MatchStartSymbol()
        {
            bool IsMatch = symbolMatcher.MatchStartSymbol(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(StartSymbol)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchEndSymbol()
        {
            bool IsMatch = symbolMatcher.MatchEndSymbol(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(EndSymbol)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSingleLineComment()
        {
            bool IsMatch = symbolMatcher.MatchSingleLineComment(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(SingleLineComment)), true);

            if (IsMatch)
            {
                Increment(LenOfLastMatchedKeyword);

                while (i < lengthOfCode)
                {
                    if (code[i] == '\n')
                    {
                        lineNumber++;
                        Increment(1);
                        break;
                    }
                    
                    Increment(1);
                }
            }
        }

        private void MatchCommentStart()
        {
            bool IsMatch = symbolMatcher.MatchCommentStart(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(CommentStart)), true);

            if (IsMatch)
            {
                Increment(LenOfLastMatchedKeyword);

                int j = i;
                int copyLineNumber = lineNumber;
                while (j < lengthOfCode && !symbolMatcher.MatchCommentEnd(sourceOfCode, copyLineNumber, GetSlice(code, j, j + GetMaxLengthOf(CommentEnd)), true))
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
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSInteger()
        {
            bool IsMatch = dataTypeMatcher.MatchSInteger(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchCharacter()
        {
            bool IsMatch = dataTypeMatcher.MatchCharacter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchString()
        {
            bool IsMatch = dataTypeMatcher.MatchString(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchFloat()
        {
            bool IsMatch = dataTypeMatcher.MatchFloat(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSFloat()
        {
            bool IsMatch = dataTypeMatcher.MatchSFloat(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchVoid()
        {
            bool IsMatch = dataTypeMatcher.MatchVoid(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBoolean()
        {
            bool IsMatch = dataTypeMatcher.MatchBoolean(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchClass()
        {
            bool IsMatch = reservedKeywordMatcher.MatchClass(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchInheritance()
        {
            bool IsMatch = reservedKeywordMatcher.MatchInheritance(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchCondition()
        {
            bool IsMatch = reservedKeywordMatcher.MatchCondition(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);
            
            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBreak()
        {
            bool IsMatch = reservedKeywordMatcher.MatchBreak(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchLoop()
        {
            bool IsMatch = reservedKeywordMatcher.MatchLoop(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchReturn()
        {
            bool IsMatch = reservedKeywordMatcher.MatchReturn(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchStruct()
        {
            bool IsMatch = reservedKeywordMatcher.MatchStruct(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestDelimiter(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchSwitch()
        {
            bool IsMatch = reservedKeywordMatcher.MatchSwitch(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchInclusion()
        {
            bool IsMatch = reservedKeywordMatcher.MatchInclusion(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchArithmeticOperation()
        {
            bool IsMatch = symbolMatcher.MatchArithmeticOperation(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(ArithmeticOperation)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchLogicOperators()
        {
            bool IsMatch = symbolMatcher.MatchLogicOperators(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(LogicOperators)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchRelationalOperators()
        {
            bool IsMatch = symbolMatcher.MatchRelationalOperators(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(RelationalOperators)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchAssignmentOperator()
        {
            bool IsMatch = symbolMatcher.MatchAssignmentOperator(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(AssignmentOperator)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchAccessOperator()
        {
            bool IsMatch = symbolMatcher.MatchAccessOperator(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(AccessOperator)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchBraces()
        {
            bool IsMatch = symbolMatcher.MatchBraces(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(Braces)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchQuotationMark()
        {
            bool IsMatch = symbolMatcher.MatchQuotationMark(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(QuotationMark)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }
        
        private void MatchIdentifier()
        {
            bool IsMatch = symbolMatcher.MatchIdentifier(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchConstant()
        {
            bool IsMatch = symbolMatcher.MatchConstant(sourceOfCode, lineNumber,
                GetSlice(code, i, i + LengthToTheNearestSymbol(i)), true);

            if (!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchLineDelimiter()
        {
            bool IsMatch = symbolMatcher.MatchLineDelimiter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(LineDelimiter)), false);

            if (!IsMatch) return;

            if (GetSlice(code, i, i + GetMaxLengthOf(LineDelimiter)) == "\n")
                lineNumber++;

            Increment(LenOfLastMatchedKeyword);
        }

        private void MatchTokenDelimiter()
        {
            bool IsMatch = symbolMatcher.MatchTokenDelimiter(sourceOfCode, lineNumber,
                GetSlice(code, i, i + GetMaxLengthOf(TokenDelimiter)), false);
            
            if(!IsMatch) return;

            Increment(LenOfLastMatchedKeyword);
        }

    }
}
