using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Microestc.Authorization
{
    public class DefaultLicenceProvider : ILicenceProvider
    {
        private readonly object _lock = new object();
        private readonly Dictionary<string, Licence> _maps = new Dictionary<string, Licence>();
        public Licence SA { get; }
        public IEnumerable<Licence> Licences => _maps.Values;

        public DefaultLicenceProvider(IOptions<LicenceOptions> options)
        {
            var licenceOptions = options.Value;
            SA = licenceOptions.SA.Build();
            foreach (var builder in licenceOptions.LicenceBuilders)
            {
                var licence = builder.Build();
                AddLicence(licence);
            }
        }

        private void AddLicence(Licence license)
        {
            if (_maps.ContainsKey(license.LicenceKey))
                throw new InvalidOperationException("Scheme already exists: " + license.LicenceKey);
            lock (_lock)
            {
                _maps.Add(license.LicenceKey, license);
            }
        }

        public IEnumerable<string> LicenceKeys => _maps.Keys;

        public Licence FindByLicenceKey(string licenceKey) => _maps.TryGetValue(licenceKey, out var licence) ? licence : null;

        public bool ContainsKey(string licenceKey) => _maps.ContainsKey(licenceKey);
    }
}

