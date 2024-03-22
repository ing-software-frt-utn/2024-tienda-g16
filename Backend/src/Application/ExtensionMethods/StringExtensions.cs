namespace Application.ExtensionMethods;

using System;

public static class StringExtensions
{
    public static Guid ToGuidOrDefault(this string input)
    {
        if (Guid.TryParse(input, out Guid result))
        {
            return result;
        }
        else
        {
            return Guid.Empty;
        }
    }
}



