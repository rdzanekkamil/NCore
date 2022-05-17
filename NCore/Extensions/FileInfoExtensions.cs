using System;
using System.Collections.Generic;
using System.IO;

namespace NCore.Extensions
{
    public static class FileInfoExtensions
    {
        public static void Delete(this IEnumerable<FileInfo> o)
        {
            foreach (FileInfo t in o)
            {
                t.Delete();
            }
        }

        public static String ChangeExtension(this FileInfo o, String extension)
            => Path.ChangeExtension(o.FullName, extension);

        public static String GetDirectoryFullName(this FileInfo o) => o.Directory.FullName;

        public static String GetDirectoryName(this FileInfo o) => o.Directory.Name;

        public static String GetFileNameWithoutExtension(this FileInfo o) => Path.GetFileNameWithoutExtension(o.FullName);

        public static String GetPathRoot(this FileInfo o) => Path.GetPathRoot(o.FullName);

        public static Boolean HasExtension(this FileInfo o) => Path.HasExtension(o.FullName);

        public static Boolean IsPathRooted(this FileInfo o) => Path.IsPathRooted(o.FullName);

        public static Byte[] ReadAllBytes(this FileInfo o) => File.ReadAllBytes(o.FullName);

        public static void Rename(this FileInfo o, string newName)
        {
            string filePath = Path.Combine(o.Directory.FullName, newName);
            o.MoveTo(filePath);
        }

        public static void RenameExtension(this FileInfo o, String extension)
        {
            string filePath = Path.ChangeExtension(o.FullName, extension);
            o.MoveTo(filePath);
        }

        public static void RenameFileWithoutExtension(this FileInfo o, string newName)
        {
            string fileName = string.Concat(newName, o.Extension);
            string filePath = Path.Combine(o.Directory.FullName, fileName);
            o.MoveTo(filePath);
        }
    }
}