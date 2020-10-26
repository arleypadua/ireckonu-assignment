using ImportFile.SharedKernel.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImportFile.Tests.Stubs
{
    public class MessageSenderStub : ISendMessages
    {
        public List<ICommand> CommandsSent { get; private set; } = new List<ICommand>();
        public List<IDomainEvent> EventsPublished { get; private set; } = new List<IDomainEvent>();

        public Task SendCommand(ICommand command)
        {
            CommandsSent.Add(command);
            return Task.CompletedTask;
        }

        public Task PublishEvent(IDomainEvent @event)
        {
            EventsPublished.Add(@event);
            return Task.CompletedTask;
        }
    }
}