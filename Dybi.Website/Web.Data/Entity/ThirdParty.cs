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
    
    public partial class ThirdParty
    {
        public System.Guid Id { get; set; }
        public string ThirdPartyName { get; set; }
        public string ContentHTML { get; set; }
        public string ComponentName { get; set; }
        public string PositionName { get; set; }
        public bool Apply { get; set; }
        public System.Guid CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}