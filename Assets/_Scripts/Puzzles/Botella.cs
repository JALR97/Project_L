using UnityEngine;
using UnityEngine.UI;
public class Botella : MonoBehaviour
{
    //**    ---Components---    **//
    [SerializeField] private Image[] Liquids;
    [SerializeField] private GameObject botellaTemp;
    [SerializeField] private GameObject pLiquidSort;

    //**    ---Variables---    **//
    private BotellaManager botellaTempScript;
    private Image LiquidTemp;
    private int posicionVacia;
    private int posicionOcupada;
    private PuzzleLiquidSort pls;
    private bool completado_color;
    //**    ---Properties---    **//


    //**    ---Functions---    **//
    void Start()
    {
        botellaTempScript = botellaTemp.GetComponent<BotellaManager>();
        pls = pLiquidSort.GetComponent<PuzzleLiquidSort>();
    }
    public void OnClickBotella()
    {
        if (IsFirstSelection())
        {
            for (int i = 0; i < Liquids.Length; i++)
            {
                botellaTempScript.putImage(Liquids[i], i);
            }
        }
        else
        {


            Image[] botellaTempLiquid = botellaTempScript.getImage();

            posicionVacia = PosicionLibre(Liquids);
            posicionOcupada= PosicionOcupada(botellaTempLiquid);
            Debug.Log(posicionVacia);
            int posPosterior = posicionVacia == 3 ? 0 : 1;
            if (posicionVacia == Liquids.Length - 1 || Liquids[posicionVacia + posPosterior].color != botellaTempLiquid[posicionOcupada].color)
            {
                LiquidTemp = botellaTempLiquid[posicionOcupada];
                Liquids[posicionVacia].color = LiquidTemp.color;
                Color nuevoColor = LiquidTemp.color;
                nuevoColor.a = 0f;
                LiquidTemp.color = nuevoColor;
                //InvokeRepeating("ModificarOpacidad", 0.5f, 1f);
                if( posicionVacia == 0)
                {
                    pls.completado += 1;
                    completado_color = true;
                }
                if(pls.completado == Liquids.Length -1)
                {
                    pls.Completed();
                }
                if(posicionOcupada == 0 && completado_color == true)
                {
                    pls.completado -=1;
                    completado_color = false;
                }

            }
            botellaTempScript.reiniciarImage();

        }

    }
    private int PosicionOcupada(Image[] a)
    {
        int posicion_ocupada = -1;
        int i = 0;
        while(i  < a.Length && posicion_ocupada == -1)
        {
            if (a[i].color.a >0)
            {
                posicion_ocupada = i;
            }
            i += 1;
        }
        return posicion_ocupada;
    }
    private int PosicionLibre(Image[] a)
    {
        int posicion_libre = -1;
        for(int i = 0; i< a.Length; i++)
        {
            if(a[i].color.a == 0)
            {
                posicion_libre = i;
            }
        }
        return posicion_libre;
    }
     
    private void ModificarOpacidad()
    {
        Debug.Log("1");
        float nuevaOpacidad = LiquidTemp.color.a - 0.1f;

        // Asegurarse de que la nueva opacidad no sea menor que cero.
        nuevaOpacidad = Mathf.Max(nuevaOpacidad, 0f);

        // Actualizar la opacidad del componente Image.
        Color nuevoColor = LiquidTemp.color;
        nuevoColor.a = nuevaOpacidad;
        LiquidTemp.color = nuevoColor;

        float nuevaOpacidad2 = Liquids[posicionVacia].color.a + 0.1f;

        // Asegurarse de que la nueva opacidad no sea menor que cero.
        nuevaOpacidad = Mathf.Min(nuevaOpacidad, 1f);

        // Actualizar la opacidad del componente Image.
        Color nuevoColor2 = Liquids[posicionVacia].color;
        nuevoColor2.a = nuevaOpacidad2;
        Liquids[posicionVacia].color = nuevoColor2;

        if (nuevaOpacidad <= 0f || nuevaOpacidad2 >= 1f)
        {
            CancelInvoke("ModificarOpacidad");
        }
    }
    private bool IsFirstSelection()
    {

        Image[] colorTemp = botellaTempScript.getImage();
        int activo = 0;
        if (colorTemp[0] != null)
        {
            for (int i = 0; i < colorTemp.Length; i++)
            {
                

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

 

}
