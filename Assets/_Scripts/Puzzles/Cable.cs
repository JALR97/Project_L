using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Cable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //**    ---Components---    **//
    [SerializeField] private Image _image;
    
    //**    ---Variables---    **//
    public int id;
    private Vector3 initialPosition;

    //**    ---Functions---    **//
    private void Start() {
        initialPosition = transform.position;
    }

    public void Connect(Vector3 newPosition) {
        transform.position = newPosition;
        initialPosition = transform.position;
    }
    
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        _image.raycastTarget = true;
        transform.position = initialPosition;
    }


}
