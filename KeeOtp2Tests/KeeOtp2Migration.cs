using KeeOtp2;
using KeePassLib;
using KeePassLib.Security;
using System;
using System.Linq;
using Xunit;
using System.Windows.Forms;

namespace KeeOtp2Tests
{
    public class KeeOtp2Migration
    {
        [Theory]
        [InlineData("otp", "wroiv2p5agecswssthwzyjn2xzgdquyk", Settings.MigrateMode.KeeOtp1ToBuiltIn, true, false)]
        [InlineData("otp", "wroiv2p5agecswssthwzyjn2xzgdq", Settings.MigrateMode.KeeOtp1ToBuiltIn, true, false)]
        [InlineData("otp", "key=wroiv2p5agecswssthwzyjn2xzgdquyk", Settings.MigrateMode.KeeOtp1ToBuiltIn, true, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroi v2p5 agec swss thwz yjn2 xzgd quyk", Settings.MigrateMode.BuiltInToKeeOtp1, true, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroi v2p5 agec swss thwz yjn2 xzgd quyk", Settings.MigrateMode.KeeOtp1ToBuiltIn, false, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroiv2p5agecswssthwzyjn2xzgdquyk", Settings.MigrateMode.BuiltInToKeeOtp1, true, true)]
        //[InlineData("otp", "", Settings.MigrateMode.BuiltInToKeeOtp1, false)]
        //[InlineData("otp", "", Settings.MigrateMode.BuiltInToKeeOtp1, false)]
        public void TestKeyFormat(string keyStringName, string keyStringValue, Settings.MigrateMode migrateMode,
            bool canMigrate, bool canLoad)
        {
            var pwdEntry = new PwEntry(true, true);
            pwdEntry.Strings.Set(keyStringName, new ProtectedString(true, keyStringValue));
            Assert.Equal(canMigrate, KeeOtp2.Settings.checkEntryMigratable(pwdEntry, migrateMode));
            if (canLoad)
                Assert.NotNull(OtpAuthUtils.loadData(pwdEntry));
            else
                Assert.Null(OtpAuthUtils.loadData(pwdEntry));
        }

    }
}
