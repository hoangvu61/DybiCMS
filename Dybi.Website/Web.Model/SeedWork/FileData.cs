using System;
using System.IO;

namespace Web.Model.SeedWork
{
    public class FileData
    {
        public string Base64data { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public string Folder { get; set; }

        public FileType Type { get; set; }

        public string FilePath { get
            {
                var path = string.Empty;
                if (string.IsNullOrEmpty(FileName)) return FileName;
                switch (Type)
                {
                    case FileType.TemplateImage: path = SeedWork.FilePath.TemplateImagePath; break;
                    case FileType.ModuleImage: path = SeedWork.FilePath.ModuleImagePath; break;
                    case FileType.WebIcon: path = SeedWork.FilePath.WebIcon; break;
                    case FileType.WebLogo: path = SeedWork.FilePath.WebLogo; break;
                    case FileType.WebImage: path = SeedWork.FilePath.WebImage; break;
                    case FileType.Background: path = SeedWork.FilePath.Background; break;
                    case FileType.UploadImage: path = SeedWork.FilePath.UploadImagePath; break;
                    case FileType.CategoryImage: path = SeedWork.FilePath.CategoryImagePath; break;
                    case FileType.ArticleImage: path = SeedWork.FilePath.ArticleImagePath; break;
                    case FileType.MediaImage: path = SeedWork.FilePath.MediaImagePath; break;
                    case FileType.ProductImage: path = SeedWork.FilePath.ProductImagePath; break;
                    case FileType.ItemImage: path = SeedWork.FilePath.ItemImagePath; break;
                    case FileType.Language: path = SeedWork.FilePath.LanguagePath; break;
                    case FileType.EventImage: path = SeedWork.FilePath.EventPath; break;
                }

                if (!string.IsNullOrEmpty(Folder)) path = string.Format(path, Folder);
                return path;
            }
        }

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(FileName)) return FileName;
                return FilePath + "/" + FileName;
            }
        }

        public string FileExtension
        {
            get
            {
                if (string.IsNullOrEmpty(FileName)) return FileName;
                if (!FileName.Contains(".")) return string.Empty;
                var names = FileName.Split('.');
                return "." + names[names.Length - 1];
            }
        }
    }
}
