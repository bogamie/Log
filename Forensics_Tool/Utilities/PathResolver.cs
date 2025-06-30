using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forensics_Tool.Utilities
{
    internal class PathResolver
    {
        /// <summary>
        /// Generates a directory path for file extraction based on the provided file path.
        /// If the file has a ".tar" extension (e.g., ".tar.gz"), it removes the additional ".tar" suffix from the directory name.
        /// </summary>
        /// <param name="filePath">The full path of the archive file to be extracted.</param>
        /// <returns>The full path to the directory where the contents should be extracted.</returns>
        /// <exception cref="ArgumentException">Thrown if the resulting extraction directory path is invalid.</exception>
        public static string GetExtractionDirectoryPath(string filePath)
        {
            string? baseDir = Path.GetDirectoryName(filePath);
            string? targetDir = Path.GetFileNameWithoutExtension(filePath);

            if (targetDir.EndsWith(".tar", StringComparison.OrdinalIgnoreCase))
                targetDir = Path.GetFileNameWithoutExtension(targetDir); // Remove .tar if present

            string extractionPath = Path.Combine(baseDir ?? string.Empty, targetDir ?? "extracted_files");

            if (!IsValidDirectoryPath(extractionPath))
                throw new ArgumentException("Invalid directory path provided.", nameof(filePath));

            return extractionPath;
        }

        /// <summary>
        /// Creates a directory for file extraction at the specified path.
        /// If the directory already exists, it creates a new one with an incremented name (e.g., "folder", "folder (1)", etc.).
        /// </summary>
        /// <param name="dirPath">The desired path for the extraction directory.</param>
        /// <returns>
        /// The path to the created directory, or an empty string if directory creation fails.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the provided directory path is invalid.</exception>
        public static string MakeExtractionDirectory(string dirPath)
        {
            if (!IsValidDirectoryPath(dirPath))
                throw new ArgumentException("Invalid directory path provided.", nameof(dirPath));

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    Debug.WriteLine($"Created extraction directory: {dirPath}");
                    
                    return dirPath; // Return the original directory path if it was successfully created
                }
                else
                {
                    string? baseDir = Path.GetDirectoryName(dirPath);
                    string folderName = Path.GetFileName(dirPath);

                    string nextDirName = GetNextAvailableDirectoryName(baseDir ?? string.Empty, folderName);
                    Directory.CreateDirectory(nextDirName);

                    return nextDirName; // Return the next available directory name if it already exists
                }
            }
            catch
            {
                return string.Empty; // Return empty string if directory creation fails
            }
        }

        /// <summary>
        /// Returns the next available directory name based on the given base directory and directory name.
        /// </summary>
        /// <param name="baseDir">The base directory where the new directory will be created.</param>
        /// <param name="dirName">The base name for the new directory.</param>
        /// <returns>The full path to the next available directory name.</returns>
        private static string GetNextAvailableDirectoryName(string baseDir, string dirName)
        {
            string searchPattern = $"{dirName} - (*)";

            var existing = Directory
                .EnumerateDirectories(baseDir, "*", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .Where(name => name != null && name.StartsWith($"{dirName} - ("))
                .Select(name =>
                {
                    int start = $"{dirName} - (".Length;
                    int end = name!.IndexOf(')', start);
                    if (end > start)
                    {
                        string numberPart = name.Substring(start, end - start);
                        return int.TryParse(numberPart, out int num) ? num : -1;
                    }
                    return -1;
                })
                .Where(i => i >= 0)
                .ToList();

            int nextIndex = (existing.Count > 0) ? existing.Max() + 1 : 1;

            return Path.Combine(baseDir, $"{dirName} - ({nextIndex})");
        }

        /// <summary>
        /// Checks whether the combination of base path and entry name forms a valid and secure path.
        /// Prevents path traversal (e.g., '..') and invalid characters.
        /// </summary>
        /// <param name="basePath">The base directory path.</param>
        /// <param name="entryName">The entry or subpath to validate.</param>
        /// <returns>True if the combined path is valid and safe; otherwise, false.</returns>
        private static bool IsValidPath(string basePath, string entryName)
        {
            if (string.IsNullOrWhiteSpace(entryName))
                return false;

            if (entryName.Contains("..") ||
                entryName.Contains("./") ||
                entryName.Contains(@".\") ||
                entryName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return false;
            }

            try
            {
                string combinedPath = Path.Combine(basePath, entryName);
                string fullBase = Path.GetFullPath(basePath);
                string fullTarget = Path.GetFullPath(combinedPath);

                return fullTarget.StartsWith(fullBase, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates whether the provided path is a syntactically valid directory path.
        /// Returns false if it is null, empty, or contains invalid path characters.
        /// </summary>
        /// <param name="path">The directory path to validate.</param>
        /// <returns>True if the path is valid; otherwise, false.</returns>
        private static bool IsValidDirectoryPath(string path)
        {
            return !string.IsNullOrWhiteSpace(path) &&
                   path.IndexOfAny(Path.GetInvalidPathChars()) < 0;
        }
    }
}
