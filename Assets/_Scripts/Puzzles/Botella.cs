using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Botella : MonoBehaviour
{
    //**    ---Components---    **//
    [SerializeField] private Image[] Liquids;
    [SerializeField] private GameObject botellaTemp;
    [SerializeField] private RectTransform dest;
    [SerializeField] private RectTransform rectT;


    //**    ---Variables---    **//
    private BotellaManager botellaTempScript;
    private RectTransform botellaRT;
    private float distance;
    private float angle;

    //**    ---Properties---    **//
    public float elevacionBotella;
    public Color[] color;

    //**    ---Functions---    **//
    void Start()
    {
        botellaTempScript = botellaTemp.GetComponent<BotellaManager>();
        botellaRT = gameObject.GetComponent<RectTransform>();
   
        for (int i = 0; i < color.Length; i++)
        {
            Color temp = color[i];
            int randomIndex = Random.Range(i, color.Length);
            color[i] = color[randomIndex];
            color[randomIndex] = temp;
        }
        for (int i = 0; i < color.Length; i++)
        {
            Liquids[i].color = color[i];
        }
    }
    public void OnClickBotella()
    {
        if (IsFirstSelection())
        {
            botellaRT.position = new Vector3(botellaRT.position.x, botellaRT.position.y + elevacionBotella, botellaRT.position.z);
            Debug.Log("h");

        }
    }
    private bool IsFirstSelection()
    {
        if (botellaTempScript != null)
        {
            Image[] colorTemp = botellaTempScript.getImage();
            int activo = 0;
            for (int i = 0; i < colorTemp.Length; i++)
            {
                Debug.Log(botellaTempScript.getImage()[i].color);

                if (colorTemp[i].color.a > 0)
                {
                    activo += 1;
                }
            }
            if (activo > 0)
                return false;
            
        }
        return true;
    }

 
    void Update()
    {
        
    }
}
