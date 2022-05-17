using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NCore.Extensions;

namespace NCore.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static List<FileInfo> GetFiles(this DirectoryInfo o, Func<FileInfo, bool> func)
                => o.GetFiles().Where(func).ToList();

        public static void DeleteDirectoriesWhere(this DirectoryInfo obj, Func<DirectoryInfo, bool> predicate)
            => obj.GetDirectories().Where(predicate).ForEach(x => x.Delete());

        public static void DeleteFilesWhere(this DirectoryInfo obj, Func<FileInfo, bool> predicate)
            => obj.GetFiles().Where(predicate).ForEach(x => x.Delete());
    }
}