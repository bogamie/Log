using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Log.IO;
using System.Formats.Tar;
using System.IO.Compression;

namespace Log.Utilities
{
    internal class Extractor
    {
        /// <summary>
        /// Recursively extracts compressed files (e.g., .gz, .gzip, .fzip, .tar, .tar.gz, .tgz) from the specified file path into the destination directory.
        /// For gzip-like files (.gz, .gzip, .fzip), extraction is done in-place.
        /// For tar-based files (.tar, .tar.gz, .tgz), a separate extraction subdirectory is generated.
        /// The method also handles nested archives by recursively extracting discovered compressed files.
        /// </summary>
        /// <param name="filePath">The full path of the archive file to be extracted.</param>
        /// <param name="destinationDir">The directory where extracted content will be stored.</param>
        /// <param name="isRoot">
        /// Indicates whether the extraction is at the top-most level. 
        /// If false, the original file will be deleted after extraction.
        /// </param>
        /// <returns>True if the current file and all nested archives were successfully extracted; otherwise, false.</returns>
        public static bool ExtractRecursive(string filePath, string destinationDir, bool isRoot)
        {
            bool success = ExtractFile(filePath, destinationDir, deleteOriginal: !isRoot);
            if (!success)
            {
                Debug.WriteLine($"Failed to extract file: {filePath}");
                return false;
            }

            foreach (string subFile in Directory.GetFiles(destinationDir, "*.*", SearchOption.AllDirectories))
            {
                if (!File.Exists(subFile))
                    continue;
                string ext = Path.GetExtension(subFile).ToLowerInvariant();
                string subExtractDir = String.Empty;

                if (ext is ".gz" or ".gzip" or ".fzip")
                {
                    subExtractDir = Path.GetDirectoryName(subFile)!;    
                } else if (ext is ".tar" or ".tar.gz" or ".tgz")
                {
                    subExtractDir = PathResolver.GetExtractionDirectoryPath(subFile);
                    subExtractDir = PathResolver.MakeExtractionDirectory(subExtractDir);
                } else
                {
                    continue;
                }
                bool childSuccess = ExtractRecursive(subFile, subExtractDir, isRoot: false);
                if (!childSuccess)
                    return false;
                Debug.WriteLine($"Successfully extracted: {filePath} to {destinationDir}");
            }
            return true;
        }

        /// <summary>
        /// Extracts a single archive file (.tar, .gz, or .gzip) to the specified destination directory.
        /// Automatically deletes the original file after successful extraction if <paramref name="deleteOriginal"/> is true.
        /// </summary>
        /// <param name="filePath">The full path of the archive file to extract.</param>
        /// <param name="destinationDir">The target directory where extracted content will be placed.</param>
        /// <param name="deleteOriginal">Indicates whether to delete the original archive file after successful extraction.</param>
        /// <returns>True if the file was successfully extracted (and deleted if specified); otherwise, false.</returns>
        private static bool ExtractFile(string filePath, string destinationDir, bool deleteOriginal)
        {
            bool extracted = false;
            string fileExt = Path.GetExtension(filePath).ToLowerInvariant();

            if (fileExt == ".tar")
            {
                extracted = ExtractTar(filePath, destinationDir);
            }
            else if (fileExt == ".gz" || fileExt == ".gzip")
            {
                extracted = ExtractGzip(filePath, Path.Combine(destinationDir, Path.GetFileNameWithoutExtension(filePath)));
            }

            if (extracted && deleteOriginal)
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting original file: {ex.Message}");
                    return false;
                }
            }
            return extracted;
        }

        /// <summary>
        /// Extracts the contents of a .tar archive to the specified directory.
        /// </summary>
        /// <param name="tarPath">The path to the .tar archive file.</param>
        /// <param name="destinationDir">The directory where the contents will be extracted.</param>
        /// <returns>True if the extraction succeeds; otherwise, false.</returns>
        private static bool ExtractTar(string tarPath, string destinationDir)
        {
            try
            {
                TarFile.ExtractToDirectory(tarPath, destinationDir, overwriteFiles: true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting tar file: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Decompresses a .gz or .gzip file to the specified output file path.
        /// Does not extract any internal tar contents—only decompresses the GZip stream.
        /// </summary>
        /// <param name="gzipPath">The path to the .gz or .gzip file.</param>
        /// <param name="destinationPath">The path where the decompressed file will be saved.</param>
        /// <returns>True if the decompression succeeds; otherwise, false.</returns>
        private static bool ExtractGzip(string gzipPath, string destinationPath)
        {
            try
            {
                using var input = new FileStream(gzipPath, FileMode.Open, FileAccess.Read);
                using var output = new FileStream(destinationPath, FileMode.Create, FileAccess.Write);
                using var gzip = new GZipStream(input, CompressionMode.Decompress);

                gzip.CopyTo(output);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting gzip file: {ex.Message}");
                return false;
            }
        }
    }
}
