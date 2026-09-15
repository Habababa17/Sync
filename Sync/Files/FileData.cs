public class FileData : IFileData
{
    public string FilePath { get; private set; }
    public long FileSize { get; private set; }
    public DateTime LastModified { get; private set; }
    public string FileName { get; private set; }
    public FileData(string filePath)
    {
        var info = new FileInfo(filePath);
        FilePath = filePath;
        FileSize = info.Length;
        LastModified = info.LastWriteTime;
        FileName = info.Name;
    }
}