using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Models.General
{
    public class Error : Exception
    {
        public int status { get; set; }
        public string message { get; set; } = "";
        public string source { get; set; } = "";

        public Error(int status, string message, string source) : base(message)
        {
            this.status = status;
            this.message = message;
            this.source = source;
        }


        public object GetError()
        {
            return new
            {
                status,
                message,
                source
            };
        }
    }
}
