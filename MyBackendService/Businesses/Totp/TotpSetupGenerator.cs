using MyBackendService.Businesses.TotpHelper;

namespace MyBackendService.Businesses
{
    public class TotpSetupGenerator : ITotpSetupGenerator
    {
        /// <summary>
        /// Generates an object you will need so that the user can setup his Google Authenticator to be used with your app.
        /// </summary>
        /// <param name="accountSecretKey">A secret key which will be used to generate one time passwords. This key is the same needed for validating a passed TOTP.</param>
        /// <returns>TotpSetup with ManualSetupKey in string</returns>
        public string Generate(string accountSecretKey)
        {
            return Base32.Encode(accountSecretKey);
        }
    }
}