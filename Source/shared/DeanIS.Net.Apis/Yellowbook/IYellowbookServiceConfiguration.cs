namespace DeanIS.Net.Apis.Yellowbook
{
    public interface IYellowbookServiceConfiguration
    {
        string Username { get; }
        string Password { get; }
        string BasePath { get; }
    }
}