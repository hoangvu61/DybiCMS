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
    
    public partial class Template
    {
        public Template()
        {
            this.TemplateComponents = new HashSet<TemplateComponent>();
            this.TemplatePositions = new HashSet<TemplatePosition>();
            this.TemplateSkins = new HashSet<TemplateSkin>();
            this.TemplateLanguageKeys = new HashSet<TemplateLanguageKey>();
            this.WebConfigs = new HashSet<WebConfig>();
        }
    
        public string TemplateName { get; set; }
        public string ImageName { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPublished { get; set; }
    
        public virtual ICollection<TemplateComponent> TemplateComponents { get; set; }
        public virtual ICollection<TemplatePosition> TemplatePositions { get; set; }
        public virtual ICollection<TemplateSkin> TemplateSkins { get; set; }
        public virtual ICollection<TemplateLanguageKey> TemplateLanguageKeys { get; set; }
        public virtual ICollection<WebConfig> WebConfigs { get; set; }
    }
}