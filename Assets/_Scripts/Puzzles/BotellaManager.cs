using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BotellaManager : MonoBehaviour
{
    [SerializeField] private Image[] liq;
    public Color[] color;
    public Image[] getImage()
    {
        return liq;
    }
}
