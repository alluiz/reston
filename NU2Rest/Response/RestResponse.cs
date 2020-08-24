namespace NU2Rest
{
    public class RestResponse<T>
    {
        public T Data { get; set; }
        public RestResponseMetadata MetaData { get; set; }

        public RestResponse()
        {
            MetaData = new RestResponseMetadata();
        }
    }
}