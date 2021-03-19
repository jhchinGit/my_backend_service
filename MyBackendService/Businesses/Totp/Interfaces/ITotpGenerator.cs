using System;
using System.Collections.Generic;

namespace MyBackendService.Businesses
{
    public interface ITotpGenerator
    {
        /// <summary>
        /// Generates a valid TOTP.
        /// </summary>
        /// <param name="accountSecretKey">User's secret key. Same as used to create the setup.</param>
        /// <returns>Creates a 6 digit one time password.</returns>
        int Generate(string accountSecretKey);

        IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance);
    }
}