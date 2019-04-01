using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eFolio.Attibutes
{
    public class AnonymousOrHasClaimFilter : IAuthorizationFilter
    {
        readonly Claim[] allowedClaims;

        public AnonymousOrHasClaimFilter(object[] claims)
        {
            this.allowedClaims = claims.Cast<Claim>().ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = false;

            List<Claim> userClaims = context.HttpContext.User.Claims.ToList();
            foreach (Claim userClaim in userClaims)
            {
                foreach (Claim claim in allowedClaims)
                {
                    if (userClaim.Type == claim.Type && userClaim.Value == claim.Value)
                    {
                        hasClaim = true;
                        break;
                    }
                }
            }

            if (!hasClaim && userClaims.Count > 0)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
