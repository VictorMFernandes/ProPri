using System;
using System.Collections.Generic;
using Rise.Core.Communication.Messages;

namespace Rise.Core.Domain
{
    public abstract class Entity : IEventContainer
    {
        public Guid Id { get; }
        public DateTime RegistrationDate { get; }
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
            InitializeCollections();
        }

        #region EventContainer Methods

        public void AddEvent(Event eventItem)
        {
            _notifications ??= new List<Event>();
            _notifications.Add(eventItem);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }

        #endregion

        public override bool Equals(object obj)
        {
            var objEntidade = obj as Entity;

            if (ReferenceEquals(this, objEntidade)) return true;
            return !(objEntidade is null) && Id.Equals(objEntidade.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        protected abstract void InitializeCollections();

        protected abstract void Validate();

        public abstract override string ToString();
    }
}