using System;
using System.Text;

public static class StringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public static string Base64Encode(
        this string plainText
    ) => Convert.ToBase64String(
        Encoding.UTF8.GetBytes(
            plainText
        )
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="base64EncodedData"></param>
    /// <returns></returns>
    public static string Base64Decode(
        this string base64EncodedData
    ) => Encoding.UTF8.GetString(
        Convert.FromBase64String(
            base64EncodedData
        )
    );
}
