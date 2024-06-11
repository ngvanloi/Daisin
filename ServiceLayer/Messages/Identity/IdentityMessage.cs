namespace ServiceLayer.Messages.Identity
{
	public static class IdentityMessage
	{
		public const string SecurityStampError = "Your critical infomation has been changed. Please try to login again";
		public static string CheckEmailAddress()
		{
			return "Email should be in email format";
		}

		public static string ComparePassword()
		{
			return "Password and Confirm Password must be same with the Password";
		}
	}
}
