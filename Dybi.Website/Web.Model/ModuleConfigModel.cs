namespace Web.Model
{
    using SeedWork;
    using System;

    public partial class ModuleConfigModel
    {    
        public Guid Id { get; set; }
       
        public string ComponentName { get; set; }
        public string Position { get; set; }
        public string SkinName { get; set; }
        
        public string Title { get; set; }

        public int Orders { get; set; }

        public int HeaderFontSize { get; set; }
        public string HeaderFontColor { get; set; }
        public string HeaderBackground { get; set; }
        public int BodyFontSize { get; set; }
        public string BodyFontColor { get; set; }
        public string BodyBackground { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public FileData HeaderBackgroundFile { get; set; }
        public FileData BodyBackgroundFile { get; set; }
    }
}
