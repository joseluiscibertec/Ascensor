namespace Ascensor.WebAPI.DTO.Responses
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public T Value { set; get; }
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
        public Response() { }
        public Response(bool _Status) { Status = _Status; }
        public Response(bool _Status, T _Value) { Status = _Status; Value = _Value; }
        public Response(bool _Status, T _Value, string _Message) { Status = _Status; Value = _Value; Message = _Message; }
        public Response(bool _Status, T _Value, string _Message, int? _ErrorCode) { Status = _Status; Value = _Value; Message = _Message; ErrorCode = _ErrorCode; }
    }
}