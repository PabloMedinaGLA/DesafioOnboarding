using desafio.Domain.Common;
using System.Threading.Tasks;

namespace desafio.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
