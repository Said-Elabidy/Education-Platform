

namespace Education.Application.RequestModels
{
	public class PaginationFilter
	{
		private const int maxPageSize = 20;

		
		public int pageNumber { get; set; } = 1;

		private int pageSize = 6;

		public int PageSize
		{
			get { return pageSize; }
			set
			{
				if (value < 1)
				{
					pageSize = pageSize;
				}
				else
				{
					pageSize = value > maxPageSize ? pageSize : value;
				}
			}
		}
	}
}
