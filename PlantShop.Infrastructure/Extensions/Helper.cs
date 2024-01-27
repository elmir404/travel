namespace PlantShop.Infrastructure.Extensions
{
    public static class Helper
    {
        //public static async Task SendSmsAsync(string phone, string message)
        //{
        //    string smsMessage = "http://www.poctgoyercini.com/api_http/sendsms.asp?user=weplant.az_s&password=weplant123s&gsm=" + phone + "&text=" + message;

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(smsMessage))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }

        //}
        public static string GetRandomString()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            //    string smsMessage = "http://www.poctgoyercini.com/api_http/sendsms.asp?user=7gul_servis&password=7gul852&gsm=" + registerVm.Phone + "&text=" + message;

            //    using (var httpClient = new HttpClient())
            //    {
            //        using (var response = await httpClient.GetAsync(smsMessage))
            //        {
            //            string apiResponse = await response.Content.ReadAsStringAsync();
            //        }
            //    }
            return finalString;
        }
    }
}
