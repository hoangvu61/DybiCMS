using System;
using Web.Model.SeedWork;

namespace Web.Model
{

    public partial class CompanyConfigModel
    {
		public Guid Id { get; set; }
		public string Template { get; set; }
		public string Language { get; set; }
        public FileData WebIcon { get; set; }
        public FileData WebImage { get; set; }
        public string Header { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Background { get; set; }
        public FileData BackgroundImage { get; set; }
        public bool CanRightClick { get; set; }
        public bool CanSelectCopy { get; set; }
        public string Keys { get; set; }
        public DateTime? RegisDate { get; set; }
        public DateTime? ExperDate { get; set; }
        public bool Hierarchy { get; set; }
    }
}  
