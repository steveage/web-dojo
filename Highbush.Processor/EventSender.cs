using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HighbushAutomation.Data;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace Highbush.Processor
{
    public class EventSender : IEventSender
    {
        public async Task SendAsync(DoorEvent doorEvent, string topicName, string topicKey)
        {
            TopicCredentials credentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(credentials);
            EventGridEvent gridEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                Subject = "Test subject 1",
                EventType = "IOT event",
                EventTime = DateTime.UtcNow,
                Data = doorEvent,
                DataVersion = "1.0.0"
            };

            List<EventGridEvent> events = new List<EventGridEvent>
            {
                gridEvent
            };

            await client.PublishEventsWithHttpMessagesAsync(topicName, events);
        }
    }
}