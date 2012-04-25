/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
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

        public override object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var v2 = value as Vector2;
                if (v2 != null)
                {
                    return String.Format(CultureInfo.InvariantCulture,
                                         "Vector2(X={0:0.000000}, Y={1:0.000000})",
                                         v2.X,
                                         v2.Y);
                }

                var v3 = value as Vector3;
                if (v3 != null)
                {
                    return String.Format(CultureInfo.InvariantCulture,
                                         "Vector3(X={0:0.000000}, Y={1:0.000000}, Z={2:0.000000})",
                                         v3.X,
                                         v3.Y,
                                         v3.Z);
                }

                var v4 = value as Vector4;
                if (v4 != null)
                {
                    return String.Format(CultureInfo.InvariantCulture,
                                         "Vector4(X={0:0.000000}, Y={1:0.000000}, Z={2:0.000000}, W={3:0.000000})",
                                         v4.X,
                                         v4.Y,
                                         v4.Z,
                                         v4.W);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context,
                                                                   object value,
                                                                   Attribute[] attributes)
        {
            PropertyDescriptor[] properties = null;
            if (value is Vector2)
            {
                properties = new[]
                {
                    new Descriptor(typeof(Vector2), typeof(float), "X"),
                    new Descriptor(typeof(Vector2), typeof(float), "Y"),
                };
            }
            else if (value is Vector3)
            {
                properties = new[]
                {
                    new Descriptor(typeof(Vector3), typeof(float), "X"),
                    new Descriptor(typeof(Vector3), typeof(float), "Y"),
                    new Descriptor(typeof(Vector3), typeof(float), "Z"),
                };
            }
            else if (value is Vector4)
            {
                properties = new[]
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

        private class Descriptor : SimplePropertyDescriptor
        {
            private readonly string _Name;

            public Descriptor(Type componentType, Type elementType, string name)
                : base(componentType, name, elementType, null)
            {
                this._Name = name;
            }

            public override object GetValue(object instance)
            {
                var vector2 = instance as Vector2;
                if (vector2 != null)
                {
                    switch (this._Name)
                    {
                        case "X":
                        {
                            return vector2.X.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "Y":
                        {
                            return vector2.Y.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        default:
                        {
                            return null;
                        }
                    }
                }

                var vector3 = instance as Vector3;
                if (vector3 != null)
                {
                    switch (this._Name)
                    {
                        case "X":
                        {
                            return vector3.X.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "Y":
                        {
                            return vector3.Y.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "Z":
                        {
                            return vector3.Z.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        default:
                        {
                            return null;
                        }
                    }
                }

                var vector4 = instance as Vector4;
                if (vector4 != null)
                {
                    switch (this._Name)
                    {
                        case "X":
                        {
                            return vector4.X.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "Y":
                        {
                            return vector4.Y.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "Z":
                        {
                            return vector4.Z.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        case "W":
                        {
                            return vector4.W.ToString("0.000000", CultureInfo.InvariantCulture);
                        }

                        default:
                        {
                            return null;
                        }
                    }
                }

                return null;
            }

            public override bool IsReadOnly
            {
                get { return true; }
            }

            public override void SetValue(object instance, object value)
            {
            }
        }
    }
}
