using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eFolio.Attibutes
{
    public class HasClaimFilter : IAuthorizationFilter
    {
        readonly Claim[] allowedClaims;
        private bool allowAnonymous; 

        public HasClaimFilter(object[] claims, bool allowAnonymous)
        {
            this.allowedClaims = claims.Cast<Claim>().ToArray();
            this.allowAnonymous = allowAnonymous; 
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!allowAnonymous)
            {
                bool hasClaim = false;

                List<Claim> userClaims = context.HttpContext.User.Claims.ToList();
                foreach (Claim userClaim in userClaims)
                {
                    foreach (Claim allowed in allowedClaims)
                    {
                        if (userClaim.Type == allowed.Type && userClaim.Value == allowed.Value)
                        {
                            hasClaim = true;
                            break;
                        }
                    }
                }

                if (!hasClaim)
                { 
                    context.Result = new ForbidResult();
                } 
            }
        }
    }

}
