using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public static class BoolExtensions
{
    public static string ToLower(
        this bool boolValue
    ) => boolValue.ToString(
        CultureInfo.CurrentCulture
    ).ToLower(
        CultureInfo.CurrentCulture
    );
}
