public interface IFileInfoComparer
{
    Task<ComparisonResult> CompareFilesAsync(IFileData filePath1, IFileData filePath2);
}