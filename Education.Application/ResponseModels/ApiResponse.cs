using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Base
{
	public class ApiResponse<T> where T : class
	{
		public T Data { get; set; }
		public int StatusCode { get; set; }
		public List<string> Errors { get; set; }
		public bool IsSuccess { get; set; }
		public string? Message { get; set; }	

		public ApiResponse() { } 
		public ApiResponse(int statusCode, T data , string? message)
		{
			StatusCode = statusCode;
			Data = data;
			IsSuccess = true;
			Errors = new List<string>();
			Message = message;
		}
		public ApiResponse(int statusCode, string error, string message=null)
		{
			StatusCode = statusCode;
			IsSuccess = false;
			Message = message;
			Errors = new List<string> { error };
		}

		public ApiResponse<T> NotSuccessed(int statusCode, string error, string message = null)
		{
			return new ApiResponse<T>()
			{
				StatusCode = statusCode,
				IsSuccess = false,
				Message = message,
				Errors = AddError(error)
			};
		}
		public List<string> AddError(string error)
		{
			Errors.Add(error);
			return Errors;
		}

	}
}
