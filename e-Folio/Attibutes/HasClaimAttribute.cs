using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace eFolio.Attibutes
{
    public class HasClaimAttribute : TypeFilterAttribute
    {
        public HasClaimAttribute(string claimName, params string[] allowedValues) 
            : base(typeof(HasClaimFilter))
        {
            Arguments = new object[] { allowedValues.Select(item => (object)new Claim(claimName, item)).ToArray() };
        }
    }

}
