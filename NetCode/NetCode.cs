﻿using NetworkInfomation.Exceptions;
using System.Text;

namespace NetworkInfomation
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

        public string IpAddress { get; private set; }
        public ushort Port { get; private set; }

        public NetCode(string ip, ushort port)
        {
            (IpAddress, Port) = (ip, port);
        }

        public static NetCode Decode(string code)
        {
            var (ip, port) = ParseCode(code);
            return new NetCode(ip, port);
        }

        private static (string, ushort) ParseCode(string code)
        {
            if (code is null)
                throw new ArgumentNullException(nameof(code));

            var builder = new StringBuilder(16);

            if (code.Length == 5)
            {
                var netTypeNum = CodeCharToDec(code[2]);
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

            // Get first dig for each group, and append others digs
            var firstDig = ConvertToRadit(CodeCharToDec(code[0]), 3).ToString("D3");

            var groups = firstDig
                .Select((x, index) =>
                {
                    var t = int.Parse($"{x}{CodeCharToDec(code[index + 1])}").ToString();
                    return t;
                }).ToArray();

            builder.Append(string.Join<string>('.', groups));

            var port = string.Concat(code[..2]
                .Select(x =>
                    ConvertToRadit(CodeCharToDec(x), 6)
                ).ToArray());

            return (builder.ToString(), (ushort)(30000 + int.Parse(port)));
        }

        public string Encode()
        {
            var ipGroups = IpAddress.Split('.');
            var ipGroupsQuery = ipGroups.Select(x => x.PadLeft(3, '0')).Skip(1);

            var builder = new StringBuilder(8);

            if (ipGroups[0] == "10")
            {
                // Get first for each dig, and convert into 3-base number.
                var firstDig = DecToCodeChar(ConvertToDec(
                     int.Parse(string.Concat(
                         ipGroupsQuery.Select(x => x[0]))
                     ), 3));

                builder.Append(firstDig);
            }
            else if (ipGroups[0] == "172")
            {
                // Get first for each dig, and convert into 3-base number.
                builder.Append(DecToCodeChar(ConvertToDec(
                     int.Parse(string.Concat(
                         ipGroupsQuery.Select(x => x[0]))
                     ) + 16, 3)));
            }
            else if (ipGroups[0] == "192")
            {
                ipGroupsQuery = ipGroupsQuery.Skip(1);

                // Get first for each dig, and convert into 3-base number.
                builder.Append(DecToCodeChar(ConvertToDec(
                     int.Parse(string.Concat(
                         ipGroupsQuery.Select(x => x[0]))
                     ), 3)));
            }

            // Get last two dig for each group, convert them to code char and concat to a string.
            var digList = ipGroupsQuery.Select(x =>
                    DecToCodeChar(int.Parse(x[1..])));

            builder.Append(string.Concat(digList));

            var portStr = (Port - 30000).ToString("D4");

            var t = int.Parse(portStr[^2..]);

            builder.Append(DecToCodeChar(ConvertToDec(int.Parse(portStr[..2]), 7)));
            builder.Append(DecToCodeChar(ConvertToDec(int.Parse(portStr[^2..]), 7)));

            return builder.ToString();
        }

        private static int CodeCharToDec(char codeChar)
        {
            if (codeChar > '9')
            {
                if (codeChar > 'a')
                    codeChar -= (char)61;
                else
                    codeChar -= (char)55;
            }
            else
            {
                codeChar -= '0';
            }

            return codeChar;
        }
        private static char DecToCodeChar(int number)
        {
            if (number > 9)
            {
                if (number > 36)
                    number += 61;
                else
                    number += 55;
            }
            else
            {
                number += '0';
            }

            return (char)number;
        }


        /// <summary>
        /// 将十进制转化为特定目标进制
        /// </summary>
        /// <param name="number">十进制数</param>
        /// <param name="targetRadix">目标进制</param>
        /// <returns></returns>
        private static int ConvertToRadit(int number, byte targetRadix)
        {
            var result = 0;

            for (var i = 1; number != 0; number /= targetRadix, i *= 10)
            {
                result += number % targetRadix * i;
            }

            return result;
        }

        /// <summary>
        /// 特定进制转换为十进制
        /// </summary>
        /// <param name="number">指定进制数字</param>
        /// <param name="sourceRadix">指定进制</param>
        /// <returns></returns>
        private static int ConvertToDec(int number, byte sourceRadix)
        {
            var result = 0;

            for (var i = 1; number != 0; number /= 10, i *= sourceRadix)
            {
                result += number % 10 * i;
            }

            return result;
        }
    }
}