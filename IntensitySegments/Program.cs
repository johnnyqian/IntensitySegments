namespace IntensitySegments
{
    class Program
    {
        static void Main(string[] args)
        {
            var segments = new IntensitySegments();
            segments.ToString().Dump(); // Should be "[]"

            segments.Add(10, 30, 1);
            segments.ToString().Dump(); // Should be: "[[10,1],[30,0]]"

            segments.Add(20, 40, 1);
            segments.ToString().Dump(); // Should be: "[[10,1],[20,2],[30,1],[40,0]]"

            segments.Add(10, 40, -2);
            segments.ToString().Dump(); // Should be: "[[10,-1],[20,0],[30,-1],[40,0]]"

            segments.Add(20, 25, 0);
            segments.ToString().Dump(); // Should be: "[[10,-1],[20,0],[30,-1],[40,0]]", adding zero intensity does not change anything

            segments.Set(15, 35, 5);
            segments.ToString().Dump(); // Should be: "[[10,-1],[15,5],[35,-1],[40,0]]"

            segments.Add(20, 30, 3);
            segments.ToString().Dump(); // Should be: "[[10,-1],[15,5],[20,8],[30,5],[35,-1],[40,0]]"

            segments.Add(18, 20, 3);
            segments.ToString().Dump(); // Should be: "[[10,-1],[15,5],[18,8],[30,5],[35,-1],[40,0]]", consecutive segments merged by adding

            segments.Set(25, 32, 5);
            segments.ToString().Dump(); // Should be: "[[10,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]", overlapped, consecutive segments merged by setting

            segments.Set(30, 32, 5);
            segments.ToString().Dump(); // Should be: "[[10,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]", no change

            segments.Add(-10, 12, 2);
            segments.ToString().Dump(); // Should be: "[[-10,2],[10, 1], [12,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]", negative start with overlap

            segments.Set(-20, 50, 2);
            segments.ToString().Dump(); // Should be: "[[-20,2],[50,0]]", full overlap

            segments.Add(int.MinValue, -10, 3);
            segments.ToString().Dump(); // Should be: "[[-2147483648,3],[-20,5],[-10,2],[50,0]]", adding from int.MinValue

            segments.Add(20, int.MaxValue, 1);
            segments.ToString().Dump(); // Should be: "[[-2147483648,3],[-20,5],[-10,2],[20,3],[50,1],[2147483647,0]]", adding to int.MaxValue
        }
    }
}