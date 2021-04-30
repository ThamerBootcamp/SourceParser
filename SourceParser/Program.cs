using System;
using System.Collections.Generic;

namespace DrwParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new Input(@"line 1,2,3,4 - cir 1,2,3,4");

            Tokenizer t = new Tokenizer(input, new Tokenizable[] {
                new KeywordsTokenizer(new List<string>
                {
                    "cir","rect","line"
                }),
                new NumberTokenizer(),
                new NewLineTokenizer(true),
                new WhiteSpaceTokenizer(true),
                new JSymbolsTokenizer(',',"comma"),
                new JSymbolsTokenizer('-',"hyphen"),
            });

            List<Token> tokens = new List<Token>();

            Token token = t.tokenize();
            while (token != null)
            {
                tokens.Add(token);
                Console.WriteLine(token.Value + " ---> " + token.Type);
                token = t.tokenize();
            }

            /**************  start parser part   ****************/

            List<DrwValue> drws = DrwParser.parse(ref tokens);

            foreach (var item in drws)
            {
                Console.WriteLine(item.print());
            }
        }
    }
}
