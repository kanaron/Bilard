using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GCSnookerPC : MonoBehaviour
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

            if (red)
            {
                AiShoot((ballsClon[ballsClon.Count - 1].transform.position - whiteBall.transform.position));
            }
            else
            {
                AiShoot((ballsClon[0].transform.position - whiteBall.transform.position));
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
                        if (red)
                        {
                            if (ball.transform.name != "Red(Clone)")
                            {
                                AiShoot();
                            }
                        }
                        else
                        {
                            if (ball.transform.name == "Red(Clone)")
                            {
                                AiShoot();
                            }
                        }
                    }
                }
            }
        }
    }


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
                    endText.text = "Player wins";
                    ISC.playerWins = 1;
                }
                else if (p1s < p2s)
                {
                    endText.text = "PC wins";
                    ISC.playerWins = -1;
                }
                else
                {
                    endText.text = "Draw";
                    ISC.playerWins = 0;
                }
                endButton.gameObject.SetActive(true);
                endText.gameObject.SetActive(true);

                ISC.endGame = true;
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

                if (player)
                {
                    canAI = true;
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
