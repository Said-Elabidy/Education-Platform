namespace EducationPlatform.helpers
{
	public class EmailHelper
	{
		public string Code { get; set; }
		public DateTime ExpiryTime { get; set; }

		public EmailHelper(string code, DateTime expiryTime)
		{
			Code = code;
			ExpiryTime = expiryTime;
		}
	}

}
