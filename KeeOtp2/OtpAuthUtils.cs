using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using OtpNet;
using KeePass.Plugins;
using System.Drawing;
using ZXing;

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
        const string KeeOtp1EncoderParameter = "encoder";

        const string builtInTotpPrefix = "TimeOtp";
        const string builtInHotpPrefix = "HmacOtp";

        const string builtInBase32Suffix = "-Secret-Base32";
        const string builtInBase64Suffix = "-Secret-Base64";
        const string builtInHexSuffix = "-Secret-Hex";
        const string builtInUtf8Suffix = "-Secret";

        const string builtInLengthSuffix = "-Length";
        const string builtInPeriodSuffix = "-Period";
        const string builtInAlgorithmSuffix = "-Algorithm";
        const string builtInCounterSuffix = "-Counter";

        const string builtInOtpHashModeSha1 = "HMAC-SHA-1";
        const string builtInOtpHashModeSha256 = "HMAC-SHA-256";
        const string builtInOtpHashModeSha512 = "HMAC-SHA-512";

        const string uriScheme = "otpauth";
        const string uriSecretKey = "secret";
        const string uriIssuerKey = "issuer";
        const string uriAlgorithmKey = "algorithm";
        const string uriSizeKey = "size"; // inofficial for "digits", only in uriToOtpAuthData
        const string uriDigitsKey = "digits";
        const string uriCounterKey = "counter";
        const string uriPeriodKey = "period";
        const string uriEncoderKey = "encoder"; // inofficial for Steam support

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
                else if (checkBuiltInMode(entry))
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
            if (entry.Strings.Exists(builtInTotpPrefix + builtInBase32Suffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInBase64Suffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInHexSuffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInUtf8Suffix))
                return true;
            else if (entry.Strings.Exists(builtInHotpPrefix + builtInBase32Suffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInBase64Suffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInHexSuffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInUtf8Suffix))
                return true;
            return false;
        }

        public static OtpType checkBuiltInType(PwEntry entry)
        {
            if (entry.Strings.Exists(builtInTotpPrefix + builtInBase32Suffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInBase64Suffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInHexSuffix) ||
                entry.Strings.Exists(builtInTotpPrefix + builtInUtf8Suffix))
                return OtpType.Totp;
            else if (entry.Strings.Exists(builtInHotpPrefix + builtInBase32Suffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInBase64Suffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInHexSuffix) ||
                entry.Strings.Exists(builtInHotpPrefix + builtInUtf8Suffix))
                return OtpType.Hotp;
            return OtpType.Totp;
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
                data.Proprietary = false;
                return data;
            }
            else
            {
                NameValueCollection parameters = ParseQueryString(entry.Strings.Get(StringDictionaryKey).ReadString());

                if (parameters[KeeOtp1KeyParameter] == null)
                    throw new ArgumentException("Must have a key in the data");

                OtpAuthData data = new OtpAuthData();

                data.loadedFields = new List<string>() { StringDictionaryKey };

                if (parameters[KeeOtp1TypeParameter] != null)
                    data.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[KeeOtp1TypeParameter], true);

                if (data.Type == OtpType.Totp && parameters[KeeOtp1EncoderParameter] != null)
                    data.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[KeeOtp1EncoderParameter], true);

                if (data.Type == OtpType.Steam)
                    data.Proprietary = false;

                if (parameters[KeeOtp1EncodingParameter] != null)
                    data.Encoding = (OtpSecretEncoding)Enum.Parse(typeof(OtpSecretEncoding), parameters[KeeOtp1EncodingParameter], true);

                data.SetPlainSecret(correctPlainSecret(parameters[KeeOtp1KeyParameter].Replace("%3d", "="), data.Encoding));

                if (parameters[KeeOtp1OtpHashModeParameter] != null)
                    data.Algorithm = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[KeeOtp1OtpHashModeParameter], true);

                if (data.Type == OtpType.Hotp && data.Algorithm != OtpHashMode.Sha1)
                    data.Proprietary = false;

                if (data.Type == OtpType.Totp)
                    data.Period = GetIntOrDefault(parameters, KeeOtp1StepParameter, 30);
                else if (data.Type == OtpType.Hotp)
                    data.Counter = GetIntOrDefault(parameters, KeeOtp1CounterParameter, 0);

                data.Digits = GetIntOrDefault(parameters, KeeOtp1SizeParameter, 6);
                if (data.Type == OtpType.Hotp && data.Digits != 6)
                    data.Proprietary = false;


                return data;
            }
        }

        public static OtpAuthData loadDataFromBuiltInOtp(PwEntry entry)
        {
            OtpAuthData data = new OtpAuthData();

            data.Type = checkBuiltInType(entry);

            data.loadedFields = new List<string>();

            string currentOtpPrefix = builtInTotpPrefix;
            if (data.Type == OtpType.Hotp)
                currentOtpPrefix = builtInHotpPrefix;

            string secretBase32Key = currentOtpPrefix + builtInBase32Suffix;
            string secretBase64Key = currentOtpPrefix + builtInBase64Suffix;
            string secretHexKey = currentOtpPrefix + builtInHexSuffix;
            string secretUTF8Key = currentOtpPrefix + builtInUtf8Suffix;
            if (entry.Strings.Exists(secretBase32Key))
            {
                data.Encoding = OtpSecretEncoding.Base32;
                data.SetPlainSecret(correctPlainSecret(entry.Strings.Get(secretBase32Key).ReadString(), data.Encoding));
                data.loadedFields.Add(secretBase32Key);
            }
            else if (entry.Strings.Exists(secretBase64Key))
            {
                data.Encoding = OtpSecretEncoding.Base64;
                data.SetPlainSecret(correctPlainSecret(entry.Strings.Get(secretBase64Key).ReadString(), data.Encoding));
                data.loadedFields.Add(secretBase64Key);
            }
            else if (entry.Strings.Exists(secretHexKey))
            {
                data.Encoding = OtpSecretEncoding.Hex;
                data.SetPlainSecret(entry.Strings.Get(secretHexKey).ReadString());
                data.loadedFields.Add(secretHexKey);
            }
            else if (entry.Strings.Exists(secretUTF8Key))
            {
                data.Encoding = OtpSecretEncoding.UTF8;
                data.SetPlainSecret(entry.Strings.Get(secretUTF8Key).ReadString());
                data.loadedFields.Add(secretUTF8Key);
            }
            else
                return null;

            string lengthKey = currentOtpPrefix + builtInLengthSuffix;
            if (entry.Strings.Exists(lengthKey))
            {
                int size;
                if (int.TryParse(entry.Strings.Get(lengthKey).ReadString(), out size))
                {
                    data.Digits = size;
                    data.loadedFields.Add(lengthKey);
                    if (data.Type == OtpType.Hotp && data.Digits != 6)
                        data.Proprietary = false;
                }
            }

            string hashModeKey = currentOtpPrefix + builtInAlgorithmSuffix;
            if (entry.Strings.Exists(hashModeKey))
            {
                string hashMode = entry.Strings.Get(hashModeKey).ReadString();
                if (hashMode == builtInOtpHashModeSha1)
                    data.Algorithm = OtpHashMode.Sha1;
                else if (hashMode == builtInOtpHashModeSha256)
                    data.Algorithm = OtpHashMode.Sha256;
                else if (hashMode == builtInOtpHashModeSha512)
                    data.Algorithm = OtpHashMode.Sha512;
                data.loadedFields.Add(hashModeKey);
                if (data.Type == OtpType.Hotp && data.Algorithm != OtpHashMode.Sha1)
                    data.Proprietary = false;
            }

            if (data.Type == OtpType.Totp)
            {
                string periodKey = currentOtpPrefix + builtInPeriodSuffix;
                if (entry.Strings.Exists(periodKey))
                {
                    int period;
                    if (int.TryParse(entry.Strings.Get(periodKey).ReadString(), out period))
                    {
                        data.Period = period;
                        data.loadedFields.Add(periodKey);
                    }
                }
            }
            else if (data.Type == OtpType.Hotp)
            {
                string counterKey = currentOtpPrefix + builtInCounterSuffix;
                if (entry.Strings.Exists(counterKey))
                {
                    int counter;
                    if (int.TryParse(entry.Strings.Get(counterKey).ReadString(), out counter))
                    {
                        data.Counter = counter;
                        data.loadedFields.Add(counterKey);
                    }
                }
            }

            return data;
        }

        public static bool checkEntryMigratable(PwEntry entry, MigrationMode migrateMode)
        {
            switch (migrateMode)
            {
                case MigrationMode.KeeOtp1ToBuiltIn:
                    return checkKeeOtp1Mode(entry);
                case MigrationMode.BuiltInToKeeOtp1:
                    return checkBuiltInMode(entry);
                default:
                    return false;
            }
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

        public static PwEntry migrateToNonProprietary(OtpAuthData data, PwEntry entry)
        {
            entry.Strings.Set(StringDictionaryKey, new ProtectedString(true, otpAuthDataToUri(entry, data).AbsoluteUri));

            return entry;
        }

        public static PwEntry migrateToBuiltInOtp(OtpAuthData data, PwEntry entry)
        {
            string currentOtpPrefix = builtInTotpPrefix;
            if (data.Type == OtpType.Hotp)
                currentOtpPrefix = builtInHotpPrefix;

            if (data.Encoding == OtpSecretEncoding.Base32)
                entry.Strings.Set(currentOtpPrefix + builtInBase32Suffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Base64)
                entry.Strings.Set(currentOtpPrefix + builtInBase64Suffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Hex)
                entry.Strings.Set(currentOtpPrefix + builtInHexSuffix, new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.UTF8)
                entry.Strings.Set(currentOtpPrefix + builtInUtf8Suffix, new ProtectedString(true, data.GetPlainSecret()));

            if (data.Digits != 6)
                entry.Strings.Set(currentOtpPrefix + builtInLengthSuffix, new ProtectedString(false, data.Digits.ToString()));

            if (data.Algorithm != OtpHashMode.Sha1)
            {
                if (data.Algorithm == OtpHashMode.Sha1)
                    entry.Strings.Set(currentOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha1));
                else if (data.Algorithm == OtpHashMode.Sha256)
                    entry.Strings.Set(currentOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha256));
                else if (data.Algorithm == OtpHashMode.Sha512)
                    entry.Strings.Set(currentOtpPrefix + builtInAlgorithmSuffix, new ProtectedString(false, builtInOtpHashModeSha512));
            }

            if (data.Type == OtpType.Totp)
            {
                if (data.Period != 30)
                    entry.Strings.Set(currentOtpPrefix + builtInPeriodSuffix, new ProtectedString(false, data.Period.ToString()));
            }
            else if(data.Type == OtpType.Hotp)
            {
                entry.Strings.Set(currentOtpPrefix + builtInCounterSuffix, new ProtectedString(false, data.Counter.ToString()));
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
            secret = secret.Replace("%3D", "").Replace("=", "").Replace(" ", "");
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
            parameters.Add(String.Format("{0}={1}", uriIssuerKey, Uri.EscapeDataString(entry.Strings.ReadSafe(PwDefs.TitleField))));
            if (data.Algorithm != OtpHashMode.Sha1)
                parameters.Add(String.Format("{0}={1}", uriAlgorithmKey, data.Algorithm.ToString()));
            if (data.Digits != 6)
                parameters.Add(String.Format("{0}={1}", uriDigitsKey, data.Digits));
            if (data.Type == OtpType.Hotp)
                parameters.Add(String.Format("{0}={1}", uriCounterKey, data.Counter));
            if (data.Period != 30)
                parameters.Add(String.Format("{0}={1}", uriPeriodKey, data.Period));

            // Special configuration for Steam, set Host to TOTP and parameter encoder=steam
            if (data.Type == OtpType.Steam)
            {
                uriBuilder.Host = OtpType.Totp.ToString().ToLower();
                parameters.Add(String.Format("{0}={1}", uriEncoderKey, OtpType.Steam.ToString().ToLower()));
            }

            uriBuilder.Query = String.Join("&", parameters.ToArray()); 

            return uriBuilder.Uri;
        }

        public static bool checkUriString(String uriString)
        {
            try
            {
                if (checkUri(new Uri(uriString)))
                    return true;
            }
            catch { }
            return false;
        }

        public static bool checkUri(Uri uri)
        {
            try
            {
                if (uriToOtpAuthData(uri) != null)
                    return true;
            }
            catch { }
            return false;
        }

        public static OtpAuthData uriToOtpAuthData(Uri uri)
        {
            if (uri.Scheme != "otpauth")
                throw new InvalidUriFormat("Given Uri does not start with 'otpauth://'!");

            OtpAuthData data = new OtpAuthData();
            data.Type = (OtpType)Enum.Parse(typeof(OtpType), uri.Host, true);

            string query = uri.Query;
            if (query.StartsWith("?"))
                query = query.TrimStart('?');

            NameValueCollection parameters = ParseQueryString(query);

            if (parameters[uriSecretKey] == null)
                throw new InvalidUriFormat("The Uri does not contain a secret. A secret is required!");

            if (data.Type == OtpType.Totp && parameters[uriEncoderKey] != null)
            {
                data.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[uriEncoderKey], true);
                data.Proprietary = false;
            }
                

            data.Encoding = OtpSecretEncoding.Base32;

            string secret = correctPlainSecret(parameters[uriSecretKey], data.Encoding);

            // Validate secret (catch)
            OtpAuthUtils.validatePlainSecret(secret, data.Encoding);

            data.SetPlainSecret(secret);

            if (parameters[uriAlgorithmKey] != null)
                data.Algorithm = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[uriAlgorithmKey], true);

            if (data.Type == OtpType.Totp)
                data.Period = GetIntOrDefault(parameters, uriPeriodKey, 30);
            else if (data.Type == OtpType.Hotp)
                data.Counter = GetIntOrDefault(parameters, uriCounterKey, 0);

            data.Digits = GetIntOrDefault(parameters, uriDigitsKey, 6);

            return data;                   
                
        }

        public static Uri bitmapToUri(Bitmap bitmap)
        {
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                if (result.ToString().StartsWith("otpauth"))
                {
                    Uri uri = new Uri(result.ToString());
                    if (checkUri(uri))
                        return uri;
                }
            }
            throw new CouldNotFindValidUri("Could not find any fitting QR Code on screen!");
        }

        public static OtpBase getOtp(OtpAuthData data)
        {
            if (data.Type == OtpType.Totp)
                return new OtpTotp(data);
            else if (data.Type == OtpType.Hotp)
                return new OtpHotp(data);
            else if (data.Type == OtpType.Steam)
                return new OtpSteam(data);
            throw new InvalidOtpConfiguration();
        }

        public static string getOtpString(OtpAuthData data)
        {
            if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                return getTotpString(data, DateTime.UtcNow);
            else if (data.Type == OtpType.Hotp)
                return getOtp(data).getHotpString(data.Counter);
            throw new InvalidOtpConfiguration();
        }

        public static string getTotpString(OtpAuthData data, DateTime time)
        {
            return getOtp(data).getTotpString(time);
        }

        public static void increaseHotpCounter(IPluginHost host, OtpAuthData data, PwEntry entry)
        {
            data.Counter = data.Counter + 1;
            if (data.KeeOtp1Mode)
                migrateToKeeOtp1String(data, entry);
            else
                migrateToBuiltInOtp(data, entry);

            entry.Touch(true);
            host.MainWindow.ActiveDatabase.Modified = true;
            host.MainWindow.UpdateUI(false, null, false, null, false, null, true);
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
