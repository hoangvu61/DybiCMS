//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModuleConfig
    {
        public ModuleConfig()
        {
            this.ModuleConfigDetails = new HashSet<ModuleConfigDetail>();
            this.ModuleConfigParams = new HashSet<ModuleConfigParam>();
        }
    
        public System.Guid Id { get; set; }
        public string SkinName { get; set; }
        public string ComponentName { get; set; }
        public string Position { get; set; }
        public int Order { get; set; }
        public bool Apply { get; set; }
        public System.Guid CompanyId { get; set; }
        public bool OnTemplate { get; set; }
    
        public virtual ICollection<ModuleConfigDetail> ModuleConfigDetails { get; set; }
        public virtual ICollection<ModuleConfigParam> ModuleConfigParams { get; set; }
        public virtual ModuleSkin ModuleSkin { get; set; }
        public virtual Company Company { get; set; }
    }
}