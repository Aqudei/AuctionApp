﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Models
{
    abstract class EntityBase : IEquatable<EntityBase>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase);
        }

        public bool Equals(EntityBase other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
        }

        public void NewId()
        {
            Id = Guid.NewGuid();
        }

        public static bool operator ==(EntityBase base1, EntityBase base2)
        {
            return EqualityComparer<EntityBase>.Default.Equals(base1, base2);
        }

        public static bool operator !=(EntityBase base1, EntityBase base2)
        {
            return !(base1 == base2);
        }
    }
}
