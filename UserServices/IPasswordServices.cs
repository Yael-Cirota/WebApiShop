using Entities;

namespace Service
{
    public interface IPasswordServices
    {
        Password GetStrength(string password);
    }
}