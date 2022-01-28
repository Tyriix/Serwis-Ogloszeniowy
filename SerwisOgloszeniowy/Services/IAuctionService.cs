namespace SerwisOgloszeniowy.Services
{
    public interface IAuctionService
    {
        bool CheckPrice(string price);
        bool CheckPhoneNumber(string phoneNum);
        bool CheckImage(byte[] image);
    }
}
