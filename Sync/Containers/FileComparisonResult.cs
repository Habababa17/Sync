public class FileComparisonResult
{
    public bool IsSame { get; set; }
    public required List<FileDifference> Differences { get; set; }
}
