using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GC8Train : MonoBehaviour
{

    public GameObject upCamera;
    public GameObject cueCamera;

    public Text endText;
    public Button endButton;

    public GameObject white;
    public GameObject[] balls = new GameObject[15];
    public List<GameObject> ballsClon = new List<GameObject>();
    public GameObject spawn;

    public float drag = 0.5f;
    public float angdrag = 0.1f;


    public PhysicMaterial pmaterial;

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

                    Destroy(ballsClon[i]);

                    ballsClon.RemoveAt(i);
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
