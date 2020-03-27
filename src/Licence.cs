using System;

namespace Microestc.Authorization
{
    public class Licence
    {
        public Licence(string licenceKey, string displayName, string description)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentNullException(nameof(displayName));
            LicenceKey = licenceKey;
            DisplayName = displayName;
            Description = description;
        }

        public virtual string LicenceKey { get; }

        public virtual string DisplayName { get; }

        public virtual string Description { get; }
    }
}

