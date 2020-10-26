using MediatR;
using System.Threading.Tasks;

namespace ImportFile.SharedKernel.Messaging.Mediatr
{
    public class MediatrMessageSender : ISendMessages
    {
        private readonly IMediator _mediator;

        public MediatrMessageSender(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand(ICommand command)
        {
            // ideally this would send a message in a queue
            return _mediator.Send(command);
        }

        public Task PublishEvent(IDomainEvent @event)
        {
            // ideally this would publish an event in a topic
            return _mediator.Publish(@event);
        }
    }
}