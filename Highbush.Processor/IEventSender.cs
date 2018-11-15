using System.Threading.Tasks;
using HighbushAutomation.Data;

namespace Highbush.Processor
{
    public interface IEventSender
    {
        Task SendAsync(DoorEvent doorEvent, string topicName, string topicKey);
    }
}