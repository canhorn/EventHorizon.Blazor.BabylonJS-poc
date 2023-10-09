using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

public static class StringExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public static string Base64Encode(this string plainText) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    /// <summary>
    ///
    /// </summary>
    /// <param name="base64EncodedData"></param>
    /// <returns></returns>
    public static string Base64Decode(this string base64EncodedData) =>
        Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));

    /// <summary>
    /// Checks if the string is null or empty
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) =>
        string.IsNullOrEmpty(str);

    /// <summary>
    /// Checks if the string is NOT null or empty
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? str) =>
        !string.IsNullOrEmpty(str);

    /// <summary>
    /// Make sure the first character is the upper cased version of the text.
    /// </summary>
    /// <param name="text">Text to upper case first character of.</param>
    /// <returns>The text with the first character uppercased.</returns>
    public static string UppercaseFirstChar(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }
        char[] a = text.ToCharArray();
        a[0] = char.ToUpper(a[0], CultureInfo.CurrentCulture);
        return new string(a);
    }

    /// <summary>
    /// Make sure the first character is the lower cased version of the text.
    /// </summary>
    /// <param name="text">Text to lower case first character of.</param>
    /// <returns>The text with the first character lowercased.</returns>
    public static string LowercaseFirstChar(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }
        char[] a = text.ToCharArray();
        a[0] = char.ToLower(a[0], CultureInfo.CurrentCulture);
        return new string(a);
    }
}
