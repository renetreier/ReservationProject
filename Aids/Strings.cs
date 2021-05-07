

namespace ReservationProject.Aids {
    public static class Strings {

       
        public static string GetHead(this string s, char separator = '.')
            => Safe.Run(
                () => {
                    if (string.IsNullOrWhiteSpace(s)) return string.Empty;
                    var i = s.IndexOf(separator);
                    return i < 0 ? s : s.Substring(0, i);
                }, string.Empty);
        public static string GetTail(this string s, char separator = '.')
            => Safe.Run(
                () => {
                    if (string.IsNullOrWhiteSpace(s)) return string.Empty;
                    var i = s.IndexOf(separator);

                    return i < 0 ? s : s[(i + 1)..];
                }, string.Empty);
    }
}
