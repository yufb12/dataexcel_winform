using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
 
namespace Feng.Data
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ServiceContractAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OperationContractAttribute : Attribute
    {
        public OperationContractAttribute()
        {
            if (DateTime.Now > new DateTime(2027, 10, 12))
            {
                System.Windows.Forms.MessageBox.Show("Updates Needed");
            }
        }
 
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BaseControlAttribute : Attribute
    {
        public BaseControlAttribute()
        {

        }
        public BaseControlAttribute(string text)
        {

        }
        public BaseControlAttribute(string text,string key)
        {

        }
    }
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DataContractAttribute : Attribute 
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DataMemberAttribute : Attribute
    {
        public DataMemberAttribute()
        {

        }

        public string Name { get; set; }

    }
 
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DisableSerializableAttribute : Attribute
    {
        public DisableSerializableAttribute()
        {
        }
    }

    [DataContract(Name = "", Namespace = "")]
    public class Test
    {
        [DataMember]
        public string Name { get; set; }

        [DisableSerializable]
        public string UserName { get; set; }
    }
}
