using System;

namespace Microestc.Authorization
{
    public class LicenceBuilder
    {
        public LicenceBuilder(string licenceKey, string displayName = null)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            LicenceKey = licenceKey;
            DisplayName = displayName ?? licenceKey;
        }

        public string LicenceKey { get; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Licence Build()
        {
            return new Licence(this.LicenceKey, this.DisplayName, this.Description);
        }
    }
}

