

namespace Education.Application.Implementations.Abstracts
{
	public interface IUriService
	{
		public Uri GetPagnationUri(int pageNumber, int PageSize, string route);
	}
}
