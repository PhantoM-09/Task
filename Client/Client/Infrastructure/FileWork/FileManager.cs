using System.IO;
using System.Text.Json;

namespace Client.Infrastructure.FileWork
{
    class FileManager
    {
        public static T ReadFile<T>(string fileName)
        {
            return JsonSerializer.Deserialize<T>(File.ReadAllText(fileName));
        }
    }
}
