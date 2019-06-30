using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GCSnookerHot : MonoBehaviour
{
    public bool player = false;

    public GameObject upCamera;
    public GameObject cueCamera;

    public Text endText;
    public Button endButton;

    public GameObject whiteBall;
    public List<GameObject> balls = new List<GameObject>();
    public List<GameObject> ballsClon = new List<GameObject>();
    public GameObject spawn;
    public int reds = 15;

    public float drag = 0.5f;
    public float angdrag = 0.1f;

    public GameObject b;

    public PhysicMaterial pmaterial;

    public bool red = false;

    public Text plText;
    public Text cueText;
    public Text plTextFoul;
    public Text cueTextFoul;
    public Text p1b, p2b;
    public int p1s = 0;
    public int p2s = 0;


    bool mov = false;
    bool hit = false;
    public bool willFoul = false;
    public bool foul = false;
    public bool good = false;
    public int moveSpeed = 5;

    float ct;

    void inPit(int i)
    {
        if (reds != 0)
        {
            ballsClon[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ballsClon[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            ballsClon[i].transform.position = ballsClon[i].transform.parent.position;
            ballsClon[i].GetComponent<Trigger>().Ent = false;

            if (!red)
            {
                willFoul = true;
            }

            red = false;
            good = true;
        }
        else
        {
            good = true;
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

        foul = true;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            endButton.gameObject.SetActive(!endButton.IsActive());

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            upCamera.SetActive(!upCamera.activeSelf);
            cueCamera.SetActive(!cueCamera.activeSelf);
        }

        cueTextFoul.gameObject.SetActive(willFoul);
        plTextFoul.gameObject.SetActive(willFoul);

        p1b.text = p1s.ToString();
        p2b.text = p2s.ToString();

        if (!foul)
        {

            mov = whiteBall.GetComponent<Raycasting>().moving;

            if (!mov)
            {
                foreach (GameObject b in ballsClon)
                {
                    if (b.GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
                    {
                        mov = true;
                    }
                }
            }

            if (whiteBall.GetComponent<Raycasting>().readyToHit)
            {
                ct = Time.time + 1;
                whiteBall.GetComponent<Raycasting>().readyToHit = false;
            }

            if (ct > Time.time)
            {
                hit = true;
                mov = true;
            }

            for (int i = 0; i < ballsClon.Count; i++)
            {
                if (ballsClon[i] != null)
                {
                    if (ballsClon[i].GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
                    {
                        mov = true;
                        cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = false;
                    }

                    if (ballsClon[i].GetComponent<Trigger>().Ent)
                    {
                        switch (ballsClon[i].transform.parent.transform.name)
                        {
                            case "Black":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 7;
                                        }
                                        else
                                        {
                                            p2s += 7;
                                        }
                                    break;
                                }
                            case "Brown":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 4;
                                        }
                                        else
                                        {
                                            p2s += 4;
                                        }
                                    break;
                                }
                            case "Blue":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 5;
                                        }
                                        else
                                        {
                                            p2s += 5;
                                        }
                                    break;
                                }
                            case "Green":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 3;
                                        }
                                        else
                                        {
                                            p2s += 3;
                                        }
                                    break;
                                }
                            case "Yellow":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 2;
                                        }
                                        else
                                        {
                                            p2s += 2;
                                        }
                                    break;
                                }
                            case "Pink":
                                {
                                    inPit(i);
                                    if (!willFoul)
                                        if (!player)
                                        {
                                            p1s += 6;
                                        }
                                        else
                                        {
                                            p2s += 6;
                                        }
                                    break;
                                }
                            default:
                                {
                                    Destroy(ballsClon[i]);
                                    reds--;
                                    red = true;
                                    good = true;

                                    if (!player)
                                    {
                                        p1s++;
                                    }
                                    else
                                    {
                                        p2s++;
                                    }

                                    ballsClon.RemoveAt(i);
                                    break;
                                }
                        }
                    }

                }
            }

            if (ballsClon.Count == 0)
            {
                upCamera.SetActive(true);
                cueCamera.SetActive(false);
                if (p1s > p2s)
                {
                    endText.text = "Player 1 wins";
                }
                else if (p1s < p2s)
                {
                    endText.text = "Player 2 wins";
                }
                else
                {
                    endText.text = "Draw";
                }
                endButton.gameObject.SetActive(true);
                endText.gameObject.SetActive(true);
            }

            if (hit && !mov)
            {
                hit = false;

                if (willFoul)
                {
                    willFoul = false;
                    foul = true;
                    good = false;
                    red = false;
                }

                if (!good)
                {
                    player = !player;
                    red = false;
                }
                else
                {
                    good = false;
                }

                cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;

            }

            if (!player)
            {
                plText.text = "Player 1";
                cueText.text = "Player 1";
            }
            else
            {
                plText.text = "Player 2";
                cueText.text = "Player 2";
            }

        }
        else
        {
            willFoul = false;
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            whiteBall.GetComponent<LineRenderer>().enabled = false;

            foreach (GameObject b in ballsClon)
            {
                b.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (GameObject b in ballsClon)
                {
                    b.GetComponent<Rigidbody>().isKinematic = false;
                }

                foul = false;
                cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;
                whiteBall.GetComponent<LineRenderer>().enabled = true;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                whiteBall.transform.Translate(0, 0, moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                whiteBall.transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                whiteBall.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                whiteBall.transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
            }

            whiteBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            whiteBall.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }

    }

    public void Load()
    {
        SceneManager.LoadScene("Menu");
    }
}
