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
    
    public partial class AttributeSource
    {
        public AttributeSource()
        {
            this.AttributeSourceLanguages = new HashSet<AttributeSourceLanguage>();
            this.AttributeValues = new HashSet<AttributeValue>();
            this.Attributes = new HashSet<Attribute>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid CompanyId { get; set; }
    
        public virtual ICollection<AttributeSourceLanguage> AttributeSourceLanguages { get; set; }
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual Company Company { get; set; }
    }
}