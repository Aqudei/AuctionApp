using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;

namespace Auction.Events
{
    enum EventType
    {
        Create, Remove, Update
    }

    class CrudEvent<T> where T : EntityBase
    {
        public EventType EventType { get; set; }
        public T Entity { get; set; }

        public CrudEvent(EventType eventType, T entity)
        {
            EventType = eventType;
            Entity = entity;
        }
    }
}
