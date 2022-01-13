using System;
using System.Collections.Generic;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Repo.Wrappers;

namespace Calligraphy.Data.Helpers
{
    public class PaginationHelper
    {
        private readonly IUriService.IUriService _uriService;
        
        public PaginationHelper(IUriService.IUriService uriService)
        {
            _uriService = uriService;
        }
        
        public PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData,
            PaginationFilter validFilter, int totalRecords, string route)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = (totalRecords / (double) validFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                    ? _uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize),
                        route)
                    : null;
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                    ? _uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize),
                        route)
                    : null;
            response.FirstPage = _uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            response.LastPage =
                _uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }

    }
}