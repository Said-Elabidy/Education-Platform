using Education.Application.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Implementations.Abstracts
{
	public interface IUriService
	{
		public Uri GetPagnationUri(int pageNumber, int PageSize, string route);
	}
}
