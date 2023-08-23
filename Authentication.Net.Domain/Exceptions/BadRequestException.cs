using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Domain.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string error) : base(error)
		{ }
	}
}
