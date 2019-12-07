using ProPri.Core.Domain;
using System;
using System.Collections.Generic;

namespace ProPri.Vocabulary.Domain
{

    public sealed class Classification : Entity
    {
        #region Properties

        public string Name { get; private set; }
        public Guid ParentId { get; private set; }

        public Classification Parent { get; private set; }
        public ICollection<Classification> Children { get; private set; }
        public ICollection<Entry> Entries { get; private set; }

        #endregion

        #region Constructors

        public Classification(Guid parentId, string name)
        {
            ParentId = parentId;
            Name = name;

            Validate();
        }

        #endregion

        #region Entity Methods

        protected override void InitializeCollections()
        {
            Children = new HashSet<Classification>();
            Entries = new HashSet<Entry>();
        }

        public override string ToString()
        {
            return Name;
        }

        protected override void Validate()
        {
        }

        #endregion
    }
}