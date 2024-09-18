using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using Web.Models.SeedWork;

namespace Web.Api.Controllers
{
    public class FileHelper
    {
        public string ContentRootPath { get; set; }
        public string Folder { get; set; }
        public string OldName { get; set; }
        public FileData File { get; set; }
        public int CompressQuality { get; set; } = 50;

        public FileHelper(string contentRootPath, string folder = "")
        {
            ContentRootPath = contentRootPath;
            Folder = folder;
        }
        public FileHelper(FileData file, string contentRootPath, string folder = "")
        {
            File = file;
            ContentRootPath = contentRootPath;
            Folder = folder;
        }
        public FileHelper(string oldName, FileData file, string contentRootPath, string folder = "")
        {
            OldName = oldName;
            File = file;
            ContentRootPath = contentRootPath;
            Folder = folder;
        }

        public List<string> Gets()
        {
            var path = Path.Combine(ContentRootPath, FilePath.UploadImagePath);
            path = string.Format(path, Folder);
            List<String> filesFound = new List<String>();
            var filters = new String[] { "webp" };
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(path, String.Format("*.{0}", filter)));
            }
            return filesFound.Select(e => Path.GetFileName(e)).ToList();
        }

        public bool CheckExist()
        {
            var path = Path.Combine(ContentRootPath, File.FullPath);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            return System.IO.File.Exists(path);
        }

        public async Task Save()
        {
            var buf = Convert.FromBase64String(File.Base64data);
            var path = Path.Combine(ContentRootPath, File.FilePath);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!string.IsNullOrEmpty(OldName) && OldName != File.FileName)
            {
                var oldFilePath = Path.Combine(path, OldName);
                if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
            }

            CompressImageSixLabors(buf, path + Path.DirectorySeparatorChar + File.FileName);
            //await System.IO.File.WriteAllBytesAsync(path + Path.DirectorySeparatorChar + File.FileName, buf);

            // save webp
            if (File.FileExtension != null && File.FileExtension.ToLower() != ".webp")
            {
                using (var inStream = new MemoryStream(buf))
                {
                    using (var myImage = await Image.LoadAsync(inStream))
                    {
                        await myImage.SaveAsWebpAsync(path + Path.DirectorySeparatorChar + File.FileName + ".webp", new WebpEncoder());

                        if (!string.IsNullOrEmpty(OldName) && OldName != File.FileName)
                        {
                            var oldFileWebpPath = Path.Combine(path, OldName + ".webp");
                            if (System.IO.File.Exists(oldFileWebpPath)) System.IO.File.Delete(oldFileWebpPath);
                        }
                    }
                }
            }
        }

        public async Task SaveChunk()
        {
            var path = Path.Combine(ContentRootPath, File.FilePath);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var filePath = path + Path.DirectorySeparatorChar + File.FileName;

            // open for writing
            using (var stream = System.IO.File.OpenWrite(filePath))
            {
                stream.Seek(File.UploadedBytes, SeekOrigin.Begin);
                stream.Write(File.UploadData, 0, File.UploadData.Length);
            }

            if (File.LastUpload == true)
            {
                // compress image
                CompressImageSixLabors(filePath, filePath);

                // save webp
                if (File.FileExtension != null && File.FileExtension.ToLower() != ".webp")
                {
                    using var myImage = await Image.LoadAsync(filePath);
                    await myImage.SaveAsWebpAsync(filePath + ".webp", new WebpEncoder());
                }
            }
        }

        public void Delete()
        {
            var path = Path.Combine(ContentRootPath, File.FullPath);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            var fileName2 = File.FileName;
            if (File.FileExtension == ".webp") fileName2 = File.FileName.Replace(".webp", "");
            else fileName2 += ".webp";
            path = Path.Combine(ContentRootPath, File.FilePath, fileName2);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        }

        public void CopyFilesRecursively(Guid newCompanyId)
        {
            var path = Path.Combine(ContentRootPath, FilePath.UploadImagePath);
            var sourcePath = string.Format(path, Folder);
            var targetPath = string.Format(path, newCompanyId);

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                System.IO.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        public void DeleteFolder()
        {
            var path = Path.Combine(ContentRootPath, FilePath.UploadImagePath);
            if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
            var folder = new DirectoryInfo(path);

            EmptyFolder(folder);
        }

        private void EmptyFolder(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
            {
                EmptyFolder(subfolder);
            }
        }

        public void CompressImageSixLabors(byte[] buffer, string outputPath)
        {
            using (Image image = Image.Load(buffer))
            {
                var encoder = new JpegEncoder
                {
                    Quality = CompressQuality // Set the quality of JPEG compression
                };

                // Save the image with compression
                image.Save(outputPath, encoder);
            }
        }

        public void CompressImageSixLabors(string inputPath, string outputPath)
        {
            using (Image image = Image.Load(inputPath))
            {
                var encoder = new JpegEncoder
                {
                    Quality = CompressQuality // Set the quality of JPEG compression
                };

                // Save the image with compression
                image.Save(outputPath, encoder);
            }
        }
    }
}
