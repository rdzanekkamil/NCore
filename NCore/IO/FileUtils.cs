using static NCore.NCore;

namespace NCore.IO
{
    public static class FileUtils
    {
        public static void CreateFileWithContentIfNotExists(string filePath, string content)
            => OfOptional(File.Exists(filePath))
                .Filter(x => !x)
                .IfPresent(x => CreateFileWithContent(filePath, content));

        public static void CreateFileWithContentIfNotExists(string filePath, Func<string> content)
        => OfOptional(File.Exists(filePath))
                .Filter(x => !x)
                .IfPresent(x => CreateFileWithContent(filePath, content()));

        public static void CreateFileWithContentIfNotExists(PathInfo filePath, string content)
            => filePath.IsFileExists()
                .Filter(x => !x)
                .IfPresent(x => CreateFileWithContent(filePath.FullPath, content));

        public static void CreateFileWithContentIfNotExists(PathInfo filePath, Func<string> content)
            => filePath.IsFileExists()
                .Filter(x => !x)
                .IfPresent(x => CreateFileWithContent(filePath.FullPath, content()));

        private static void CreateFileWithContent(string filePath, string content)
        {
            File.Create(filePath).Dispose();
            using var tw = new StreamWriter(filePath);
            tw.Write(content);
        }
    }
}