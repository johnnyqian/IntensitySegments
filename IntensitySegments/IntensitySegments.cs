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

            if (amount == 0)
            {
                return; // No change needed
            }

            // Add the 'to' point if it doesn't exist
            if (!points.ContainsKey(to))
            {
                int valueAtToBefore = GetIntensityAtPosition(to);
                points[to] = valueAtToBefore;
            }

            // Add or update the 'from' point
            int valueAtFromBefore = GetIntensityAtPosition(from);
            points[from] = valueAtFromBefore + amount;

            // Use binary search to find the range of points between 'from' and 'to' efficiently
            var keys = points.Keys.ToArray();
            int fromIndex = BinarySearch(keys, from);
            int toIndex = BinarySearch(keys, to);

            // Update all points between from and to
            for (int i = fromIndex + 1; i < toIndex; i++)
            {
                points[keys[i]] += amount;
            }

            MergeSegmentsInRange(from, to);
        }

        public void Set(int from, int to, int amount)
        {
            if (from >= to)
            {
                throw new ArgumentException("From must be less than to.");
            }

            // Add the 'to' point if it doesn't exist
            if (!points.ContainsKey(to))
            {
                int valueAtToBefore = GetIntensityAtPosition(to);
                points[to] = valueAtToBefore;
            }

            // Set the 'from' point to the new amount
            points[from] = amount;

            // Use binary search to find range of points to remove efficiently
            var keys = points.Keys.ToArray();
            int fromIndex = BinarySearch(keys, from);
            int toIndex = BinarySearch(keys, to);

            // Remove all points in the range (from, to)
            // Becareful to remove from the end to avoid index shifting
            for (int i = toIndex - 1; i > fromIndex; i--)
            {
                points.Remove(keys[i]);
            }

            MergeSegmentsInRange(from, to);
        }

        // A standard binary search
        private int BinarySearch(int[] sortedArray, int target)
        {
            int left = 0;
            int right = sortedArray.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (sortedArray[mid] == target)
                {
                   return mid;
                }
                else if (sortedArray[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }

        // A revised binary search to find the rightmost index where keys[index] <= target
        // If all elements are > target, returns -1
        private int BinarySearchLTE(int[] sortedArray, int target)
        {
            int left = 0;
            int right = sortedArray.Length - 1;
            int resultIndex = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (sortedArray[mid] <= target)
                {
                    resultIndex = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return resultIndex;
        }

        // Gets the intensity value at a specific position (the value of the segment that contains this position)
        // The po
        // Using binary search for O(log n) instead of O(n)
        private int GetIntensityAtPosition(int position)
        {
            if (points.Count == 0)
            {
                return 0;
            }

            if (points.ContainsKey(position))
            {
                return points[position];
            }

            var keys = points.Keys.ToArray();
            int resultIndex = BinarySearchLTE(keys, position);

            if (resultIndex != -1)
            {
                return points[keys[resultIndex]];
            }

            return 0; // Default value if no key <= position
        }

        // Merges consecutive segments with the same intensity
        // Optimized to only check adjacent segments in the affected range
        private void MergeSegmentsInRange(int from, int to)
        {
            if (points.Count <= 1) return;

            var keys = points.Keys.ToArray();
            int fromIndex = BinarySearch(keys, from);
            int toIndex = BinarySearch(keys, to);

            // Adjust indices to ensure we check adjacent segments
            if (fromIndex > 0) fromIndex--;  // Check one point before the range
            if (toIndex < keys.Length - 1) toIndex++;  // Check one point after the range

            // Only check the relevant section for merging, from end to beginning to avoid index shifting
            for (int i = toIndex; i > fromIndex; i--)
            {
                if (points[keys[i]] == points[keys[i - 1]])
                {
                    points.Remove(keys[i]);
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