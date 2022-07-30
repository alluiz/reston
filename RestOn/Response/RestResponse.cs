namespace RestOn
{
    public class RestResponse<T>
    {
        public T Data { get; set; }
        public RestResponseMetadata Metadata { get; set; }

        public RestResponse()
        {
            Metadata = new RestResponseMetadata();
        }
    }
}