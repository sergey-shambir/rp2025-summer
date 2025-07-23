using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        // Регулярное выражение для поиска слов:
        // - Слово начинается и заканчивается на букву.
        // - Может содержать апострофы и дефисы внутри.
        // - Не содержит чисел или знаков препинания.
        var pattern = @"\p{L}+(?:[\-\']\p{L}+)*";

        var regex = new Regex(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }
}