namespace EducationPlatform.helpers
{
	public class EmailVerificationCode
	{
		private Dictionary<string, EmailHelper> emailVerification
		= new Dictionary<string, EmailHelper>();

		public string GenerateCode(string email)
		{
			Random random = new Random();
			var code = random.Next(10000, 99999).ToString();
			emailVerification[email] = new EmailHelper(code, DateTime.Now.AddMinutes(60));
			return code;
		}

		public bool CheckCode(string email, string code)
		{
			if (emailVerification.TryGetValue(email, out EmailHelper storedData))
			{
				if (DateTime.Now > storedData.ExpiryTime)
				{
					emailVerification.Remove(email);
					return false;
				}
				return code == storedData.Code;
			}
			return false;
		}

	}
}
