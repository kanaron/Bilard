using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class InterSceneController : MonoBehaviour
{
    public string patch = "/save.txt";
    public string filePatch;

    public string SPatch = "/saveS.txt";
    public string SFilePatch;

    public int difficulty = 0;
    public Dropdown diffDrop;

    public int playerWins = 0;
    public bool endGame = false;
    public bool tournament = false;

    public bool STournament = false;

    public int toWin8Ball;

    public int toWinS;


    public void difficultyChange()
    {
        difficulty = diffDrop.value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.name = "DontDestroy";

        DontDestroyOnLoad(this.gameObject);

        filePatch = Application.dataPath + patch;
        SFilePatch = Application.dataPath + SPatch;

        if (File.Exists(filePatch))
        {
            toWin8Ball = int.Parse(File.ReadAllText(filePatch));
        }
        else
        {
            toWin8Ball = 10;
        }

        if (File.Exists(SFilePatch))
        {
            toWinS = int.Parse(File.ReadAllText(SFilePatch));
        }
        else
        {
            toWinS = 10;
        }
    }

    public void Restart8Ball()
    {
        toWin8Ball = 10;
    }

    public void RestartS()
    {
        toWinS = 10;
    }

    public void Start8BallToournament()
    {
        tournament = true;

        if (toWin8Ball < 2)
        {
            difficulty = 4;
        }
        else if (toWin8Ball < 4)
        {
            difficulty = 3 + Random.Range(0, 1);
        }
        else if (toWin8Ball < 7)
        {
            difficulty = 2 + Random.Range(0, 1);
        }
        else
        {
            difficulty = 0 + Random.Range(0, 1);
        }

        SceneManager.LoadScene("8PC");
    }

    public void StartSnookerToournament()
    {
        STournament = true;

        if (toWinS < 2)
        {
            difficulty = 4;
        }
        else if (toWinS < 4)
        {
            difficulty = 3 + Random.Range(0, 1);
        }
        else if (toWinS < 7)
        {
            difficulty = 2 + Random.Range(0, 1);
        }
        else
        {
            difficulty = 0 + Random.Range(0, 1);
        }

        SceneManager.LoadScene("SnookerPC");
    }

    public void SaveFile()
    {
        File.WriteAllText(filePatch, toWin8Ball.ToString());
        File.WriteAllText(SFilePatch, toWinS.ToString());

    }
}
