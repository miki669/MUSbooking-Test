using Newtonsoft.Json;

namespace MUSbooking.Model;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}public class ValidationErrorDetails : ErrorDetails
{
    public IEnumerable<string> Errors { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}