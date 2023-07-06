using UnityEngine;
using UnityEngine.EventSystems;

public class Plug : MonoBehaviour, IDropHandler
{
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    
    //  [[ set in Start() ]] 
    
    
    //**    ---Variables---    **//
    public int id;
    
    //**    ---Properties---    **//
    
    
    //**    ---Functions---    **//
    public void OnDrop(PointerEventData eventData) {
        Cable cable = eventData.pointerDrag.GetComponent<Cable>();
        if (cable.id == id) {
            Debug.Log("Correct cable");
        }
    }
}
