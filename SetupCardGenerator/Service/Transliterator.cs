using System;
using System.Collections.Generic;

namespace SetupCardGenerator.Service
{
    public static class Transliterator
    {
        private static string FromCyrrilToLatin(string text)
        {
            Dictionary<char, string> d = new Dictionary<char, string>()
            {
                {' ', " "},
                {'а', "a"},
                {'б', "b"},
                {'в', "v"},
                {'г', "g"},
                {'д', "d"},
                {'е', "e"},
                {'ё', "jo"},
                {'ж', "zh"},
                {'з', "z"},
                {'и', "i"},
                {'й', "j"},
                {'к', "k"},
                {'л', "l"},
                {'м', "m"},
                {'н', "n"},
                {'о', "o"},
                {'п', "p"},
                {'р', "r"},
                {'с', "s"},
                {'т', "t"},
                {'у', "u"},
                {'ф', "f"},
                {'х', "h"},
                {'ц', "c"},
                {'ч', "ch"},
                {'ш', "sh"},
                {'щ', "sch"},
                {'ъ', "\""},
                {'ы', "y"},
                {'ь', "'"},
                {'э', "e"},
                {'ю', "ju"},
                {'я', "ja"}
            };
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("Ошибка транслитерации");
            }
            string result = "";
            foreach (char c in text.ToLower())
            {
                if (d.TryGetValue(c, out var t))
                    result += t;
                else
                    result += c;
            }
            return result.ToUpper();
        }
    }
}
