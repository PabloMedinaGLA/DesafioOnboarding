using Avro.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace desafio.Application.Interfaces
{
    public interface IPublisherService<TEvent> where TEvent : class, ISpecificRecord
    {
        public Task Publish(List<TEvent> data, string id, string topico);
        public Task Publish(TEvent @event, string id, string topico);
    }
}
