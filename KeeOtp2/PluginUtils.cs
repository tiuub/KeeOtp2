using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeeOtp2
{
    class PluginUtils
    {
        public static object GetField(string field, object obj)
        {
            BindingFlags bf = BindingFlags.Instance | BindingFlags.NonPublic;
            return GetField(field, obj, bf);
        }

        public static object GetField(string field, object obj, BindingFlags bf)
        {
            if (obj == null) return null;
            FieldInfo fi = obj.GetType().GetField(field, bf);
            if (fi == null) return null;
            return fi.GetValue(obj);
        }

        public static object GetPluginInstance(string PluginName)
        {
            string comp = PluginName + "." + PluginName + "Ext";
            BindingFlags bf = BindingFlags.Instance | BindingFlags.NonPublic;
            try
            {
                var PluginManager = GetField("m_pluginManager", KeePass.Program.MainForm);
                var PluginList = GetField("m_vPlugins", PluginManager);
                MethodInfo IteratorMethod = PluginList.GetType().GetMethod("System.Collections.Generic.IEnumerable<T>.GetEnumerator", bf);
                IEnumerator<object> PluginIterator = (IEnumerator<object>)(IteratorMethod.Invoke(PluginList, null));
                while (PluginIterator.MoveNext())
                {
                    object result = GetField("m_pluginInterface", PluginIterator.Current);
                    if (comp == result.GetType().ToString()) return result;
                }
            }

            catch (Exception) { }
            return null;
        }

        public static void CheckKeeTheme(Control c)
        {
            KeePass.Plugins.Plugin p = (KeePass.Plugins.Plugin)PluginUtils.GetPluginInstance("KeeTheme");
            if (p == null) return;
            var t = PluginUtils.GetField("_theme", p);
            if (t == null) return;
            bool bKeeThemeEnabled = (bool)t.GetType().GetProperty("Enabled").GetValue(t, null);
            if (!bKeeThemeEnabled) return;
            var v = PluginUtils.GetField("_controlVisitor", p);
            if (v == null) return;
            MethodInfo miVisit = v.GetType().GetMethod("Visit", new Type[] { typeof(Control) });
            if (miVisit == null) return;
            miVisit.Invoke(v, new object[] { c });
        }
    }
}
