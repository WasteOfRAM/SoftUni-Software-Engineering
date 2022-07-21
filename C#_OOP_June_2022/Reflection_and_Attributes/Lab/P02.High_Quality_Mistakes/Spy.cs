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
            FieldInfo[] fieldsInfo = hackeAccount.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var classInstance = Activator.CreateInstance(hackeAccount);

            sb.AppendLine($"Class under investigation: {hackeAccount}");

            foreach (var field in fieldsInfo.Where(f => fields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type classType = Type.GetType($"Stealer.{className}");
            FieldInfo[] fieldsInfo = classType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] nonPublicMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fieldsInfo)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }
            foreach (var method in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }
            foreach (var method in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().Trim();
        }
    }
}
