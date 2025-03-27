using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentageApplication.StandardResponse
{
	public class Response
	{
		public string statusCode { get; set; }

		public string statusMessage { get; set; }

        public List<string> Data { get; set; }
    }

    public enum ResponseStatus
    {
        Success = 0,
        Failure = 1,
        NoRecord = 2,
        EmptyRequest = 3,
        NoDelete = 4,
        NoUpdate = 5,
        ValidationError = 6,
        PartialSuccess = 7

    }


    public static class ResponseHelper
    {
        public static Response CreateResponse(ResponseStatus status, List<string> errors = null)
        {
            return status switch
            {
                ResponseStatus.Success => new Response { statusCode = "00", statusMessage = "Successful" },
                ResponseStatus.Failure => new Response { statusCode = "01", statusMessage = "Failed" },
                ResponseStatus.NoRecord => new Response { statusCode = "02", statusMessage = "No record found" },
                ResponseStatus.NoDelete => new Response { statusCode = "04", statusMessage = "Failed to delete record" },
                ResponseStatus.NoUpdate => new Response { statusCode = "05", statusMessage = "Failed to update record" },
                ResponseStatus.EmptyRequest => new Response { statusCode = "03", statusMessage = "Empty request sent" },
                ResponseStatus.PartialSuccess => new Response { statusCode = "07", statusMessage = "Successful. But failed to save address" },
                ResponseStatus.ValidationError => new Response { statusCode = "06", statusMessage = "Validation failed", Data = errors },
                _ => new Response { statusCode = "99", statusMessage = "Unknown status" }
            };
        }
    }
}

