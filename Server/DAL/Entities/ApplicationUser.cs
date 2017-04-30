using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }

        public static bool IsNull(ApplicationUser applicationUser)
        {
            if (Equals(applicationUser,null))
                return true;
            return false;
        }
    }
}
