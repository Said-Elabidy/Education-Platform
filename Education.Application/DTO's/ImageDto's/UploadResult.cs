using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.ImageDto_s
{
	public class UploadResult
	{
		public bool IsUploaded { get; set; }
		public string? ErrorMessage { get; set; }
		public string ImageName { get; set; }
	}
}
