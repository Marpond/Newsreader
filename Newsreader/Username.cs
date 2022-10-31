using System.IO;

namespace Newsreader;

public static class Username
{
    // Use relative path to the file
    private const string PATH = "../username.txt";

    public static string Get()
    {
        lock (typeof(Username))
        {
            // Check if file exists
            if (File.Exists(PATH))
            {
                // Read username from file
                return File.ReadAllText(PATH);
            }

            // Create file
            File.Create(PATH).Close();

            // Return empty string
            return "";
        }
    }

    public static void Set(string value)
    {
        CheckFileExists();
        // Clear the file
        File.WriteAllText(PATH, string.Empty);
        // Write the new value
        File.WriteAllText(PATH, value);
    }
    
    public static void Delete()
    {
        CheckFileExists();
        // Clear the file
        File.WriteAllText(PATH, string.Empty);
    }
    
    private static void CheckFileExists()
    {
        if (!File.Exists(PATH))
        {
            File.Create(PATH);
        }
    }
}