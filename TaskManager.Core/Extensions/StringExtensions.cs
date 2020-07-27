namespace TaskManager.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string expressiom)
        {
            return string.IsNullOrWhiteSpace(expressiom);
        }

        public static int ToInt(this string expressiom)
        {
            return int.Parse(expressiom);
        }
    }
}
