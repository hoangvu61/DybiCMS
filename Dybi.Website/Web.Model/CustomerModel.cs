 
 

namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class CustomerModel
    {
        public Guid Id { get; set; }
        public int CompanyId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}  
