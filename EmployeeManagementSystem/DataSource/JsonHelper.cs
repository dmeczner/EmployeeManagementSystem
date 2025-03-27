using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace EmployeeManagementSystem.DataSource
{
    public static class JsonHelper
    {
        private static string GetExecutableDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static async Task SerializeToFileAsync<T>(T obj, string fileName)
        {
            string filePath = Path.Combine(GetExecutableDirectory(), fileName);
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, jsonString);
            Process.Start("notepad.exe", filePath);
        }

        public static async Task<T> DeserializeFromFileAsync<T>(string fileName)
        {
            string filePath = Path.Combine(GetExecutableDirectory(), fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{fileName}' was not found in the executable directory.");
            }

            string jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
