using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className,params string[] fieldsInvestigate)
        {
            Type classType = Type.GetType(className);
            FieldInfo[]classFields = classType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic) ;

            var sb = new StringBuilder();
            Object classInstance = Activator.CreateInstance(classType,new object[] { }) ;
            sb.AppendLine($"Class under investigation: {className}");

            foreach(FieldInfo field in classFields.Where(a=> fieldsInvestigate.Contains(a.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().Trim();
        }
    }
}
