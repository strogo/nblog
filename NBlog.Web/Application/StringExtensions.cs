﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBlog.Web.Application
{
    public static class StringExtensions
    {
        public static string AsNullIfEmpty(this string items)
        {
            if (String.IsNullOrEmpty(items))
            {
                return null;
            }
            return items;
        }

        public static string AsNullIfWhiteSpace(this string items)
        {
            if (String.IsNullOrWhiteSpace(items))
            {
                return null;
            }
            return items;
        }
    }
}