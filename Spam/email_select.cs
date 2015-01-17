using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spam
{
    class email_select
    {
        public string RemoveCharacters(string texto, string Chars)
        {
            for (int i = 0; i < Chars.Length; i++)
            {
                texto = texto.Replace(Chars.ElementAt(i).ToString(), "\n");
            }
            return texto;
        }

        public string EmailCheck(string texto)
        {
            if (texto.Contains("@") && 
                (texto.EndsWith(".com") || 
                texto.EndsWith(".ru") || 
                texto.EndsWith(".uk") || 
                texto.EndsWith(".edu") || 
                texto.EndsWith(".it") || 
                texto.EndsWith(".net") ||
                texto.EndsWith(".su") ||
                texto.EndsWith(".fr") ||
                texto.EndsWith(".pl") ||
                texto.EndsWith(".ph") ||
                texto.EndsWith(".kz") ||
                texto.EndsWith(".cn") ||
                texto.EndsWith(".jp") || 
                texto.EndsWith(".com.br")))
            {
                return texto;
            }
            return "";
        }

        public int NumberOf(string input, char c)
        {
            int retval = 0;
            for (int i = 0; i < input.Length; i++)
                if (c == input[i])
                    retval++;
            return retval;
        }

        public string RemoveSpaces(string texto)
        {
            while (texto.Contains("\n\n"))
            {
                texto = texto.Replace("\n\n", "\n");
            }
            return texto;
        }
    }
}
