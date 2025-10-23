namespace IntensitySegments
{
    /// <summary>
    /// Represents a collection of intensity segments, where each segment is defined by a range of values and an
    /// associated intensity. Provides functionality to add, set, and manage intensity values across specified ranges.
    /// </summary>
    /// <remarks>This class maintains a sorted list of key-value pairs, where the keys represent the
    /// boundaries of the segments and the values represent the intensity at those boundaries. Overlapping or adjacent
    /// segments with the same intensity are automatically merged to optimize storage and representation.</remarks>
    public class IntensitySegments
    {
        private SortedList<int, int> points;

        public IntensitySegments()
        {
            points = new SortedList<int, int>();
        }

        public void Add(int from, int to, int amount)
        {
            if (from >= to)
            {
                throw new ArgumentException("From must be less than to.");
            }

            // Process the 'to' point insertions first
            if (!points.ContainsKey(to))
            {
                points.Add(to, 0);
                var toIndex = points.IndexOfKey(to);
                if (toIndex > 0)
                {
                    points[to] = points.Values[toIndex - 1];
                }
            }

            // Process the 'from' point insertions
            if (points.ContainsKey(from))
            {
                points[from] += amount;
            }
            else
            {
                points.Add(from, amount);
                var fromIndex = points.IndexOfKey(from);
                if (fromIndex > 0)
                {
                    points[from] += points.Values[fromIndex - 1];
                }
            }

            // Find all points between from and to and update their intensity
            var fromIndex2 = points.IndexOfKey(from);
            var toIndex2 = points.IndexOfKey(to);
            for (int i = fromIndex2 + 1; i < toIndex2; i++)
            {
                points[points.Keys[i]] += amount;
            }

            MergeSegments();
        }

        public void Set(int from, int to, int amount)
        {
            if (from >= to)
            {
                throw new ArgumentException("From must be less than to.");
            }

            // Process the 'to' point insertions first
            if (!points.ContainsKey(to))
            {
                points.Add(to, 0);
                var toIndex = points.IndexOfKey(to);
                if (toIndex > 0)
                {
                    points[to] = points.Values[toIndex - 1];
                }
            }

            // Process the 'from' point insertions
            if (points.ContainsKey(from))
            {
                points[from] = amount;
            }
            else
            {
                points.Add(from, amount);
            }

            // Find all points between from and to and remove them
            // because they are now overridden with a same intensity
            var fromIndex2 = points.IndexOfKey(from);
            var toIndex2 = points.IndexOfKey(to);

            // Becareful to remove from the end to avoid index shifting
            for (int i = toIndex2 - 1; i > fromIndex2; i--)
            {
                points.RemoveAt(i);
            }

            MergeSegments();
        }

        // Merges consecutive segments with the same intensity
        private void MergeSegments()
        {
            for (int i = points.Count - 1; i > 0; i--)
            {
                if (points.Values[i] == points.Values[i - 1])
                {
                    points.RemoveAt(i);
                }
            }
        }

        public override string ToString()
        {
            if (points.Count == 0)
            {
                return "[]";
            }
            else
            {
                return "[" + string.Join(",", points.Select(kv => $"[{kv.Key},{kv.Value}]")) + "]";
            }
        }
    }
}
