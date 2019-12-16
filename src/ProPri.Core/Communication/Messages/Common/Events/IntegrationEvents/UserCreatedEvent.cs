using System;

namespace ProPri.Core.Communication.Messages.Common.Events.IntegrationEvents
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public Guid CreatedUserId { get; }
        public string CreatedUserName { get; }
        public string CreatedUserEmail { get; }
        public string CreatedUserPassword { get; }

        public UserCreatedEvent(Guid createdUserId, string createdUserName, string createdUserEmail, string createdUserPassword)
        {
            AggregateId = createdUserId;
            CreatedUserId = createdUserId;
            CreatedUserName = createdUserName;
            CreatedUserEmail = createdUserEmail;
            CreatedUserPassword = createdUserPassword;
        }
    }
}