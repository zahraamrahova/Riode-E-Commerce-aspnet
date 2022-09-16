using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
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

                #region Reload Claims for current user
                var currentClaims = currentIdendity.Claims.Where(c => Program.principals.Contains(c.Type)).ToArray();
               foreach (var claim in currentClaims)
                {
                    currentIdendity.RemoveClaim(claim);
                }
                var currentPolicies = await (from uc in _db.UserClaims
                                             where uc.UserId == userId && uc.ClaimValue == "1"
                                             select uc.ClaimType)
                                             .Union(from rc in _db.RoleClaims
                                                    join ur in _db.UserRoles on rc.RoleId equals ur.RoleId
                                                    where ur.UserId == userId && rc.ClaimValue == "1"
                                                    select rc.ClaimType).ToListAsync();

                foreach (var policy in currentPolicies)
                {
                    currentIdendity.AddClaim(new Claim(policy, "1"));
                }
                #endregion
            }
            return principal;
        }
    }
}
