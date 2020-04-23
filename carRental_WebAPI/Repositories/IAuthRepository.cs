namespace carRental_WebAPI.Repositories
{
    public interface IAuthRepository
    {
        bool Login(string userName, string password);
        string CreateToken(string userName);
    }
}
