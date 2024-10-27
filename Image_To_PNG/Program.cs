using System;
using System.IO;
using System.Drawing;

namespace Image_To_PNG
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ask for folder path
            Console.WriteLine("Enter the folder path:");
            string folderPath = Console.ReadLine();

            // Check if folder exists
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The folder does not exist.");
                return;
            }

            // Get all image files in the folder
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();

                // Check if the file is an image and not a .png
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".gif" || extension == ".tiff")
                {
                    if (ConvertToPng(file))
                    {
                        DeleteNonPng(file);  // Delete non-PNG image after conversion
                    }
                }
            }

            Console.WriteLine("Conversion complete!");
        }

        static bool ConvertToPng(string filePath)
        {
            try
            {
                // Load the image
                using (Image img = Image.FromFile(filePath))
                {
                    // Set the output file path with a .png extension
                    string outputFilePath = Path.ChangeExtension(filePath, ".png");

                    // Save the image as a .png
                    img.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    Console.WriteLine($"Converted: {filePath} -> {outputFilePath}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting {filePath}: {ex.Message}");
                return false;
            }
        }

        static void DeleteNonPng(string filePath)
        {
            try
            {
                // Delete the original non-PNG file
                File.Delete(filePath);
                Console.WriteLine($"Deleted: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting {filePath}: {ex.Message}");
            }
        }
    }
}
