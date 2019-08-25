using UnityEngine;

namespace DataUtility
{

    [System.Serializable]
    public struct IntVector2
    {
        public int x;
        public int y;

        public IntVector2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
