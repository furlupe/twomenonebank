using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Auth.Common.Policies
{
    public static class Policies
    {
        public const string EmployeeOrHigher = "employee_or_higher";
        public const string CreateUserIfNeeded = "create_user";
    }
}
