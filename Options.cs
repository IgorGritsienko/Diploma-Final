using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma
{
    public static class Options
    {
        public const string connectionString = "Host=students.ami.nstu.ru;Username=pmi-b6605;Password=dyewJev4;Database=students";
        public static string fio;
    }

    public static class Utility
    {
        public static byte[] ConvertHexToBytes(string input)
        {
            var result = new byte[(input.Length + 1) / 2];
            var offset = 0;
            if (input.Length % 2 == 1)
            {
                result[0] = (byte)Convert.ToUInt32(input[0] + "", 16);
                offset = 1;
            }
            for (int i = 0; i < input.Length / 2; i++)
            {
                result[i + offset] = (byte)Convert.ToUInt32(input.Substring(i * 2 + offset, 2), 16);
            }
            return result;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
