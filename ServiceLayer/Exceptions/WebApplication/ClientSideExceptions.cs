using ServiceLayer.Messages.WebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Exceptions.WebApplication
{
	public class ClientSideExceptions : Exception
	{
		public ClientSideExceptions(string? message) : base(message)
		{
		}
	}
}
