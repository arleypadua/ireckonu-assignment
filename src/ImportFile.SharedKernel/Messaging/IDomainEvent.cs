using System;

namespace ImportFile.SharedKernel.Messaging
{
    public interface IDomainEvent
    {
        DateTime PublishedAtUtc { get; }
    }
}