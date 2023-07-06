using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour

{
    //**    ---Components---    **//
    [SerializeField] private RectTransform origin;
    [SerializeField] private RectTransform dest;
    private RectTransform rectT;

    //**    ---Variables---    **//
    private float distance;
    private float angle;

    //**    ---Properties---    **//


    //**    ---Functions---    **//
    private void Start() {
        rectT = GetComponent<RectTransform>();
    }

    private void Update() {
        Vector2 pos1 = origin.position;
        Vector2 pos2 = dest.position;
        
        Vector2 direction = pos2 - pos1;

        float angle = Vector2.Angle(Vector2.right, direction);
        
        if (direction.y < 0) angle = -angle;

        rectT.localEulerAngles = new Vector3(0, 0, angle);
        
        rectT.sizeDelta = new Vector2 (direction.magnitude, 10);
    }
}
