using System;
using System.Collections.Generic;

namespace CompilerProject3.Models
{
    public static class Keyword
    {
        public static int LenOfLastMatchedKeyword = 0;

        public static readonly string Class = "Class";
        public static readonly string Inheritance = "Inheritance";
        public static readonly string Condition = "Condition";
        public static readonly string Integer = "Integer";
        public static readonly string SInteger = "SInteger";
        public static readonly string Character = "Character";
        public static readonly string String = "String";
        public static readonly string Float = "Float";
        public static readonly string SFloat = "SFloat";
        public static readonly string Void = "Void";
        public static readonly string Boolean = "Boolean";
        public static readonly string Break = "Break";
        public static readonly string Loop = "Loop";
        public static readonly string Return = "Return";
        public static readonly string Struct = "Struct";
        public static readonly string Switch = "Switch";
        public static readonly string StartSymbol = "Start Symbol";
        public static readonly string EndSymbol = "End Symbol";
        public static readonly string ArithmeticOperation = "Arithmetic Operation";
        public static readonly string LogicOperators = "Logic operators";
        public static readonly string RelationalOperators = "Relational operators";
        public static readonly string AssignmentOperator = "Assignment operator";
        public static readonly string AccessOperator = "Access Operator";
        public static readonly string Braces = "Braces";
        public static readonly string QuotationMark = "Quotation Mark";
        public static readonly string Inclusion = "Inclusion";
        public static readonly string SingleLineComment = "Single line Comment";
        public static readonly string CommentStart = "Comment start";
        public static readonly string CommentEnd = "Comment end";
        public static readonly string LineDelimiter = "Line delimiter";
        public static readonly string TokenDelimiter = "Token delimiter";
        public static readonly string Constant = "Constant";
        public static readonly string Matched = "Matched";
        public static readonly string NotMatched = "Not Matched";


        private static readonly Dictionary<string, int> MaxLength = new Dictionary<string, int>();

        private static readonly Dictionary<string, string> ReturnToken = new Dictionary<string, string>
        {
            {"Type", Class},
            {"Infer", Inheritance},
            {"If", Condition},
            {"Else", Condition},
            {"Ipok", Integer},
            {"Sipok", SInteger},
            {"Craf", Character},
            {"Sequence", String},
            {"Ipokf", Float},
            {"Sipokf", SFloat},
            {"Valueless", Void},
            {"Rational", Boolean},
            {"Endthis", Break},
            {"However", Loop},
            {"When", Loop},
            {"Respondwith", Return},
            {"Srap", Struct},
            {"Scan", Switch},
            {"Conditionof", Switch},
            {"@", StartSymbol},
            {"^", StartSymbol},
            {"$", EndSymbol},
            {"#", EndSymbol},
            {"+", ArithmeticOperation},
            {"-", ArithmeticOperation},
            {"*", ArithmeticOperation},
            {"/", ArithmeticOperation},
            {"&&", LogicOperators},
            {"||", LogicOperators},
            {"~", LogicOperators},
            {"==", RelationalOperators},
            {"<", RelationalOperators},
            {">", RelationalOperators},
            {"!=", RelationalOperators},
            {"<=", RelationalOperators},
            {">=", RelationalOperators},
            {"=", AssignmentOperator},
            {"->", AccessOperator},
            {"{", Braces},
            {"}", Braces},
            {"[", Braces},
            {"]", Braces},
            {"\"", QuotationMark},
            {"'", QuotationMark},
            {"Require", Inclusion},
            {"***", SingleLineComment},
            {"</", CommentStart},
            {"/>", CommentEnd},
            {";", LineDelimiter},
            {"\n", LineDelimiter},
            {" ", TokenDelimiter},
            {"\t", TokenDelimiter}
        };


        public static void CalculateMaxLengths()
        {
            foreach (KeyValuePair<string, string> record in ReturnToken)
            {
                if (!MaxLength.ContainsKey(record.Value))
                {
                    MaxLength.Add(record.Value, LengthOfKeyword(record.Key));
                }
                else
                {
                    if (LengthOfKeyword(record.Key) > MaxLength[record.Value])
                    {
                        MaxLength[record.Value] = LengthOfKeyword(record.Key);
                    }
                }
            }
        }

        public static string GetReturnToken(string keyword)
        {
            return ReturnToken[keyword];
        }

        public static int GetMaxLengthOf(string type)
        {
            return MaxLength[type];
        }

        public static int LengthOfKeyword(string keyword)
        {
            int length = 0;
            foreach (char c in keyword)
            {
                length++;
            }
            return length;
        }

    }
}
