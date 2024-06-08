namespace TodoApi.Models.General
{
    public class Response
    {
        public int status { get; set; }
        public string message { get; set; } = "";

        public object? data { get; set; }

        public Response(int status, string message, object data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }

        public Response(int status, string message)
        {
            this.status = status;
            this.message = message;
        }
    }
}
