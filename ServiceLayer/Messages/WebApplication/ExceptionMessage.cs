using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Messages.WebApplication
{
	public static class ExceptionMessage
	{
		public const string ConcurencyException = "Your data has been changed. Please try again later.";
	}
}
