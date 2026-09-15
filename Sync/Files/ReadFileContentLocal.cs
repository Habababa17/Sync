public class ReadFileContentLocal : IReadFileContent
{
    private readonly string _filePath;

    public ReadFileContentLocal(string filePath)
    {
        _filePath = filePath;
    }

    public byte[] Buffer(long offset, int length)
    {
        using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
        {
            stream.Seek(offset, SeekOrigin.Begin);
            byte[] buffer = new byte[length];
            int bytesRead = stream.Read(buffer, 0, length);
            if (bytesRead < length)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            return buffer;
        }
    }
}