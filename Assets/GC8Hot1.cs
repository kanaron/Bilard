using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GC8Hot1 : MonoBehaviour
{
    public GameObject upCamera;
    public GameObject cueCamera;

    public bool player = false;

    public Text endText;
    public Button endButton;

    public Text plText;
    public Text cueText;
    public Text plTextFoul;
    public Text cueTextFoul;
    public Text p1b, p2b;
    bool mov = false;
    bool hit = false;
    public GameObject whiteBall;

    public GameObject[] balls = new GameObject[15];
    public List<GameObject> ballsClon = new List<GameObject>();
    public GameObject spawn;

    public float drag = 0.5f;
    public float angdrag = 0.1f;


    public PhysicMaterial pmaterial;


    public List<int> p1 = new List<int>();
    public List<int> p2 = new List<int>();
    public bool first = true;

    public bool willFoul = false;
    public bool foul = false;
    public bool good = false;
    public int moveSpeed = 5;

    float ct;


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        Transform spawnt;
        foreach (GameObject b in balls)
        {
            i++;

            spawnt = spawn.transform.Find(i.ToString()).GetComponent<Transform>();

            b.transform.position = new Vector3(0, 0, 0);
            b.GetComponent<Rigidbody>().drag = drag;
            b.GetComponent<Rigidbody>().angularDrag = angdrag;
            b.GetComponent<SphereCollider>().material = pmaterial;


            ballsClon.Add(Instantiate(b, spawnt));
            ballsClon[i - 1].AddComponent<Trigger>();
            ballsClon[i - 1].GetComponent<Rigidbody>().maxAngularVelocity = 200;
            ballsClon[i - 1].transform.localScale = new Vector3(8, 8, 8);
            ballsClon[i - 1].transform.tag = "Ball";

            ballsClon[i - 1].AddComponent<SphereCollider>();
            ballsClon[i - 1].GetComponent<SphereCollider>().radius *= 2;
            ballsClon[i - 1].GetComponent<SphereCollider>().isTrigger = true;
        }
    }


    void Update()
    {
        cueTextFoul.gameObject.SetActive(willFoul);
        plTextFoul.gameObject.SetActive(willFoul);

        p1b.text = null;
        p2b.text = null;

        for (int i = 0; i < p1.Count; i++)
        {
            p1b.text += p1[i];
            p1b.text += "\n";
        }
        for (int i = 0; i < p2.Count; i++)
        {
            p2b.text += p2[i];
            p2b.text += "\n";
        }

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            endButton.gameObject.SetActive(!endButton.IsActive());

        }

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

                        string name = ballsClon[i].transform.parent.name;
                        int numb = int.Parse(name);

                        if (first)
                        {
                            first = false;

                            if ((!player && numb < 8) || ((player && numb > 8)))
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    p1.Add(j + 1);
                                    p2.Add(j + 9);
                                }
                            }
                            else if ((player && numb < 8) || ((!player && numb > 8)))
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    p2.Add(j + 1);
                                    p1.Add(j + 9);
                                }
                            }
                        }

                        if (!player && p1.Count > 0 && p1[0] > 8 && numb > 8)
                        {
                            p1.Remove(numb);
                            good = true;
                        }
                        else if (!player && p1.Count > 0 && p1[0] > 8 && numb < 8)
                        {
                            p2.Remove(numb);
                            if (!good)
                                willFoul = true;
                        }
                        else if (!player && p1.Count > 0 && p1[0] < 8 && numb > 8)
                        {
                            p2.Remove(numb);
                            if (!good)
                                willFoul = true;
                        }
                        else if (!player && p1.Count > 0 && p1[0] < 8 && numb < 8)
                        {
                            p1.Remove(numb);
                            good = true;
                        }
                        else if (player && p2.Count > 0 && p2[0] > 8 && numb > 8)
                        {
                            p2.Remove(numb);
                            good = true;
                        }
                        else if (player && p2.Count > 0 && p2[0] > 8 && numb < 8)
                        {
                            p1.Remove(numb);
                            if (!good)
                                willFoul = true;
                        }
                        else if (player && p2.Count > 0 && p2[0] < 8 && numb > 8)
                        {
                            p1.Remove(numb);
                            if (!good)
                                willFoul = true;
                        }
                        else if (player && p2.Count > 0 && p2[0] < 8 && numb < 8)
                        {
                            p2.Remove(numb);
                            good = true;
                        }

                        if (numb == 8)
                        {
                            if (!player && p1.Count > 0)
                            {
                                endText.text = "Player 2 wins";
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (player && p2.Count > 0)
                            {
                                endText.text = "Player 1 wins";
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (!player && p1.Count == 0)
                            {
                                endText.text = "Player 1 wins";
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (player && p2.Count == 0)
                            {
                                endText.text = "Player 2 wins";
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }

                        }

                        Destroy(ballsClon[i]);

                        ballsClon.RemoveAt(i);


                    }

                }
            }



            if (hit && !mov)
            {
                hit = false;

                if (willFoul)
                {
                    willFoul = false;
                    foul = true;
                    good = false;
                }

                if (!good)
                {
                    player = !player;
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