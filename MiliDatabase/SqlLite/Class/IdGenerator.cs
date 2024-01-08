using System;

namespace MiliSoftware.SqlLite
{
    public static class IdGenerator
    {
        private static readonly object _lock = new object();

        public static string GenerateId(int id)
        {
            byte[] timestampBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond);
            byte[] counterBytes;

            lock (_lock)
            {
                counterBytes = BitConverter.GetBytes(id);
            }

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
                Array.Reverse(counterBytes);
            }

            byte[] idBytes = new byte[12];
            Array.Copy(timestampBytes, 2, idBytes, 0, 6);
            Array.Copy(counterBytes, 0, idBytes, 6, 4);

            return BytesToHex(idBytes);
        }

        private static string BytesToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
