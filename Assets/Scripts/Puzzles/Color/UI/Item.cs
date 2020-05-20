using DN.UI;
using UnityEngine;

namespace DN.Puzzle.Color
{
    public partial class ColorCommandDropzone
    {
        private partial struct Item
        {
            public DraggableItem draggableItem;
            public ColorCommand colorCommand;
            public Transform previousParent;
        }
    }
}