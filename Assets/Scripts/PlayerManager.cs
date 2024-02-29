using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int vidaTotal = 5;
    public int vidaAtual;

    void Start()
    {
        vidaAtual = vidaTotal;
    }

    public void receberDano(int valor)
    {
        vidaAtual -= valor;
        verficaMorte();
    }

    void verficaMorte()
    {
        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }
}