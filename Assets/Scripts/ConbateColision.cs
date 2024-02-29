using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //if(collider.gameObject.tag == "Inimigo")

        if (collider.GetComponent<EnemyController>() != null)
        {
            collider.GetComponent<EnemyController>().receberDano(1);

        }

        if (collider.GetComponent<PlayerManager>() != null)
        {
            collider.GetComponent<PlayerManager>().receberDano(1);

        }

    }
}