using Microsoft.AspNetCore.Authentication;
using Riode.WebUI.Models.DAL;
using System.Security.Claims;

namespace Riode.WebUI.AppCode.Providers
{
    public class AppClaimProvider : IClaimsTransformation
    {
        private readonly RiodeDbContext _db;
        public AppClaimProvider(RiodeDbContext db)
        {
            _db = db;
        }
        public  async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {

            if(principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity currentIdendity)
            {
                var userId = Convert.ToInt32(currentIdendity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
                var user =  _db.Users.FirstOrDefault(u => u.Id == userId);
                if (user!=null)
                {
                    currentIdendity.AddClaim(new Claim("name", user.Name));
                    currentIdendity.AddClaim(new Claim("surname", user.Surname));
                }
                #region Relod Roles for current user
                var role = currentIdendity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                while (role != null)
                {
                    currentIdendity.RemoveClaim(role);
                    role = currentIdendity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                }
                var currentRoles = (from ur in _db.UserRoles
                                    join r in _db.Roles on ur.RoleId equals r.Id
                                    where ur.UserId == userId
                                    select r.Name).ToArray();

                foreach (var roleName in currentRoles)
                {
                    currentIdendity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
                #endregion
            }
            return principal;
        }
    }
}
