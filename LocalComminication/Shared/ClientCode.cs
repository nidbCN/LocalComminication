using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LocalComminication.Shared
{
    public class ClientCode
    {
        public ushort Value { get; set; }

        public ClientCode(string identifier)
        {
        }
    }
}
