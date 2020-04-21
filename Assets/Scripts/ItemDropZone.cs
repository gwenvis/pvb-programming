using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DN.UI
{
    /// <summary>
    /// This script handles everything after the dragged item is dropped on to an <see cref="IDroppable"/> object
    /// </summary>
    public class ItemDropZone : MonoBehaviour, IDroppable
    {
        private DraggableItem currentObj;

        public void Drop(DraggableItem droppedObject)
        {
            if(currentObj == null)
            {
                droppedObject.transform.position = transform.position;

                currentObj = droppedObject;

                droppedObject.PickedUpItemEvent += OnPickedUpItemEvent;
            }
            else
            {
                droppedObject.transform.position = droppedObject.StartPos;
            }
        }

        private void OnPickedUpItemEvent()
        {
            currentObj.PickedUpItemEvent -= OnPickedUpItemEvent;
            currentObj = null;
        }
    }
}
