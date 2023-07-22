using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour

{
    //**    ---Components---    **//
    [SerializeField] private RectTransform origin;
    [SerializeField] private RectTransform dest;
    [SerializeField] private RectTransform rectT;

    //**    ---Variables---    **//
    private float distance;
    private float angle;

    //**    ---Properties---    **//
    public bool connected = false;
    public Color color;

    //**    ---Functions---    **//
    private void Start() {
        color = origin.GetComponent<Image>().color;
        GetComponent<Image>().color = color;
    }

    public void Setup(RectTransform o, RectTransform d, Color color) {
        origin = o; 
        dest = d;
        transform.position = origin.position;
        GetComponent<Image>().color = color;
    }

    public void UpdateSize() {
        Vector2 pos1 = origin.position;
        Vector2 pos2 = dest.position;

        Vector2 direction = pos2 - pos1;

        float angle = Vector2.Angle(Vector2.right, direction);
        
        if (direction.y < 0) angle = -angle;

        rectT.localEulerAngles = new Vector3(0, 0, angle);
        
        rectT.sizeDelta = new Vector2 (direction.magnitude/2, 10);
    }
    
    private void Update() {
        if (!connected) 
            UpdateSize();
    }
}
