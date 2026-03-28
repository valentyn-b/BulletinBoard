namespace BulletinBoard.UI.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email, string name);
    }
}
