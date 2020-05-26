using UnityEngine;

namespace DN.Puzzle.Color
{
    public partial class ColorPuzzleSettings
    {
        [System.Serializable]
        public partial struct ColorLineSettings
        {
            public LineColor LineColor => lineColor;
            public UnityEngine.Color Color => color;
            [SerializeField] private LineColor lineColor;
            [SerializeField] private UnityEngine.Color color;
        }
    }
}