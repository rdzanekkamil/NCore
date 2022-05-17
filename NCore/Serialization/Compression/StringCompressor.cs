using System.IO.Compression;
using System.Text;

namespace NCore.Serialization.Compression
{
    public class StringCompressor
    {
        private static Func<MemoryStream, CompressionLevel, GZipStream> gZipStreamFactory = 
            (ms, level) => level == CompressionLevel.Optimal 
                ? new GZipStream(ms, CompressionMode.Compress) 
                : new GZipStream(ms, level);
        public static string GZipStringCompress(string s,
                                                Encoding encoding = null,
                                                CompressionLevel level = CompressionLevel.Optimal)
        {           
            encoding = encoding ?? (encoding = Encoding.UTF8);

            var bytes = encoding.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = gZipStreamFactory(mso, level))
                    msi.CopyTo(gs);
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string GZipStringDecompress(string s, Encoding encoding = null)
        {
            encoding = encoding ?? (encoding = Encoding.UTF8);

            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    gs.CopyTo(mso);
                return encoding.GetString(mso.ToArray());
            }
        }
    }
}