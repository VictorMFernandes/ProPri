using Rise.Core.Domain.ValueObjects;
using System;

namespace Rise.Core.Communication.Messages.Common.Events.IntegrationEvents
{
    public class UserCreationFailedEvent : IntegrationEvent
    {
        public string CreatedUserName { get; }
        public Image CreatedUserImage { get; }

        public UserCreationFailedEvent(Guid performingUserId, string createdUserName, Image createdUserImage)
        {
            AggregateId = performingUserId;
            CreatedUserName = createdUserName;
            CreatedUserImage = createdUserImage;
        }
    }
}