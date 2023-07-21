using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BotellaManager : MonoBehaviour
{
    [SerializeField] private Image[] liq;

    private void Start()
    {
        reiniciarImage();
    }
    public Image[] getImage()
    {
        return liq;
    }

    public void putImage(Image l, int pos)
    {
        liq[pos] = l;
    }
    public void reiniciarImage()
    {
        liq = new Image[liq.Length];
    }
}
