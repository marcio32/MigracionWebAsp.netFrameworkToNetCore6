using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class LogHelper
    {
        /// <summary>
        /// Create a .txt file in D: disk and separate it by modules
        /// </summary>
        /// <param name="ex">Object that the exception throw</param>
        /// <param name="module">Module where it began. Ex: Companies module.</param>
        /// <param name="method">Method that causes the problem. Ex: SaveCompany()</param>
        public static async Task LogError(Exception ex, string module, string method = "")
        {
            try
            {
                // Create the path to save the file
                var path = string.Format(@"C:\Logs\{0:yyyy-MM-dd}\{1}", DateTime.Now, module);

                // Validate if the path exist already
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Create the file
                var file = string.Format(@"{0}\{1:yyyy-MM-dd}.txt", path, DateTime.Now);

                // Write the content
                var content = $"=====================================\n" +
                                $"---------{DateTime.Now:dd/MM/yyyy H:mm:ss}---------\n" +
                                $"Error: {ex.Message}\n" +
                                $"Módulo: {module}\n" +
                                $"Método: {method}\n" +
                                $"Ubicación: {ex.StackTrace}\n" +
                                $"=====================================\n\n";

                // Save the content into the file
                await File.AppendAllTextAsync(file, content);
            }
            catch { }
        }
    }
}
