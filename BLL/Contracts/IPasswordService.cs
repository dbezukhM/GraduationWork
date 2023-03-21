namespace BLL.Contracts
{
    public interface IPasswordService
    {
        string GeneratePassword(int requiredLength = 8);
    }
}