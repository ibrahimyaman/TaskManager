using System;

namespace TaskManager.Core.Extensions
{
    public static class EnumExtentions
    {
        public static int ToInt(this Enum EnumValue)
        {
            return (int)(object)EnumValue;
        }
        public static TEnum ToEnum<TEnum>(this int EnumValue) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("enum tipi gelmedi");
            }
            return (TEnum)(object)EnumValue;
        }
    }
}
