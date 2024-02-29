using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Transform colisaoSoco;

    Animator animator;

    int combo = 0;
    int comboTotal = 3;

    bool podeAtacar = true;

    void Start()
    {
        colisaoSoco = transform.Find("ColisaoSoco");
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        if (InputController.inputAcaoPrincipal == true && podeAtacar == true)
        {
            realizaGolpe();
        }

    }

    void realizaGolpe()
    {
        combo++;
        animator.SetInteger("comboSoco", combo);

        podeAtacar = false;

        if (combo >= comboTotal)
        {
            combo = 0;
        }

    }

    void fimDoGolpe()
    {
        podeAtacar = true;
        animator.SetInteger("comboSoco", 0);
    }

    void habilitaColisaoGolpe(bool habilitar)
    {
        colisaoSoco.GetComponent<Collider>().enabled = false;
        colisaoSoco.GetComponent<MeshRenderer>().enabled = false;
    }
}