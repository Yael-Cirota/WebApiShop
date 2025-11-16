using Entities;

namespace Service
{
    public interface IPasswordServices
    {
        Password GetStrength(Password password);
        Password GetStrength(string password);
    }
}