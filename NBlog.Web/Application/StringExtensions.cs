using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBlog.Web.Application
{
    public static class StringExtensions
    {
        /// <summary>
        /// Null if the string is empty, otherwise the original string.
        /// (Useful to use with with null coalesce, e.g. myString.AsNullIfEmpty() ?? defaultString
        /// </summary>
        public static string AsNullIfEmpty(this string items)
        {
            return string.IsNullOrEmpty(items) ? null : items;
        }

        /// <summary>
        /// Null if the string is empty or whitespace, otherwise the original string.
        /// (Useful to use with with null coalesce, e.g. myString.AsNullIfWhiteSpace() ?? defaultString
        /// </summary>
        public static string AsNullIfWhiteSpace(this string items)
        {
            return string.IsNullOrWhiteSpace(items) ? null : items;
        }
    }
}