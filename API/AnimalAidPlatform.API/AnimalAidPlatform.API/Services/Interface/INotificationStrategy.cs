namespace AnimalAidPlatform.API.Services.Interface
{
    public interface INotificationStrategy
    {
        Task SendNotificationAsync(string message);
    }
}
