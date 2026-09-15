public interface IFileData
{
    string FilePath { get; }
    long FileSize { get; }
    DateTime LastModified { get; }
    string FileName { get; }
    
}