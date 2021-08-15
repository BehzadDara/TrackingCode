using System;
using System.Threading;

namespace TrackingCode.Domain.Implementations
{
    public static class Comb
    {
        private static int _counter;

        private static long GetTicks()
        {
            var i = Interlocked.Increment(ref _counter);
            return DateTime.UtcNow.Ticks + i;
        }

        public static Guid Create()
        {
            var uid = Guid.NewGuid().ToByteArray();
            var binDate = BitConverter.GetBytes(GetTicks());

            return new Guid(
                new[]
                {
                    uid[0], uid[1], uid[2], uid[3],
                    uid[4], uid[5],
                    uid[6], (byte)(0xc0 | (0xf & uid[7])),
                    binDate[1], binDate[0],
                    binDate[7], binDate[6], binDate[5], binDate[4], binDate[3], binDate[2]
                });
        }
    }
}