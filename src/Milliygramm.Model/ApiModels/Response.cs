namespace Milliygramm.Model.ApiModels;

public class Response
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
}