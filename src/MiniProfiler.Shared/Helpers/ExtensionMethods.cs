﻿using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StackExchange.Profiling.Helpers
{
    /// <summary>
    /// Common extension methods to use only in this project
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Answers true if this String is either null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Answers true if this String is neither null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Chops off a string at the specified length and accounts for smaller length
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maxLength">The length to truncate to.</param>
        public static string Truncate(this string s, int maxLength) =>
            s?.Length > maxLength ? s.Substring(0, maxLength) : s;

        /// <summary>
        /// Removes trailing / characters from a path and leaves just one
        /// </summary>
        /// <param name="input">The string to ensure a trailing slash on.</param>
        public static string EnsureTrailingSlash(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "/+$", string.Empty) + "/";
        }

        /// <summary>
        /// Converts a List{Guid} into a JSON representation
        /// </summary>
        /// <param name="guids">The guids to convert.</param>
        /// <returns>A JSON representation of the guids.</returns>
        public static string ToJson(this List<Guid> guids)
        {
            if (guids == null || guids.Count == 0)
            {
                return "[]";
            }

            // Yes we're making JSON, but it's *really simple* JSON...don't go serializer crazy here.
            var sb = new StringBuilder("[");
            for (var i = 0; i < guids.Count; i++)
            {
                sb.Append('"').Append(guids[i]).Append('"');
                if (i < guids.Count - 1) sb.Append(',');
            }
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Renders the parameter <see cref="MiniProfiler"/> to JSON.
        /// </summary>
        /// <param name="profiler">The <see cref="MiniProfiler"/> to serialize.</param>
        public static string ToJson(this MiniProfiler profiler) =>
            profiler != null ? JsonConvert.SerializeObject(profiler) : null;

        /// <summary>
        /// Serializes <paramref name="o"/> to a JSON string.
        /// </summary>
        /// <param name="o">the instance to serialise</param>
        /// <returns>the resulting JSON object as a string</returns>
        public static string ToJson(this object o) =>
            o != null ? JsonConvert.SerializeObject(o) : null;

        /// <summary>
        /// Deserializes <paramref name="s"/> to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="s">The string to deserialize</param>
        /// <returns>The object resulting from the given string</returns>
        public static T FromJson<T>(this string s) where T : class =>
            !string.IsNullOrEmpty(s) ? JsonConvert.DeserializeObject<T>(s) : null;
    }
}
