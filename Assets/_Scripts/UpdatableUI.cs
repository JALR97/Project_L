using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatableUI : MonoBehaviour
{
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private TMP_Text UItext;
    //  [[ set in Start() ]] 


    //**    ---Variables---    **//
    //  [[ balance control ]] 
    [SerializeField] private float hideTime;
    
    //  [[ internal work ]] 
    private float setPoint = 0;

    //**    ---Properties---    **//


    //**    ---Functions---    **//
    public void UpdateUI(int current) {
        UItext.text = current.ToString();
        setPoint = Time.time + hideTime;
        if(!gameObject.activeSelf) {
            StopAllCoroutines();
            gameObject.SetActive(true);
            StartCoroutine(Countdown());
        }
    }

    public IEnumerator Countdown() {
        while (Time.time < setPoint) {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}   
