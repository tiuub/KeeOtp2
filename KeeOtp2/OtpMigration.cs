using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeOtp2
{
    public enum MigrationMode
    {
        KeeOtp1ToBuiltIn,
        BuiltInToKeeOtp1
    }

    public class MigrationProfile
    {
        public string name { get; set; }
        public MigrationMode migrationMode { get; set; }
        public Dictionary<OtpType, string> findPlaceholder { get; set; }
        public Dictionary<OtpType, string> replacePlaceholder { get; set; }

        public MigrationProfile(string name)
        {
            this.name = name;
        }

        public MigrationProfile(string name, MigrationMode migrationMode, Dictionary<OtpType, string> findPlaceholder, Dictionary<OtpType, string> replacePlaceholder)
        {
            this.name = name;
            this.migrationMode = migrationMode;
            this.findPlaceholder = findPlaceholder;
            this.replacePlaceholder = replacePlaceholder;
        }
    }
}
