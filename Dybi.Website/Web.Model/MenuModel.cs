
using System;
using System.Collections.Generic;

namespace Web.Model
{
    public partial class MenuModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Component { get; set; }
        public string Url { get; set; }

        public List<MenuModel> Children { get; set; }
    }
}  
