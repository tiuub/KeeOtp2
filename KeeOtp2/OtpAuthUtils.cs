using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace KeeOtp2
{
    public class OtpAuthUtils
    {
        const string StringDictionaryKey = "otp";

        const string KeeOtp1KeyParameter = "key";
        const string KeeOtp1TypeParameter = "type";
        const string KeeOtp1StepParameter = "step";
        const string KeeOtp1SizeParameter = "size";
        const string KeeOtp1EncodingParameter = "encoding";
        const string KeeOtp1CounterParameter = "counter";
        const string KeeOtp1OtpHashModeParameter = "otpHashMode";

        const string builtInOtpPrefix = "TimeOtp";

        const string builtInBase32Suffix = "-Secret-Base32";
        const string builtInBase64Suffix = "-Secret-Base64";
        const string builtInHexSuffix = "-Secret-Hex";
        const string builtInUtf8Suffix = "-Secret";

        const string builtInLengthSuffix = "-Length";
        const string builtInPeriodSuffix = "-Period";
        const string builtInAlgorithmSuffix = "-Algorithm";

        const string builtInOtpHashModeSha1 = "HMAC-SHA-1";
        const string builtInOtpHashModeSha256 = "HMAC-SHA-256";
        const string builtInOtpHashModeSha512 = "HMAC-SHA-512";

        const string uriScheme = "otpauth";
        const string uriSecretKey = "secret";
        const string uriIssuerKey = "issuer";
        const string uriAlgorithmKey = "algorithm";
        const string uriDigitsKey = "digits";
        const string uriCounterKey = "counter";
        const string uriPeriodKey = "period";

        public static OtpAuthData loadData(PwEntry entry)
        {
            try
            {
                if (checkKeeOtp1Mode(entry))
                {
                    OtpAuthData data = loadDataFromKeeOtp1String(entry);
                    data.KeeOtp1Mode = true;
                    return data;
                }
                else if (entry.Strings.GetKeys().Any(x => x.StartsWith(builtInOtpPrefix)))
                {
                    OtpAuthData data = loadDataFromBuiltInOtp(entry);
                    return data;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool checkEntry(PwEntry entry)
        {
            return checkKeeOtp1Mode(entry) || checkBuiltInMode(entry);
        }

        public static bool checkKeeOtp1Mode(PwEntry entry)
        {
            if (entry.Strings.Exists(StringDictionaryKey))
                return true;
            return false;
        }

        public static bool checkBuiltInMode(PwEntry entry)
        {
            if (entry.Strings.Exists(builtInOtpPrefix + builtInBase32Suffix) ||
                entry.Strings.Exists(builtInOtpPrefix + builtInBase64Suffix) ||
                entry.Strings.Exists(builtInOtpPrefix + builtInHexSuffix) ||
                entry.Strings.Exists(builtInOtpPrefix + builtInUtf8Suffix))
                return true;
            return false;
        }

        public static PwEntry purgeLoadedFields(OtpAuthData data, PwEntry entry)
        {
            if (data != null && data.loadedFields != null)
                foreach (string field in data.loadedFields)
                {
                    if (entry.Strings.Exists(field))
                        entry.Strings.Remove(field);
                }
            return entry;
        }

        public static PwEntry replacePlaceholder(PwEntry entry, string oldPlaceholder, string newPlaceholder)
        {
            entry.AutoType.DefaultSequence = entry.AutoType.DefaultSequence.Replace(oldPlaceholder, newPlaceholder);
            foreach (AutoTypeAssociation ata in entry.AutoType.Associations)
            {
                ata.Sequence = ata.Sequence.Replace(oldPlaceholder, newPlaceholder);
            }
            return entry;
        }

        public static OtpAuthData loadDataFromKeeOtp1String(PwEntry entry)
        {
            if (checkUriString(entry.Strings.Get(StringDictionaryKey).ReadString()))
            {
                OtpAuthData data = uriToOtpAuthData(new Uri(entry.Strings.Get(StringDictionaryKey).ReadString()));
                data.loadedFields = new List<string>() { StringDictionaryKey };
                return data;
            }
            else
            {
                NameValueCollection parameters = ParseQueryString(entry.Strings.Get(StringDictionaryKey).ReadString());

                if (parameters[KeeOtp1KeyParameter] == null)
                    throw new ArgumentException("Must have a key in the data");

                OtpAuthData otpData = new OtpAuthData();

                otpData.loadedFields = new List<string>() { StringDictionaryKey };

                if (parameters[KeeOtp1EncodingParameter] != null)
                    otpData.Encoding = (OtpSecretEncoding)Enum.Parse(typeof(OtpSecretEncoding), parameters[KeeOtp1EncodingParameter], true);

                otpData.SetPlainSecret(correctPlainSecret(parameters[KeeOtp1KeyParameter].Replace("%3d", "="), otpData.Encoding));

                if (parameters[KeeOtp1TypeParameter] != null)
                    otpData.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[KeeOtp1TypeParameter], true);

                if (parameters[KeeOtp1OtpHashModeParameter] != null)
                    otpData.Algorithm = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[KeeOtp1OtpHashModeParameter], true);

                if (otpData.Type == OtpType.Totp)
                    otpData.Period = GetIntOrDefault(parameters, KeeOtp1StepParameter, 30);
                else if (otpData.Type == OtpType.Hotp)
                    otpData.Counter = GetIntOrDefault(parameters, KeeOtp1CounterParameter, 0);

                otpData.Digits = GetIntOrDefault(parameters, KeeOtp1SizeParameter, 6);

                return otpData;
            }
        }

        public static OtpAuthData loadDataFromBuiltInOtp(PwEntry entry)
        {
            OtpAuthData otpData = new OtpAuthData();

            otpData.loadedFields = new List<string>();

            string secretBase32Key = builtInOtpPrefix + builtInBase32Suffix;
            string secretBase64Key = builtInOtpPrefix + builtInBase64Suffix;
            string secretHexKey = builtInOtpPrefix + builtInHexSuffix;
            string secretUTF8Key = builtInOtpPrefix + builtInUtf8Suffix;
            if (entry.Strings.Exists(secretBase32Key))
            {
                otpData.Encoding = OtpSecretEncoding.Base32;
                otpData.SetPlainSecret(correctPlainSecret(entry.Strings.Get(secretBase32Key).ReadString(), otpData.Encoding));
                otpData.loadedFields.Add(secretBase32Key);
            }
            else if (entry.Strings.Exists(secretBase64Key))
            {
                otpData.Encoding = OtpSecretEncoding.Base64;
                otpData.SetPlainSecret(correctPlainSecret(entry.Strings.Get(secretBase64Key).ReadString(), otpData.Encoding));
                otpData.loadedFields.Add(secretBase64Key);
            }
            else if (entry.Strings.Exists(secretHexKey))
            {
                otpData.Encoding = OtpSecretEncoding.Hex;
                otpData.SetPlainSecret(entry.Strings.Get(secretHexKey).ReadString());
                otpData.loadedFields.Add(secretHexKey);
            }
            else if (entry.Strings.Exists(secretUTF8Key))
            {
                otpData.Encoding = OtpSecretEncoding.UTF8;
                otpData.SetPlainSecret(entry.Strings.Get(secretUTF8Key).ReadString());
                otpData.loadedFields.Add(secretUTF8Key);
            }
            else
                return null;

            string lengthKey = builtInOtpPrefix + builtInLengthSuffix;
            if (entry.Strings.Exists(lengthKey))
            {
                int size;
                if (int.TryParse(entry.Strings.Get(lengthKey).ReadString(), out size))
                {
                    otpData.Digits = size;
                    otpData.loadedFields.Add(lengthKey);
                }
            }

            string stepKey = builtInOtpPrefix + builtInPeriodSuffix;
            if (entry.Strings.Exists(stepKey))
            {
                int step;
                if (int.TryParse(entry.Strings.Get(stepKey).ReadString(), out step))
                {
                    otpData.Period = step;
                    otpData.loadedFields.Add(stepKey);
                }
            }

            string hashModeKey = builtInOtpPrefix + builtInAlgorithmSuffix;
            if (entry.Strings.Exists(hashModeKey))
            {
                string hashMode = entry.Strings.Get(hashModeKey).ReadString();
                if (hashMode == builtInOtpHashModeSha1)
                    otpData.Algorithm = OtpHashMode.Sha1;
                else if (hashMode == builtInOtpHashModeSha256)
                    otpData.Algorithm = OtpHashMode.Sha256;
                else if (hashMode == builtInOtpHashModeSha512)
                    otpData.Algorithm = OtpHashMode.Sha512;
                otpData.loadedFields.Add(hashModeKey);
            }

            return otpData;
        }

        public static PwEntry migrateToKeeOtp1String(OtpAuthData data, PwEntry entry)
        {
            NameValueCollection collection = new NameValueCollection();

            collection.Add(KeeOtp1KeyParameter, data.GetPlainSecret());

            if (data.Type != OtpType.Totp)
                collection.Add(KeeOtp1TypeParameter, data.Type.ToString());

            if (data.Type == OtpType.Hotp)
                collection.Add(KeeOtp1CounterParameter, data.Counter.ToString());
            else if (data.Type == OtpType.Totp)
            {
                if (data.Period != 30)
                    collection.Add(KeeOtp1StepParameter, data.Period.ToString());
            }

            if (data.Digits != 6)
                collection.Add(KeeOtp1SizeParameter, data.Digits.ToString());

            if (data.Algorithm != OtpHashMode.Sha1)
                collection.Add(KeeOtp1OtpHashModeParameter, data.Algorithm.ToString());

            if (data.Encoding != OtpSecretEncoding.Base32)
                collection.Add(KeeOtp1EncodingParameter, data.Encoding.ToString());

            string output = string.Empty;
            foreach (var key in collection.AllKeys)
            {
                output += string.Format("{0}={1}&", key, collection[key]);
            }

            entry.Strings.Set(StringDictionaryKey, new ProtectedString(true, output.TrimEnd('&')));

            return entry;
        }

        public static PwEntry migrateToBuiltInOtp(OtpAuthData data, PwEntry entry)
        {
            if (data.Encoding == OtpSecretEncoding.Base32)
                entry.Strings.Set(builtInOtpPrefix + builtInBase32Suffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Base64)
                entry.Strings.Set(builtInOtpPrefix + builtInBase64Suffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Hex)
                entry.Strings.Set(builtInOtpPrefix + builtInHexSuffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.UTF8)
                entry.Strings.Set(builtInOtpPrefix + builtInUtf8Suffix, new ProtectedString(true, data.GetPlainSecret()));

            if (data.Digits != 6)
                entry.Strings.Set(builtInOtpPrefix + builtInLengthSuffix, new ProtectedString(false, data.Digits.ToString()));

            if (data.Period != 30)
                entry.Strings.Set(builtInOtpPrefix + builtInPeriodSuffix, new ProtectedString(false, data.Period.ToString()));

            if (data.Algorithm != OtpHashMode.Sha1)
            {
                if (data.Algorithm == OtpHashMode.Sha1)
                    entry.Strings.Set(builtInOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha1));
                else if (data.Algorithm == OtpHashMode.Sha256)
                    entry.Strings.Set(builtInOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha256));
                else if (data.Algorithm == OtpHashMode.Sha512)
                    entry.Strings.Set(builtInOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha512));
            }

            return entry;
        }

        /// <remarks>
        /// Hacky query string parsing.  This was done due to reports
        /// of people with just a 3.5 or 4.0 client profile getting errors
        /// as the System.Web assembly where .net's implementation of
        /// Url encoding and query string parsing is located.
        /// 
        /// This should be fine since the only thing stored in the string
        /// that needs to be encoded or decoded is the '=' sign.
        /// </remarks>
        private static NameValueCollection ParseQueryString(string data)
        {
            var collection = new NameValueCollection();

            var parameters = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                if (parameter.Contains("="))
                {
                    var pieces = parameter.Split(new[] { '=' }, 2);
                    if (pieces.Length != 2)
                        continue;

                    collection.Add(pieces[0], pieces[1].Replace("%3d", "="));
                }
            }

            return collection;
        }

        private static int GetIntOrDefault(NameValueCollection parameters, string parameterKey, int defaultValue)
        {
            if (parameters[parameterKey] != null)
            {
                int step;
                if (int.TryParse(parameters[parameterKey], out step))
                    return step;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }

        public static bool validatePlainSecret(string secret, OtpSecretEncoding encoding)
        {
            if (encoding == OtpSecretEncoding.Base32)
            {
                if (Regex.IsMatch(secret, @"^(?:[A-Z2-7]{8})*(?:[A-Z2-7]{2}={6}|[A-Z2-7]{4}={4}|[A-Z2-7]{5}={3}|[A-Z2-7]{7}=)?$"))
                    return true;
                else
                    throw new InvalidBase32FormatException("Invalid Base32 format!\n\nRequired length: 8 * n (if shorter, fill up with =)\n\nAllowed characters:\nABCDEFGHIJKLMNOPQRSTUVWXYZ234567=\n\nRegex:\n^(?:[A-Z2-7]{8})*(?:[A-Z2-7]{2}={6}|[A-Z2-7]{4}={4}|[A-Z2-7]{5}={3}|[A-Z2-7]{7}=)+$");
            }
            else if (encoding == OtpSecretEncoding.Base64)
            {
                if (Regex.IsMatch(secret, @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$"))
                    return true;
                else
                    throw new InvalidBase64FormatException("Invalid Base32 format!\n\nRequired length: 4 * n (if shorter, fill up with =)\n\nAllowed characters:\nabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789=\n\nRegex:\n^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$");
            }
            else if (encoding == OtpSecretEncoding.Hex)
            {
                if (Regex.IsMatch(secret, @"^([A-Fa-f0-9]{2})+$"))
                    return true;
                else
                    throw new InvalidHexFormatException("Invalid Hex format!\n\nRequired length: 2 * n\n\nAllowed characters:\nabcdefABCDEF0123456789\n\nRegex:\n^([a-fA-F0-9]{2})+$");
            }
            else if (encoding == OtpSecretEncoding.UTF8)
            {
                return true;
            }
            throw new InvalidOperationException("No Encoding given!");
        }

        public static string correctPlainSecret(string secret, OtpSecretEncoding encoding)
        {
            secret = secret.Replace("=", "").Replace(" ", "");
            int secretLength = secret.Length;

            if (encoding == OtpSecretEncoding.Base32)
            {
                secret = secret.ToUpper();
                if (secretLength % 8 == 2 || secretLength % 8 == 4 || secretLength % 8 == 5 || secretLength % 8 == 7)
                {
                    secret += new string('=', 8 - secretLength % 8);
                }
            }
            else if (encoding == OtpSecretEncoding.Base64)
            {
                if (secretLength % 4 == 2 || secretLength % 4 == 3)
                {
                    secret += new string('=', 4 - secretLength % 4);
                }
            }

            return secret;
        }

        public static Uri otpAuthDataToUri(PwEntry entry, OtpAuthData data)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = uriScheme;
            uriBuilder.Host = data.Type.ToString().ToLower();
            uriBuilder.Path = String.Format("{0}:{1}", entry.Strings.ReadSafe(PwDefs.TitleField), entry.Strings.ReadSafe(PwDefs.UserNameField));

            List<string> parameters = new List<string>();
            parameters.Add(String.Format("{0}={1}", uriSecretKey, data.GetPlainSecret()));
            parameters.Add(String.Format("{0}={1}", uriIssuerKey, entry.Strings.ReadSafe(PwDefs.TitleField)));
            if (data.Algorithm != OtpHashMode.Sha1)
                parameters.Add(String.Format("{0}={1}", uriAlgorithmKey, data.Algorithm.ToString()));
            if (data.Digits != 6)
                parameters.Add(String.Format("{0}={1}", uriDigitsKey, data.Digits));
            if (data.Type == OtpType.Hotp)
                parameters.Add(String.Format("{0}={1}", uriCounterKey, data.Counter));
            if (data.Period != 30)
                parameters.Add(String.Format("{0}={1}", uriPeriodKey, data.Period));

            uriBuilder.Query = String.Join("&", parameters.ToArray()); 

            return uriBuilder.Uri;
        }

        public static bool checkUriString(String uriString)
        {
            try
            {
                if (uriToOtpAuthData(new Uri(uriString)) != null)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static OtpAuthData uriToOtpAuthData(Uri uri)
        {
            if (uri.Scheme == "otpauth")
            {
                OtpAuthData data = new OtpAuthData();
                data.Type = (OtpType)Enum.Parse(typeof(OtpType), uri.Host, true);

                string query = uri.Query;
                if (query.StartsWith("?"))
                    query = query.TrimStart('?');

                NameValueCollection parameters = ParseQueryString(query);
                if (parameters[uriSecretKey] != null)
                {
                    data.Encoding = OtpSecretEncoding.Base32;

                    string secret = correctPlainSecret(parameters[uriSecretKey], data.Encoding);

                    // Validate secret (catch)
                    OtpAuthUtils.validatePlainSecret(secret, data.Encoding);

                    data.SetPlainSecret(secret);

                    if (parameters[uriAlgorithmKey] != null)
                        data.Algorithm = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[uriAlgorithmKey], true);

                    if (data.Type == OtpType.Totp)
                        data.Period = GetIntOrDefault(parameters, KeeOtp1StepParameter, 30);
                    else if (data.Type == OtpType.Hotp)
                        data.Counter = GetIntOrDefault(parameters, KeeOtp1CounterParameter, 0);

                    data.Digits = GetIntOrDefault(parameters, KeeOtp1SizeParameter, 6);

                    return data;
                }
                else
                    throw new InvalidUriFormat("The Uri does not contain a secret. A secret is required!");
            }
            else
                throw new InvalidUriFormat("Given Uri does not start with 'otpauth://'!");
        }

        public static Totp getTotp(OtpAuthData data)
        {
            return new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
        }

        public static string getTotpString(OtpAuthData data)
        {
           return getTotpString(data, DateTime.UtcNow);
        }

        public static string getTotpString(OtpAuthData data, DateTime time)
        {
            try
            {
                return getTotp(data).ComputeTotp(time);
            }
            catch
            {
                return null;
            }
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
