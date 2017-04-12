using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class EnumerationExtension : MarkupExtension
    {
        private Type _enumType;
        private IEnumerable<EnumerationMember> members;

        public class EnumerationMember
        {
            public string Description { get; set; }
            public object Value { get; set; }
        }

        public Type EnumType
        {
            get { return _enumType; }
            private set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public EnumerationExtension(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");

            EnumType = enumType;
        }

        protected EnumerationExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            members = GetAllValuesAndDescriptions(EnumType);
            return members;
        }

        public string GetDescription(Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public IEnumerable<EnumerationMember> GetAllValuesAndDescriptions(Type t)
        {
            if (!t.IsEnum)
                throw new ArgumentException("t must be an enum type");

            var enumValues = Enum.GetValues(t).Cast<Enum>();

            return (
              from Enum enumValue in enumValues
              select new EnumerationMember
              {
                  Value = enumValue,
                  Description = GetDescription(enumValue)
              }).ToList();
        }
    }
}
