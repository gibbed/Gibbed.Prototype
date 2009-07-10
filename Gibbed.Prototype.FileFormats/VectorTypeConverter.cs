using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Gibbed.Prototype.FileFormats
{
    public class VectorTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Vector2 v2 = value as Vector2;
                if (v2 != null) {
                    return String.Format(CultureInfo.InvariantCulture, "Vector2(X={0:0.000000}, Y={1:0.000000})", v2.X, v2.Y);
                }

                Vector3 v3 = value as Vector3;
                if (v3 != null)
                {
                    return String.Format(CultureInfo.InvariantCulture, "Vector3(X={0:0.000000}, Y={1:0.000000}, Z={2:0.000000})", v3.X, v3.Y, v3.Z);
                }

                Vector4 v4 = value as Vector4;
                if (v4 != null)
                {
                    return String.Format(CultureInfo.InvariantCulture, "Vector4(X={0:0.000000}, Y={1:0.000000}, Z={2:0.000000}, W={3:0.000000})", v4.X, v4.Y, v4.Z, v4.W);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptor[] properties = null;
            if (value is Vector2)
            {
                properties = new Descriptor[2]
                {
                    new Descriptor(typeof(Vector2), typeof(float), "X"),
                    new Descriptor(typeof(Vector2), typeof(float), "Y"),
                };
            }
            else if (value is Vector3)
            {
                properties = new Descriptor[3]
                {
                    new Descriptor(typeof(Vector3), typeof(float), "X"),
                    new Descriptor(typeof(Vector3), typeof(float), "Y"),
                    new Descriptor(typeof(Vector3), typeof(float), "Z"),
                };
            }
            else if (value is Vector4)
            {
                properties = new Descriptor[4]
                {
                    new Descriptor(typeof(Vector4), typeof(float), "X"),
                    new Descriptor(typeof(Vector4), typeof(float), "Y"),
                    new Descriptor(typeof(Vector4), typeof(float), "Z"),
                    new Descriptor(typeof(Vector4), typeof(float), "W"),
                };
            }
            return new PropertyDescriptorCollection(properties);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        private class Descriptor : TypeConverter.SimplePropertyDescriptor
        {
            string name;
            public Descriptor(Type componentType, Type elementType, string name)
                : base(componentType, name, elementType, null)
            {
                this.name = name;
            }

            public override object GetValue(object instance)
            {
                if (instance is Vector2)
                {
                    switch (name)
                    {
                        case "X":
                            return (((Vector2)instance).X).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "Y":
                            return (((Vector2)instance).Y).ToString("0.000000", CultureInfo.InvariantCulture);
                        default:
                            return null;
                    }
                }
                else if (instance is Vector3)
                {
                    switch (name)
                    {
                        case "X":
                            return (((Vector3)instance).X).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "Y":
                            return (((Vector3)instance).Y).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "Z":
                            return (((Vector3)instance).Z).ToString("0.000000", CultureInfo.InvariantCulture);
                        default:
                            return null;
                    }

                }
                else if (instance is Vector4)
                {
                    switch (name)
                    {
                        case "X":
                            return (((Vector4)instance).X).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "Y":
                            return (((Vector4)instance).Y).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "Z":
                            return (((Vector4)instance).Z).ToString("0.000000", CultureInfo.InvariantCulture);
                        case "W":
                            return (((Vector4)instance).W).ToString("0.000000", CultureInfo.InvariantCulture);
                        default:
                            return null;
                    }
                }
                return null;
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override void SetValue(object instance, object value)
            {
            }
        }
    }
}
