using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Services.Interface
{
    public interface INotificationStrategy
    {
        Task SendNotificationAsync(Notification notification);
    }
}
