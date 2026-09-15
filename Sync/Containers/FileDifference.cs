public class FileDifference
{
    public long Offset { get; set; }
    public required byte[] ChangedData { get; set; }
    public FileDifferenceType DifferenceType { get; set; }
}