using System;
using System.Collections.Generic;
using System.Text;

public static class StringExtensions
{
    public static string Base64Encode(
        this string plainText
    ) => Convert.ToBase64String(
        Encoding.UTF8.GetBytes(
            plainText
        )
    );

    public static string Base64Decode(
        this string base64EncodedData
    ) => Encoding.UTF8.GetString(
        Convert.FromBase64String(
            base64EncodedData
        )
    );
}
