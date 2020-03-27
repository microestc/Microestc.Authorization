using System.Collections.Generic;

namespace Microestc.Authorization
{
    public interface ILicenceProvider
    {
        IEnumerable<string> LicenceKeys { get; }
        Licence SA { get; }
        IEnumerable<Licence> Licences { get; }

        bool ContainsKey(string licenceKey);
        Licence FindByLicenceKey(string licenceKey);
    }
}

