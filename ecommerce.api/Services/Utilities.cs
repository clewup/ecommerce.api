namespace ecommerce.api.Services;

public static class Utilities
{
    public static string Abbreviate(this string input)
    {
        string abbr = "";
        int i = 0;
        abbr += input[0];
  
        for(i = 0; i < input.Length - 1; i++)
        {
            if (input[i] == ' ' || input[i] == '\t' || 
                input[i] == '\n')
            {
                abbr += input[i + 1];
            }
        }
        return abbr;
    }

    public static string RemoveWhitespace(this string input)
    {
        return String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
    }
}