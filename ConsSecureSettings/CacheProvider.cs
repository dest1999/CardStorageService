using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography;

namespace ConsSecureSettings
{
    public class CacheProvider
    {
        private byte[] key = { 4, 1, 4, 2, 8, 7, 9, 5 };
        public void CaheConnections(List<ConnectionString> connections)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(ms, Encoding.UTF8);
            xmlSerializer.Serialize(xmlTextWriter, connections);

            byte[] protectedData = Protect(ms.ToArray());
            string tmp = Convert.ToBase64String(protectedData);
            File.WriteAllText("data.enc", tmp);
        }


        public List<ConnectionString> GetConnectionsFromCache()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
            byte[] protectedData = Convert.FromBase64String(File.ReadAllText("data.enc"));
            byte[] data = Unprotect(protectedData);
            return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
        }
        private byte[] Protect(byte[] data)
        {
            return ProtectedData.Protect(data, key, DataProtectionScope.CurrentUser); // сработает ли CurrentUser на учётке MS?
        }

        private byte[] Unprotect(byte[] data)
        {
            return ProtectedData.Unprotect(data, key, DataProtectionScope.CurrentUser);
        }

    }
}
