using System;
using System.Collections.Generic;

namespace Microestc.Authorization
{
    public class LicenceOptions
    {
        private IDictionary<string, LicenceBuilder> Maps { get; }

        public LicenceOptions()
        {
            Maps = new Dictionary<string, LicenceBuilder>(StringComparer.Ordinal);
            SA = new LicenceBuilder(LicenceKeys.SA, "超级管理许可证") { Description = "全站最高级别管理许可证书" };
        }

        internal IEnumerable<LicenceBuilder> LicenceBuilders => Maps.Values;

        internal LicenceBuilder SA { get; }

        public void AddLicence(string licenceKey, string displayName, Action<LicenceBuilder> configureBuilder)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            if (licenceKey == SA.LicenceKey) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            if (Maps.ContainsKey(licenceKey)) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            var builder = new LicenceBuilder(licenceKey, displayName);
            configureBuilder(builder);
            Maps.Add(licenceKey, builder);
        }

        public void AddLicence(string licenceKey, Action<LicenceBuilder> configureBuilder)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            if (licenceKey == SA.LicenceKey) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            if (Maps.ContainsKey(licenceKey)) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            var builder = new LicenceBuilder(licenceKey);
            configureBuilder(builder);
            Maps.Add(licenceKey, builder);
        }

        public void AddLicence(string licenceKey, string displayName = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(licenceKey)) throw new ArgumentNullException(nameof(licenceKey));
            if (licenceKey == SA.LicenceKey) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            if (Maps.ContainsKey(licenceKey)) throw new InvalidOperationException("licenseKey already exists: " + licenceKey);
            var builder = new LicenceBuilder(licenceKey, displayName) { Description = description };
            Maps.Add(licenceKey, builder);
        }
    }
}

