using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Stealer
{
    public class Spy
    {
        public Spy()
        {
        }

        public string StealFieldInfo(string className, params string[] fields)
        {
            StringBuilder sb = new StringBuilder();

            Type hackeAccount = Type.GetType(className);
            FieldInfo[] fieldInfos = hackeAccount.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var classInstance = Activator.CreateInstance(hackeAccount);

            sb.AppendLine($"Class under investigation: {hackeAccount}");

            foreach (var field in fieldInfos.Where(f => fields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }
    }
}
