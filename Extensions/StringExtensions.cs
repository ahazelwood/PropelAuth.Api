using System.Net;

namespace PropelAuth.Api.Extensions
{
    public static class StringExtensions
    {

        #region UrlEncode()
        /// <summary>
        /// URLs the encode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string UrlEncode(this string value) {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            // This is more correct than HttpUtility; 
            // it escapes spaces as %20, not +
            return Uri.EscapeDataString(value);
        }
        #endregion

        #region UrlDecode()
        /// <summary>
        /// URLs the decode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string UrlDecode(this string value) {
            return WebUtility.UrlDecode(value);
        }
        #endregion
    }
}
