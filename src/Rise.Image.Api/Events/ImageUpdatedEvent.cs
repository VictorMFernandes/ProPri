using Rise.Core.Communication.Messages;
using System;

namespace Rise.ImageUpload.Api.Events
{
    public class ImageUpdatedEvent:Event
    {
        public string OldImagePublicId { get; }

        public ImageUpdatedEvent(Guid aggregateId, string oldImagePublicId)
        {
            AggregateId = aggregateId;
            OldImagePublicId = oldImagePublicId;
        }
    }
}