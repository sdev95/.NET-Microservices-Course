using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing 
{
    enum EventType 
    {
        PlatformPublished,
        Undetermined
    }

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch(eventType) 
            {
                case EventType.PlatformPublished: 
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage) 
        {
            Console.WriteLine($"--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch(eventType.Event) 
            {
                case "Platform_Published": 
                    Console.WriteLine($"--> Platform Published Event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine($"--> Undetermined Event detected");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {

            // Doordat dit een singleton is moet er op een andere manier DI gedaan worden, middels een scopefactory
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
                try 
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if ( !repo.ExternalPlatformExists(plat.ExternalId) ) 
                    {
                        Console.WriteLine($"--> Platform added!");
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    } 
                    else 
                    {
                        Console.WriteLine($"--> Platform already exists....");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");    
                }

            }
        }
    }

}