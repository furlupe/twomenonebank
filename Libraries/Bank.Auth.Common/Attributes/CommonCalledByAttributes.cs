using Bank.Auth.Common.Enumerations;

namespace Bank.Auth.Common.Attributes
{
    public class CalledByHumanAttribute(params Role[] roles) : CalledByAttribute(Caller.Human, roles) { }
    public class CalledByStaffAttribute() : CalledByHumanAttribute(Role.Admin, Role.Employee) { }
    public class CalledByUserAttribute(): CalledByHumanAttribute(Role.User) { }

    public class CalledByServiceAttribute() : CalledByAttribute(Caller.Service) { }
}
