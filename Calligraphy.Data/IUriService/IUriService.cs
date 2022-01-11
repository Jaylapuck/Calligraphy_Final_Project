using System;
using Calligraphy.Data.Filters;

namespace Calligraphy.Data.IUriService
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}