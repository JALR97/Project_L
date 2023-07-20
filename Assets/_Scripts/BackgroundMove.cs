using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float moveVelocity;
    [SerializeField] private GameObject Sprite;
    private float spriteWidth, startPosition;
    //private GameObject cloneSprite;
   

    private void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
        //cloneSprite = Instantiate(Sprite);
        //cloneSprite.transform.position = new Vector3(spriteWidth * 2, transform.position.y,transform.position.z);
    }

    void LateUpdate()
    {
        transform.Translate(new Vector3(-moveVelocity * Time.deltaTime, 0, 0));
        //cloneSprite.transform.Translate(new Vector3(-moveVelocity * Time.deltaTime, 0, 0));
        float moveAmount = transform.position.x * (1 - moveVelocity);

        if(transform.position.x < startPosition - spriteWidth)
        {
            transform.position = new Vector3(startPosition,transform.position.y, transform.position.z);
            
        }
        }
}

