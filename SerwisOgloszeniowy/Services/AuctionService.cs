namespace SerwisOgloszeniowy.Services
{
    public class AuctionService : IAuctionService
    {
        public bool CheckImage(byte[] image)
        {
            if (image is byte[])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPhoneNumber(string phoneNum)
        {
            if (phoneNum.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPrice(string price)
        {
            int Num;
            bool isNum = int.TryParse(price.ToString(), out Num);
            if (isNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
