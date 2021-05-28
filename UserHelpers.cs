using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ToDoList
{
    public static class UserHelpers
    {
        public static string GetUserId(this IPrincipal principal)
        {
            //https://entityframeworkcore.com/knowledge-base/38543193/proper-way-to-get-current-user-id-in-entity-framework-core

            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim.Value;
        }
    }
}
