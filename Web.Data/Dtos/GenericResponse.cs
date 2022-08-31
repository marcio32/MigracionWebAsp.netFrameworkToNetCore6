using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Dtos
{
    public class GenericResponse
	{
        public string Message { get; set; }
        public HttpStatusCode HttpCode { get; set; }
		public Stream Data { get; internal set; }
	}
}
