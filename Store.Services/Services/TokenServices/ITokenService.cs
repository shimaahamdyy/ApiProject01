using Store.Data.Entities.IdentityEntities;

namespace Store.Services.Services.TokenServices
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
