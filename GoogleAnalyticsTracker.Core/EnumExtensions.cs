using System;
using System.Reflection;

namespace GoogleAnalyticsTracker.Core;

public static class EnumExtensions
{
    public static bool IsNullableEnum(this Type t)
    {
        var u = Nullable.GetUnderlyingType(t);
        return u != null && u.GetTypeInfo().IsEnum;
    }
}