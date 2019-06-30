using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GC8PC : MonoBehaviour
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

    public GameObject ball, predict;
    public bool canAI = false;
    public bool shootAnywhere = false;


    public int stupidity = 0;
    public Vector3 forward;

    public InterSceneController ISC;


    public void AiShoot()
    {
        willFoul = false;
        cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;
        whiteBall.GetComponent<LineRenderer>().enabled = true;
        forward = whiteBall.transform.position - cueCamera.transform.parent.transform.position;
        forward.y = 0;
        forward = Quaternion.AngleAxis(Random.Range(-stupidity, stupidity), Vector3.up) * forward;
        whiteBall.GetComponent<Raycasting>().Hit(25, forward);
        canAI = false;
        ct = Time.time + 1;
        shootAnywhere = false;
    }

    public void AiShoot(Vector3 v)
    {
        willFoul = false;
        cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;
        whiteBall.GetComponent<LineRenderer>().enabled = true;
        whiteBall.GetComponent<Raycasting>().Hit(25, v);
        canAI = false;
        ct = Time.time + 1;
        shootAnywhere = false;
    }

    public void AI()
    {
        ball = null;
        predict = null;
        cueCamera.transform.parent.transform.RotateAround(whiteBall.transform.position, new Vector3(0, -1, 0), 10 * Time.deltaTime);

        if (cueCamera.transform.parent.transform.eulerAngles.y > 270 && cueCamera.transform.parent.transform.eulerAngles.y < 271)
        {
            shootAnywhere = true;
        }

        try
        {
            ball = whiteBall.GetComponent<Raycasting>().hit.transform.gameObject;
            predict = whiteBall.GetComponent<Raycasting>().h.transform.gameObject;
        }
        catch
        {

        }

        if (shootAnywhere)
        {
            if (first)
            {
                AiShoot((ballsClon[0].transform.position - whiteBall.transform.position));
            }
            else
            {
                if (p2.Count == 0)
                {
                    if (p1.Count == 0)
                    {
                        AiShoot((ballsClon[0].transform.position - whiteBall.transform.position));
                    }
                    else if (p1[0] > 8)
                    {
                        AiShoot((ballsClon[0].transform.position - whiteBall.transform.position));
                    }
                    else
                    {
                        AiShoot((ballsClon[ballsClon.Count - 1].transform.position - whiteBall.transform.position));
                    }
                }
                else
                {
                    if (p2[0] < 8)
                    {
                        AiShoot((ballsClon[0].transform.position - whiteBall.transform.position));
                    }
                    else
                    {
                        AiShoot((ballsClon[ballsClon.Count - 1].transform.position - whiteBall.transform.position));
                    }
                }
            }
        }
        else
        {
            if (ball != null && predict != null && ball.transform.parent != null)
            {
                if (ball.tag == "Ball")
                {
                    if (predict.tag == "Pit")
                    {
                        if (ball.transform.parent.name != "8" && first)
                        {
                            AiShoot();
                        }
                        else if (!first)
                        {
                            if (p2.Count == 0)
                            {
                                if (ball.transform.parent.name == "8")
                                {
                                    AiShoot();
                                }
                            }
                            else
                            {
                                int numb = int.Parse(ball.transform.parent.name);

                                if (p2[0] < 8 && numb < 8)
                                {
                                    AiShoot();
                                }
                                else if (p2[0] > 8 && numb > 8)
                                {
                                    AiShoot();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

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

        ISC = GameObject.Find("DontDestroy").GetComponent<InterSceneController>();
        switch (ISC.difficulty)
        {
            case 0:
                {
                    stupidity = 10;
                    break;
                }
            case 1:
                {
                    stupidity = 6;
                    break;
                }
            case 2:
                {
                    stupidity = 4;
                    break;
                }
            case 3:
                {
                    stupidity = 2;
                    break;
                }
            case 4:
                {
                    stupidity = 0;
                    break;
                }
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

        if (!foul)
        {

            if (player && canAI)
            {
                AI();
            }

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

                if (player)
                {
                    canAI = true;
                }

                cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;

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

                        //Debug.Log(numb);

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
                                endText.text = "PC wins";
                                ISC.playerWins = -1;
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (player && p2.Count > 0)
                            {
                                endText.text = "Player 1 wins";
                                ISC.playerWins = 1;
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (!player && p1.Count == 0)
                            {
                                endText.text = "Player 1 wins";
                                ISC.playerWins = 1;
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }
                            else if (player && p2.Count == 0)
                            {
                                endText.text = "PC wins";
                                ISC.playerWins = -1;
                                endText.gameObject.SetActive(true);
                                endButton.gameObject.SetActive(true);
                            }

                            ISC.endGame = true;
                        }

                        Destroy(ballsClon[i]);

                        ballsClon.RemoveAt(i);


                    }

                }
            }

            if (!player)
            {
                plText.text = "Player 1";
                cueText.text = "Player 1";
            }
            else
            {
                plText.text = "PC";
                cueText.text = "PC";
            }
        }
        else
        {
            willFoul = false;
            upCamera.SetActive(true);
            cueCamera.SetActive(false);
            cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            whiteBall.GetComponent<LineRenderer>().enabled = false;

            if (!player)
            {
                foreach (GameObject b in ballsClon)
                {
                    b.GetComponent<Rigidbody>().isKinematic = true;
                }
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

            if (player)
            {
                foul = false;
                cueCamera.transform.parent.GetComponent<MeshRenderer>().enabled = true;
                whiteBall.GetComponent<LineRenderer>().enabled = true;
            }

        }
    }

    public void Load()
    {
        SceneManager.LoadScene("Menu");
    }
}