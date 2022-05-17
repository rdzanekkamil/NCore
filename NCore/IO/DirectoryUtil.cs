namespace NCore.IO
{
    public static class DirectoryUtil
    {
        public static void MakeAllDictionaryIfNotExists(PathInfo pathInfo)
        {
            pathInfo.IsDirectoryExists()
                .Filter(x => !x)
                .IfPresent(x =>{
                    string tmpPath = string.Empty;
                    pathInfo.AllDictionary.ForEach(element => {
                        tmpPath = Path.Combine(tmpPath, element);
                        MakeDirIfNotExists(tmpPath);
                    });
                });
        }

        public static void MakeDirIfNotExists(string pathInfo)
        {
            if (!Directory.Exists(pathInfo))
                Directory.CreateDirectory(pathInfo);
        }
    }
}