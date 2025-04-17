﻿using Education.Application.Implementations.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;


namespace Education.Application.Implementations
{
	public class UriService : IUriService
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		public UriService(IHttpContextAccessor  httpContextAccessor)
		{
				this.httpContextAccessor = httpContextAccessor;	
		}
		public string GetBaseUri()
		{
			var request = httpContextAccessor.HttpContext.Request;
			var Schema = request.Scheme;
			var Host = request.Host.ToUriComponent();
			string BaseUri = string.Concat(Schema, "://", Host);
			return BaseUri;
		}

		public Uri GetPagnationUri(int pageNumber,int PageSize, string route)
		{
			var BaseUri = GetBaseUri();	
			var endPointUri = new Uri(string.Concat(BaseUri, route));

			var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "pageNumber", pageNumber.ToString());
			modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", PageSize.ToString());
			return new Uri(modifiedUri);
		}


	}
}
