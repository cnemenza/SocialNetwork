using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.CORE
{
    public class ClaimsExtension
    {
        public static Guid GetCurrentUserId()
            => Guid.Parse(HttpContext.Current.User.Identity.Name.Split('|')[0]);

        public static string GetCurrentName()
            => HttpContext.Current.User.Identity.Name.Split('|')[1];

        public static string GetEmail()
            => HttpContext.Current.User.Identity.Name.Split('|')[2];
    }
}
