public class FileInfoComparerMetadata: IFileInfoComparer
{
    public async Task<ComparisonResult> CompareFilesAsync(IFileData filePath1, IFileData filePath2)
    {
        if (filePath1.FileSize != filePath2.FileSize)
        {
            return ComparisonResult.Different;
        }

        if (filePath1.LastModified != filePath2.LastModified)
        {
            return ComparisonResult.Different;
        }

        return ComparisonResult.Same;
    }
}