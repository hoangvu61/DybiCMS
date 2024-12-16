
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public class CreateWebModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string WebsiteName { get; set; }

        public string TemplateName { get; set; }
    }
}  
