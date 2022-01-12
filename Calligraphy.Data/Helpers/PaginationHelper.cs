using System;
using System.Collections.Generic;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Repo.Wrappers;

namespace Calligraphy.Data.Helpers
{
    public class PaginationHelper
    {
        public PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData,
            PaginationFilter validFilter, int totalRecords, IUriService.IUriService uriService, string route)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double) totalRecords / (double) validFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                    ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize),
                        route)
                    : null;
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                    ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize),
                        route)
                    : null;
            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            response.LastPage =
                uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }

    }
}