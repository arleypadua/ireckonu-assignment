using MediatR;
using System;

namespace ImportFile.SharedKernel.Messaging
{
    public interface IDomainEvent : INotification
    {
        DateTime PublishedAtUtc { get; }
    }
}