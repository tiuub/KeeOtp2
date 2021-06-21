using System;

namespace KeeOtp2
{
    /// <summary>
    /// The type of One Time Password to generate
    /// </summary>
    public enum OtpType
    {
        /// <summary>
        /// Timed One Time Password
        /// </summary>
        Totp,
        /// <summary>
        /// HMAC One Time Password
        /// </summary>
        Hotp,
        /// <summary>
        /// Steam Timed One Time Password
        /// </summary>
        Steam,
    }
}
