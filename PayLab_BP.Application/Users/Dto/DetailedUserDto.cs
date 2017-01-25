using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Users.Dto
{
    public class DetailedUserDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string SupplierName { get; set; }

        public string RoleType { get; set; }

        public bool IsActive { get; set; }
    }
}
