namespace ValidationAttributes.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            Type objType = obj.GetType();

            PropertyInfo[] properties = objType.GetProperties().Where(pi => pi.CustomAttributes.Any(a => a.AttributeType.BaseType == typeof(MyValidationAttribute))).ToArray();

            foreach (var property in properties)
            {
                object propValue = property.GetValue(obj);

                foreach (var customAttribute in property.CustomAttributes)
                {
                    Type customAttributeType = customAttribute.AttributeType;
                    object attributeInstance = property.GetCustomAttribute(customAttributeType);

                    MethodInfo validationMethod = customAttributeType.GetMethods().First(m => m.Name == "IsValid");

                    bool result = (bool)validationMethod.Invoke(attributeInstance, new object[] { propValue });

                    if(!result)
                        return false;
                }
            }

            return true;
        }
    }
}
