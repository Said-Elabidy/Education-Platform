using Education.Application.Base;
using Education.Application.Implementations.Abstracts;
using Education.Application.RequestModels;


namespace Education.Application.ResponseModels
{
	public static class BuildPagedResponse
	{
		public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(this IEnumerable<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
		{
			var respose = new PagedResponse<IEnumerable<T>>(pagedData, validFilter.pageNumber, validFilter.PageSize);
			var totalPages = ((double)totalRecords / (double)validFilter.PageSize);

			int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
			respose.NextPage =
				validFilter.pageNumber >= 1 && validFilter.pageNumber < roundedTotalPages
				? uriService.GetPagnationUri(validFilter.pageNumber + 1, validFilter.PageSize, route)
				: null;
			respose.PreviousPage =
				validFilter.pageNumber - 1 >= 1 && validFilter.pageNumber <= roundedTotalPages
				? uriService.GetPagnationUri(validFilter.pageNumber - 1, validFilter.PageSize, route)
				: null;

			respose.FirstPage = uriService.GetPagnationUri(1, validFilter.PageSize, route);
			respose.LastPage = uriService.GetPagnationUri(roundedTotalPages, validFilter.PageSize, route);
			respose.TotalPages = roundedTotalPages;
			respose.TotalRecords = totalRecords;
			return respose;
		}
	}
}
