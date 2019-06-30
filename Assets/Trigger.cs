using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public bool Ent = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pit")
        {
            Ent = true;
        }
    }
}
