using NetCode.Exceptions;
using System.Text;

namespace NetCode
{
    public class NetCode
    {
        /*
         * Gaein/Aimer Protocol (G/AP)
         * Use "NetCode" to record private IP address and ports(30000 to 36666).
         * NetCode contains five or six characters, the last 2 characters record port.
         * Six characters code means A-class private address, and five means B or C-class.
         * 
         */

        public string IpAddress { get; set; }
        public ushort Port { get; set; }

        public NetCode(string ip, ushort port)
        {
            (IpAddress, Port) = (ip, port);
        }

        public NetCode(string code)
        {
            (IpAddress, Port) = ParseCode(code);
        }

        private (string, ushort) ParseCode(string code)
        {
            if (code is null)
                throw new ArgumentNullException(nameof(code));

            var builder = new StringBuilder(16);

            if (code.Length == 5)
            {
                var netTypeNum = GetCodeCharValue(code[2]);
                builder.Append(netTypeNum > 16 ? $"172.{netTypeNum - 16}." : "192.168.");
            }
            else if (code.Length == 6)
            {
                builder.Append("10.");
            }
            else
            {
                throw new InvaildCodeException(code);
            }

            var groups = ConvertToByBase(GetCodeCharValue(code[0]), 3)
                .Select((x, index) =>
                    $"{x}{ConvertToByBase(GetCodeCharValue(code[index]), 5)}"
                ).ToArray();

            builder.Append(string.Join<string>('.', groups));

            var port = string.Concat(code[..2]
                .Select(x =>
                    ConvertToByBase(GetCodeCharValue(x), 6)
                ).ToArray());

            return (builder.ToString(), (ushort)(30000 + int.Parse(port)));
        }

        private byte GetCodeCharValue(char codeChar)
        {
            var codeCharDecoded = (byte)(int)(codeChar);
            return codeCharDecoded > 0x30 ? (byte)(codeCharDecoded - 55) : codeCharDecoded;
        }

        private string ConvertToByBase(byte number, byte baseNum)
        {
            var builder = new StringBuilder(4);

            while (number != 0)
            {
                builder.Append(number % baseNum);
                number /= baseNum;
            }

            for (var i = builder.Length; builder[i] != '0'; i--)
            {
                builder.Remove(i, 1);
            }

            return builder.ToString();
        }
    }
}