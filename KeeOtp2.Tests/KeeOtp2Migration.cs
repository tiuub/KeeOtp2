﻿using KeeOtp2;
using KeePassLib;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KeeOtp2.Tests
{
    public class KeeOtp2Migration
    {
        [Theory]
        [InlineData("otp", "wroiv2p5agecswssthwzyjn2xzgdquyk", MigrationMode.KeeOtp1ToBuiltIn, true, false)]
        [InlineData("otp", "wroiv2p5agecswssthwzyjn2xzgdq", MigrationMode.KeeOtp1ToBuiltIn, true, false)]
        [InlineData("otp", "key=wroiv2p5agecswssthwzyjn2xzgdquyk", MigrationMode.KeeOtp1ToBuiltIn, true, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroi v2p5 agec swss thwz yjn2 xzgd quyk", MigrationMode.BuiltInToKeeOtp1, true, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroi v2p5 agec swss thwz yjn2 xzgd quyk", MigrationMode.KeeOtp1ToBuiltIn, false, true)]
        [InlineData("TimeOtp-Secret-Base32", "wroiv2p5agecswssthwzyjn2xzgdquyk", MigrationMode.BuiltInToKeeOtp1, true, true)]
        //[InlineData("otp", "", MigrationMode.BuiltInToKeeOtp1, false)]
        //[InlineData("otp", "", MigrationMode.BuiltInToKeeOtp1, false)]
        public void TestKeyFormat(string keyStringName, string keyStringValue, MigrationMode migrateMode,
            bool canMigrate, bool canLoad)
        {
            
            var pwdEntry = new PwEntry(true, true);
            pwdEntry.Strings.Set(keyStringName, new ProtectedString(true, keyStringValue));
            Console.WriteLine(keyStringValue);
            Assert.Equal(canMigrate, OtpAuthUtils.checkEntryMigratable(pwdEntry, migrateMode));
            if (canLoad)
                Assert.NotNull(OtpAuthUtils.loadData(pwdEntry));
            else
                Assert.Null(OtpAuthUtils.loadData(pwdEntry));
        }
    }
}
