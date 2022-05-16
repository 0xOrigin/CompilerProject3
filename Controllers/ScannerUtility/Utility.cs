using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CompilerProject3.Controllers.ScannerUtility
{
    public static class Utility
    {
        public static int GetLength(string code)
        {
            int counter = 0;
            foreach (var c in code)
            {
                counter++;
            }
            return counter;
        }

        public static string GetSlice(string code, int startIndex, int endIndex)
        {
            int lengthOfCode = GetLength(code); 
            string result = "";

            if (!IsValidIndex(endIndex, lengthOfCode))
            {
                endIndex = lengthOfCode;
            }

            for (int i = startIndex; i < endIndex; i++)
            {
                result += code[i];
            }
            
            return result;
        }

        
        public static string RemoveLastChar(string code)
        {
            string result = "";

            for (int i = 0; i < GetLength(code) - 1; i++)
            {
                result += code[i];
            }

            return result;
        }

        private static bool IsValidIndex(int endIndex, int lengthOfCode)
        {
            if (endIndex <= lengthOfCode) return true;
            // Console.WriteLine("Empty string.");
            return false;
        }

    }
}
