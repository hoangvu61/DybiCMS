using System.Collections.Generic;

namespace Web.Asp.ObjectData
{
    using Model.SeedWork;
    using System;
    
    public class ModuleSkin
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int HeaderFontSize { get; set; }
        public string HeaderFontColor { get; set; }
        public string HeaderBackground { get; set; }
        public int BodyFontSize { get; set; }
        public string BodyFontColor { get; set; }
        public string BodyBackground { get; set; }

        public FileData HeaderBackgroundFile { get; set; }
        public FileData BodyBackgroundFile { get; set; }
    }
}
