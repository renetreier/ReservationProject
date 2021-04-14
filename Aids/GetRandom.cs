using System;

namespace ReservationProject.Aids
{
    public static class GetRandom {
        public static int Int32(int min = int.MinValue, int max = int.MaxValue) {
            var rnd = new Random();
            return rnd.Next(min, max);
        }
    }
}
