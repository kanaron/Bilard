using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycasting : MonoBehaviour
{

    public Vector3 offsetPosition;
    Quaternion stickRot;

    public GameObject stick;
    LineRenderer LR;
    Vector3 line, endPoint;

    public bool moving = false;
    Transform last;


    public float hitForce;
    public float maxHit;
    //public Text hitText;
    //public Text mainHitText;
    public Slider hitCue;
    public Slider hitMain;
    public int hitSpeed;
    public bool space = false;
    public bool readyToHit = false;
    public string txt = "Hit force: ";

    public RaycastHit hit, h;

    // Start is called before the first frame update
    void Start()
    {
        hitSpeed = 50;
        maxHit = 30;

        LR = this.GetComponent<LineRenderer>();
        last = this.transform;
        last.position = transform.position;
        stickRot = stick.transform.rotation;

        this.GetComponent<Rigidbody>().maxAngularVelocity = 200;

        hitCue.maxValue = maxHit;
        hitMain.maxValue = maxHit;
    }

    public void Hit(float hitForce)
    {
        line.y = 0;
        line.Normalize();
        this.GetComponent<Rigidbody>().AddForce(line * hitForce, ForceMode.Impulse);
        readyToHit = true;
    }

    public void Hit(float hitForce, Vector3 vec)
    {
        vec.Normalize();
        this.GetComponent<Rigidbody>().AddForce(vec * hitForce, ForceMode.Impulse);
    }

    Vector3 DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {

        Ray ray = new Ray(startPos, direction);

        Vector3 endPos = startPos + (distance * direction);

        if (Physics.Raycast(ray, out hit, distance))
        {
            endPos = hit.point;

            if (hit.transform.tag == "Ball")
            {
                Vector3 predict = hit.normal * -1;
                Physics.Raycast(hit.collider.transform.position, predict, out h, 1000);

                Debug.DrawLine(hit.collider.transform.position, h.point, Color.red, 2);
            }

        }

        Debug.DrawLine(startPos, endPos, Color.green, 2);


        return endPos;
    }

    bool IsMoving()
    {
        if (this.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
        {
            return false;
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        line = (this.transform.position - stick.transform.position) / 5;
        line.y = 0;

        endPoint = DetectHit(transform.position, 30, line);

        //endPoint -= this.transform.position;

        LR.SetPosition(0, this.transform.position);
        LR.SetPosition(1, endPoint);

        if (Input.GetKeyDown(KeyCode.Space) && !moving && space && stick.GetComponent<MeshRenderer>().enabled == true)
        {
            Hit(hitForce);
            stick.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<LineRenderer>().enabled = false;
            space = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !moving && !space && stick.GetComponent<MeshRenderer>().enabled == true)
        {
            hitForce = 2;
            hitCue.value = 2;
            hitMain.value = 2;
            space = true;
        }

        if(space)
        {
            hitForce += hitSpeed * Time.deltaTime;

            hitCue.value = hitForce;
            hitMain.value = hitForce;
            
            if (hitForce > maxHit || hitForce < 1)
            {
                hitSpeed *= -1;
            }
        }

        if (moving)
        {
            stick.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<LineRenderer>().enabled = false;
        }


        if (!IsMoving() && moving)
        {
            stick.transform.position = this.transform.position + offsetPosition;
            stick.transform.rotation = stickRot;
            stick.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<LineRenderer>().enabled = true;
        }

        moving = IsMoving();



    }
}
