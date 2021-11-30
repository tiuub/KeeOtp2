using System;

namespace KeeOtp2
{
    [Serializable]
    public class InvalidBase32FormatException : Exception
    {
        public InvalidBase32FormatException() { }
        public InvalidBase32FormatException(string message) : base(message) { }
    }

    [Serializable]
    public class InvalidBase64FormatException : Exception
    {
        public InvalidBase64FormatException() { }
        public InvalidBase64FormatException(string message) : base(message) { }
    }

    [Serializable]
    public class InvalidHexFormatException : Exception
    {
        public InvalidHexFormatException() { }
        public InvalidHexFormatException(string message) : base(message) { }
    }

    [Serializable]
    public class InvalidUriFormat : Exception
    {
        public InvalidUriFormat() { }
        public InvalidUriFormat(string message) : base(message) { }
    }

    [Serializable]
    public class InvalidOtpConfiguration : Exception
    {
        public InvalidOtpConfiguration() { }
        public InvalidOtpConfiguration(string message) : base(message) { }
    }

    [Serializable]
    public class CouldNotFindValidUri : Exception
    {
        public CouldNotFindValidUri() { }
        public CouldNotFindValidUri(string message) : base(message) { }
    }
}
