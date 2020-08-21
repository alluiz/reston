namespace NU2Rest
{
    public class NU2RestResponse<T>
    {
        public T Data { get; set; }
        public NU2RestResponseMetadata MetaData { get; set; }

        public NU2RestResponse()
        {
            MetaData = new NU2RestResponseMetadata();
        }
    }
}