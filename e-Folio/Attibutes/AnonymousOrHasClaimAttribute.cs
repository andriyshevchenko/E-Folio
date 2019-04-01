using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace eFolio.Attibutes
{
    public class AnonymousOrHasClaimAttribute : TypeFilterAttribute
    {
        public AnonymousOrHasClaimAttribute(string claimName, params string[] allowedValues)
            : base(typeof(AnonymousOrHasClaimFilter))
        {
            Arguments = new object[] { allowedValues.Select(item => (object)new Claim(claimName, item)).ToArray() };
        }   
    }

}
