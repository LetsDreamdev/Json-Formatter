﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Json_Formatter
{
    class Program
    {
        private const string INDENT_STRING = "    ";

        static void Main(string[] args)
        {
            string json = File.ReadAllText(Environment.CurrentDirectory + "\\uglyjson.txt");

            string beautyJson = FormatJson(json);

            File.WriteAllText(Environment.CurrentDirectory + "\\json.txt", beautyJson, Encoding.UTF8);

            Console.WriteLine(beautyJson);
            Console.ReadLine();
        }

        static string FormatJson(string json)
        {

            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return String.Concat(result);
        }
    }
}
