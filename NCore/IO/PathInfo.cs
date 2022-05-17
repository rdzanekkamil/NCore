using System.Diagnostics;
using NCore.Extensions;
using NCore.Monadas;
using static NCore.NCore;

namespace NCore.IO
{
    [DebuggerDisplay("{FullPath}")]
    public struct PathInfo
    {
        public string FullPath { get; init; }

        public List<string> AllDictionary { get; init; }

        public NOptional<string> FileName { get; init; }

        public FileExt Extension { get; init; }

        public bool IsFile => Extension.HasValue;

        public bool IsDirectory => Extension.IsEmpty;

        public bool IsNullOrEmpty { get; init; }

        public static PathInfo Empty = new PathInfo();

        public PathInfo(string path)
        {
            FullPath = path;
            var tp = Path.GetExtension(path);
            Extension = OfNullable(Path.GetExtension(path))
                .Filter(x => !string.IsNullOrEmpty(x));
            FileName = OfNullable(Extension.IsEmpty)
                .Filter(x => !x)
                .Map(x => Path.GetFileNameWithoutExtension(path))
                .Filter(x => !string.IsNullOrEmpty(x));
            AllDictionary = OfNullable(Path.GetDirectoryName(path))
                .Filter(x => !string.IsNullOrEmpty(x))
                .Map(x => x.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries).ToList())
                .OrElse(new List<string>());
            IsNullOrEmpty = string.IsNullOrEmpty(FullPath);
        }

        public PathInfo GetWithBaseDirectory() => OfBaseDirectory(FullPath);  

        public PathInfo GetWithCurrentDirectory() => OfCurrentDirectory(FullPath);

        public FileInfo GetFileInfo() => IsFile ? new FileInfo(FullPath) : new FileInfo(string.Empty);

        public DirectoryInfo GetDirectoryInfo() => IsDirectory ? new DirectoryInfo(FullPath) : new DirectoryInfo(string.Empty);

        public bool IsExists() => IsFile ? File.Exists(FullPath) : Directory.Exists(FullPath);

        public PathInfo Combain(params string[] args)
        {
            var fullPath = FullPath;
            return OfNullable(args)
                .Map(x => {
                    var paths = new List<string>();
                    paths.Add(fullPath);
                    paths.AddRange(args);
                    return paths;
                })
                .Map(x => new PathInfo(string.Join(Path.DirectorySeparatorChar, x)))
                .OrElse(fullPath);
        }

        public NOptional<string> GetDirectoryPath() => OfOptional(string.Join(Path.DirectorySeparatorChar, AllDictionary));

        public NOptional<bool> IsFileExists() 
            => OfOptional(this)
                .Filter(x => x.IsFile)
                .Map(x => File.Exists(x.FullPath));

        public NOptional<bool> IsDirectoryExists() 
            => GetDirectoryPath()
                .Map(x => Directory.Exists(x));

        public static PathInfo OfBaseDirectory(string partOfPath)
            => partOfPath.StartsWithIgnoreCases(System.AppContext.BaseDirectory)
                ? new PathInfo(partOfPath)
                : new PathInfo(Path.Combine(System.AppContext.BaseDirectory, partOfPath));

        public static PathInfo OfCurrentDirectory(string partOfPath)
            => partOfPath.StartsWithIgnoreCases(Environment.CurrentDirectory)
                ? new PathInfo(partOfPath)
                : new PathInfo(Path.Combine(Environment.CurrentDirectory, partOfPath));

        public static PathInfo OfFull(string fullPath) => new PathInfo(fullPath);

        public static implicit operator string(PathInfo o) => o.FullPath;

        public static implicit operator PathInfo(string o) => new PathInfo(o);
    }
}