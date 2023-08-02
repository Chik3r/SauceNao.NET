namespace SauceNao.NET.Exceptions; 

public class UnknownClientError : Exception {
    public UnknownClientError() : base("Unknown client error, status < 0") { }
}