namespace IntensitySegments
{
    public static class StringExtensions
    {
        /// <summary>
        /// Writes the string to the console and returns it (facilitates chaining).
        /// </summary>
        public static string Dump(this string value)
        {
            Console.WriteLine(value ?? "null");
            return value;
        }
    }
}