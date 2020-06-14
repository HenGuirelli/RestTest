using System;
using System.Globalization;

namespace RestTest.Library.Config
{
    public static class ExtensionMethos
    {
        public static string Parse(this Method method)
        {
            return method.ToString().ToUpper();
        }

        public static Method ToMethod(this string str)
        {
            return ToEnum<Method>(str);
        }

        public static string ToCapitalize(this string str)
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str);
        }

        public static string Parse(this TestType testType)
        {
            return testType.ToString().ToUpper();
        }

        public static TestType ToTestType(this string str)
        {
            return ToEnum<TestType>(str);
        }

        private static T ToEnum<T>(string str) where T : struct
        {
            if (str is null || string.IsNullOrWhiteSpace(str)) return default;

            Enum.TryParse<T>(str, ignoreCase: true, out var result);
            return result;
        }
    }
}
