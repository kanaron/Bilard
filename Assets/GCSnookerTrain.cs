using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GCSnookerTrain : MonoBehaviour
{

    public GameObject upCamera;
    public GameObject cueCamera;

    public Text endText;
    public Button endButton;

    public GameObject white;
    public List<GameObject> balls = new List<GameObject>();
    public List<GameObject> ballsClon = new List<GameObject>();
    public GameObject spawn;
    public int reds = 15;

    public float drag = 0.5f;
    public float angdrag = 0.1f;

    public GameObject b;

    public PhysicMaterial pmaterial;

    void inPit(int i)
    {
        if (reds != 0)
        {
            ballsClon[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ballsClon[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            ballsClon[i].transform.position = ballsClon[i].transform.parent.position;
            ballsClon[i].GetComponent<Trigger>().Ent = false;
        }
        else
        {
            Destroy(ballsClon[i]);

            ballsClon.RemoveAt(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Transform spawnt;
        for (int i = 0; i < spawn.transform.childCount; i++)
        {


            spawnt = spawn.transform.GetChild(i).GetComponent<Transform>();

            switch (spawnt.name)
            {
                case "Black":
                    {

                        for (int j = 0; j < balls.Count; j++)
                        {
                            //Debug.Log(balls[j].transform.name);

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {
                                
                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                case "Blue":
                    {
                        for (int j = 0; j < balls.Count; j++)
                        {

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {

                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                case "Yellow":
                    {
                        for (int j = 0; j < balls.Count; j++)
                        {

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {

                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                case "Pink":
                    {
                        for (int j = 0; j < balls.Count; j++)
                        {

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {

                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                case "Green":
                    {
                        for (int j = 0; j < balls.Count; j++)
                        {

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {

                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                case "Brown":
                    {
                        for (int j = 0; j < balls.Count; j++)
                        {

                            if (string.Equals(balls[j].transform.name, spawnt.name))
                            {

                                b = balls[j];
                            }
                        }

                        if (b != null)
                        {
                            b.GetComponent<Rigidbody>().drag = drag;
                            b.GetComponent<Rigidbody>().angularDrag = angdrag;
                            b.GetComponent<SphereCollider>().material = pmaterial;

                            ballsClon.Add(Instantiate(b, spawnt));
                            ballsClon.Last<GameObject>().AddComponent<Trigger>();
                        }
                        else
                        {
                            Debug.Log("Ni ma");
                        }
                        break;
                    }
                default:
                    {
                        if (spawnt.name != "White")
                        {
                            for (int j = 0; j < balls.Count; j++)
                            {

                                if (string.Equals(balls[j].transform.name, "Red"))
                                {

                                    b = balls[j];
                                }
                            }

                            if (b != null)
                            {
                                b.GetComponent<Rigidbody>().drag = drag;
                                b.GetComponent<Rigidbody>().angularDrag = angdrag;
                                b.GetComponent<SphereCollider>().material = pmaterial;

                                ballsClon.Add(Instantiate(b, spawnt));
                                ballsClon.Last<GameObject>().AddComponent<Trigger>();
                            }
                            else
                            {
                                Debug.Log("Ni ma");
                            }
                        }
                        break;
                    }
            }



        }

        foreach (GameObject b in ballsClon)
        {
            b.transform.tag = "Ball";
            b.transform.localScale = new Vector3(8, 8, 8);
            b.AddComponent<SphereCollider>();
            b.GetComponent<SphereCollider>().radius *= 2;
            b.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            upCamera.GetComponent<Camera>().orthographicSize -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            upCamera.GetComponent<Camera>().orthographicSize += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            upCamera.SetActive(!upCamera.activeSelf);
            cueCamera.SetActive(!cueCamera.activeSelf);
        }


        for (int i = 0; i < ballsClon.Count; i++)
        {
            if (ballsClon[i] != null)
                if (ballsClon[i].GetComponent<Trigger>().Ent)
                {

                    switch (ballsClon[i].transform.parent.transform.name )
                    {
                        case "Black":
                            {
                                inPit(i);

                                break;
                            }
                        case "Brown":
                            {
                                inPit(i);

                                break;
                            }
                        case "Blue":
                            {
                                inPit(i);

                                break;
                            }
                        case "Green":
                            {
                                inPit(i);

                                break;
                            }
                        case "Yellow":
                            {
                                inPit(i);

                                break;
                            }
                        case "Pink":
                            {
                                inPit(i);

                                break;
                            }
                        default:
                            {
                                Destroy(ballsClon[i]);
                                reds--;

                                ballsClon.RemoveAt(i);
                                break;
                            }
                    }

                    
                }


        }

        if (ballsClon.Count == 0)
        {
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            endButton.gameObject.SetActive(true);
            endText.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            endButton.gameObject.SetActive(!endButton.IsActive());
            
        }
    }

    public void Load()
    {
        SceneManager.LoadScene("Menu");
    }
}
