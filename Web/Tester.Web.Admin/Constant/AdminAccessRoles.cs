using Tester.Core.Constant;

namespace Tester.Web.Admin.Constant
{
    public static class AdminAccessRoles
    {
        public const string Roles = RoleNameConstant.Admin + "," +
                                    RoleNameConstant.Moderator + "," +
                                    RoleNameConstant.Lecturer;
    }
}