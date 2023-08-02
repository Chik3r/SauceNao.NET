namespace SauceNao.NET.Exceptions; 

public class UnknownServerError : Exception {
    public UnknownServerError() : base("Unknown API error, status > 0") { }
}