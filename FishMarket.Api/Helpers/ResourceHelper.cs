namespace FishMarket.Api.Helpers;

public static class ResourceHelper
{
    public static string ReadResource(string path, string fileName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(currentDirectory, path, fileName);
        return File.ReadAllText(filePath);
    }
}
