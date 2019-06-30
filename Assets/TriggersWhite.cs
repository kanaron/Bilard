using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersWhite : MonoBehaviour
{

    public Transform spawn;

    public bool enter = false;

    public GC8Hot1 gc;
    public GC8PC gcp;
    public GCSnookerHot gs;
    public GCSnookerPC gsp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pit")
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            this.transform.position = spawn.position;

            enter = true;

            if (gc != null)
            {
                gc.willFoul = true;
            }
            if (gcp != null)
            {
                gcp.willFoul = true;
            }
            if (gs != null)
            {
                gs.willFoul = true;
            }
            if (gsp != null)
            {
                gsp.willFoul = true;
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
