using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Cable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //**    ---Components---    **//
    [SerializeField] private Image _image;
    
    //**    ---Variables---    **//
    public int id;
    
    //**    ---Functions---    **//
    public void SetColor(int colorId) {
        id = colorId;
    }
    
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        _image.raycastTarget = true;
    }


}
