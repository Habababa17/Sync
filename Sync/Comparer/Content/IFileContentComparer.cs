public interface IFileContentComparer
{
    Task<FileComparisonResult> CompareFilesAsync(IReadFileContent file1, IReadFileContent file2);

}