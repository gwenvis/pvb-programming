using DN.Color.Puzzle.Editor;
using DN.Service;
using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
    [Service]
    public class EditorPrefabsData : ScriptableObject
    {
        public NewDialog CreateNewPopup => createNewPopup;
        
        [SerializeField] private NewDialog createNewPopup;
        [SerializeField] private GameObject unsavedDataPopup;
    }
}