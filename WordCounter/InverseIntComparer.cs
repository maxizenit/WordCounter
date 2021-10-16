using System.Collections.Generic;

namespace WordCounter
{
    class ReverseComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y) return 1;
            if (x == y) return 0;
            return -1;
        }
    }
}