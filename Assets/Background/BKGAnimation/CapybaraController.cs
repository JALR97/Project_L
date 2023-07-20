using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraController : MonoBehaviour
{
    [SerializeField] private float moveVelocity;
    [SerializeField] private float direction;
    Dictionary<string, string[]> estados = new Dictionary<string, string[]>() { { "idle", new string[] { "munch","walk","keep" } }, { "munch", new string[] { "idle", "keep", "keep" } }, { "walk", new string[] {"sitting_idle", "keep","idle" } }, { "sitting_idle", new string[] {"walk", "keep", "keep"} }  };
    private string estadoActivo;
    private Animator obj_Animator;
    private SpriteRenderer spriteRenderer;

    private float  tiempoInicial;
    [SerializeField] private float tiempoEsperaAnimacion;
    private void Start()
    {
        tiempoInicial = Time.time;
        estadoActivo = "idle";

        obj_Animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction == -1 ? true : false;
        CambiarAnimacion();
    }
    private void Update()
    {
        float tiempoTranscurrido = Time.time - tiempoInicial;

        if(tiempoTranscurrido >= tiempoEsperaAnimacion)
        {
            CambiarAnimacion();
            tiempoInicial = Time.time;
        }
    }

    void FixedUpdate()
    {
        

        if (estadoActivo == "walk")
        {
            if (transform.position.x < -13 || transform.position.x >10)
            {
                direction = direction * -1;
                spriteRenderer.flipX = direction == -1 ? true : false;
            }
            transform.position = new Vector3(transform.position.x + moveVelocity * direction * Time.deltaTime, transform.position.y, transform.position.z);

        }
    }
    private void CambiarAnimacion(){


            int cambioEstado = Random.Range(0, 2);
            if (estados[estadoActivo][cambioEstado] != "keep")
            {
                obj_Animator.SetBool(estadoActivo, false);
                obj_Animator.SetBool(estados[estadoActivo][cambioEstado], true);
                estadoActivo = estados[estadoActivo][cambioEstado];
                
            }
    }

}
