using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Base
{
	public class PagedResponse<T> : ApiResponse<T> where T : class 
	{
        public int PageNumber { get; set; }	
		public int PageSize { get; set; }	
		public int TotalPages { get; set; }	
		public int TotalRecords { get; set; }
		public Uri FirstPage { get; set; }
		public Uri LastPage { get; set; }
		public Uri? NextPage { get; set; }
		public Uri? PreviousPage { get; set; }

		public PagedResponse(int statusCode, string error) : base(statusCode, error) { }

		public PagedResponse(T data, int pageNumber, int Pagesize)
		{
				this.PageNumber = pageNumber;
			    this.PageSize = Pagesize;	
			    this.Data = data;
			    this.IsSuccess = true;
			    this.Errors = null;
			    this.StatusCode = 200;
		}
	}	
}
