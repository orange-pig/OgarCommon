using System;

namespace OgarCommon
{
    /// <summary>Computes the Adler-32 hash for the input data using the managed library.</summary>
    public class Adler32Helper
    {
        private readonly object syncLock = new object();

        private uint checksum = 1;

        /// <summary>Initializes the algorithm.</summary>
        public void Initialize()
        {
            lock (syncLock)
            {
                checksum = 1;
            }
        }

        /// <summary>Performs the hash algorithm on the data provided.</summary>
        /// <param name="array">The array containing the data.</param>
        /// <param name="ibStart">The position in the array to begin reading from.</param>
        /// <param name="cbSize">How many bytes in the array to read.</param>
        public uint HashCore(byte[] array, int ibStart, int cbSize)
        {
            lock (syncLock)
            {
                int n;
                uint s1 = checksum & 0xFFFF;
                uint s2 = checksum >> 16;

                while (cbSize > 0)
                {
                    n = (3800 > cbSize) ? cbSize : 3800;
                    cbSize -= n;

                    while (--n >= 0)
                    {
                        s1 = s1 + (uint)(array[ibStart++] & 0xFF);
                        s2 = s2 + s1;
                    }

                    s1 %= 65521;
                    s2 %= 65521;
                }

                checksum = (s2 << 16) | s1;

                return checksum;
            }
        }
    }
}
