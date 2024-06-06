using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Messages.Identity
{
	public static class IdentityMessage
	{
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
