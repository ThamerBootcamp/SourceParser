using System;
using System.Collections.Generic;

namespace DrwParser
{
    public class DrwParser
    {
      
        public static List<DrwValue> parse(ref List<Token> tokens)
        {
            List<DrwValue> result = new List<DrwValue>();

            while (tokens.Count > 0)
            {

                if (tokens[0].Type == "shape")
                {
                    result.Add(ReadRow(ref tokens));
                }
                else if (tokens[0].Value == "-")
                {
                    tokens.RemoveAt(0);
 
                    result.Add(ReadRow(ref tokens));
                }
                else
                {
                    throw new Exception("Parsing Error: invalid Drw object at line number: " + tokens[0].LineNumber);
                }

            }
            return result;
        }

        public static DrwValue ReadRow(ref List<Token> tokens)
        {
            DrwValue row = new DrwValue();
            
            if (tokens[0].Type == "shape")
            {
                row.Shape = tokens[0].Value;

                tokens.RemoveAt(0);
                for (int i = 0; i < 7; i++)
                {
                    try
                    {
                        if (tokens[0].Type == "number")
                        {
                            row.Values.Add(Convert.ToInt32(tokens[0].Value));
                            tokens.RemoveAt(0);
                        }else if(tokens[0].Value == ",")
                        {
                            tokens.RemoveAt(0);
                        }
                        else
                        {
                            throw new Exception("Parsing Error: missing colon after value, at line number: " + tokens[0].LineNumber);
                        }

                    }
                    catch (Exception)
                    {

                        throw new Exception("Parsing Error, missing value tokens");
                    }
                }

                return row;
            }
            else
            {
                throw new Exception("Parsing Error: Token is not a valid shape, at line number: " + tokens[0].LineNumber);
            }
        }
      
    }

    public class DrwValue
    {
        public string Shape;
        public List<int> Values= new List<int>();

        public string getShape()
        {
            return this.Shape;
        }
        public List<int> getValue()
        {
            return this.Values;
        }
        public string print()
        {
            string res = "drw object: "+this.Shape;
            for (int i = 0; i < 4; i++)
            {
                if(!(i+1==Values.Count))
                    res += " " + Values[i]+",";
                else
                    res += " " + Values[i] ;
            }
            return res;
        }

    }

    
}
