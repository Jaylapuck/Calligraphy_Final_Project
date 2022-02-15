using System;
#if NET || NETCOREAPP
using Microsoft.AspNetCore.Http;

#else
using System.Web;
#endif

namespace Calligraphy
{
#if NET || NETCOREAPP
#endif
    internal static class HttpContextHelper
    {
        private const string Message = "Prefer accessing HttpContext via injection";

        /// <summary>
        ///     Gets the current <see cref="HttpContext" />. Returns <c>null</c> if there is no current <see cref="HttpContext" />.
        /// </summary>
#if NET || NETCOREAPP
#if NET5_0_OR_GREATER
        [Obsolete(Message, false, DiagnosticId = "HttpContextCurrent",
            UrlFormat = "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context")]
#else
        [Obsolete(Message, error: false)]
#endif
        public static HttpContext Current => HttpContextAccessor.HttpContext;

        private static readonly HttpContextAccessor HttpContextAccessor = new();
#else
#pragma warning disable UA0005 // Do not use HttpContext.Current
        [Obsolete(Message, error: false)]
        public static HttpContext Current => HttpContext.Current;
#pragma warning restore UA0005 // Do not use HttpContext.Current
#endif
    }
}