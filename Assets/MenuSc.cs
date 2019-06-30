using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSc : MonoBehaviour
{

    public GameObject ISCObj;
    public InterSceneController ISC;

    public Text BallTournamentText;
    public Text BallTournamentInfo;

    public Text STournamentText;
    public Text STournamentInfo;

    public Button newTour;
    public Button startTour;
    public Dropdown drop;

    public Button SnewTour;
    public Button SstartTour;

    public GameObject e0, e1, e2;


    private void Start()
    {
        if (GameObject.Find("DontDestroy") == null)
        {
            ISC = Instantiate(ISCObj).GetComponent<InterSceneController>();
        }
        else
        {
            ISC = GameObject.Find("DontDestroy").GetComponent<InterSceneController>();
        }

        ISC.diffDrop = GameObject.Find("Dropdown").GetComponent<Dropdown>();

        newTour.onClick.AddListener(NewTournament);
        startTour.onClick.AddListener(StartTournament);
        drop.onValueChanged.AddListener(delegate { ValChang(drop); });

        SnewTour.onClick.AddListener(SNewTournament);
        SstartTour.onClick.AddListener(SStartTournament);
    }

    private void Update()
    {
        if (ISC.tournament)
        {
            if (ISC.endGame)
            {
                ISC.endGame = false;

                if (ISC.playerWins == 1)
                {
                    ISC.toWin8Ball--;
                }
                else if (ISC.playerWins == -1)
                {
                    BallTournamentText.text = "You lost in ";

                    if (ISC.toWin8Ball < 2)
                    {
                        BallTournamentText.text += "finals";
                    }
                    else if (ISC.toWin8Ball < 4)
                    {
                        BallTournamentText.text += "half-finals";
                    }
                    else if (ISC.toWin8Ball < 7)
                    {
                        BallTournamentText.text += "quoter-finals";
                    }
                    else
                    {
                        BallTournamentText.text += "eliminations";
                    }

                    ISC.toWin8Ball = 10;
                }
            }
        }
        else if (ISC.STournament)
        {
            if (ISC.endGame)
            {
                ISC.endGame = false;

                if (ISC.playerWins == 1)
                {
                    ISC.toWinS--;
                }
                else if (ISC.playerWins == -1)
                {
                    STournamentText.text = "You lost in ";

                    if (ISC.toWinS < 2)
                    {
                        STournamentText.text += "finals";
                    }
                    else if (ISC.toWinS < 4)
                    {
                        STournamentText.text += "half-finals";
                    }
                    else if (ISC.toWinS < 7)
                    {
                        STournamentText.text += "quoter-finals";
                    }
                    else
                    {
                        STournamentText.text += "eliminations";
                    }

                    ISC.toWinS = 10;
                }
            }
        }
        else
        {
            ISC.endGame = false;
            ISC.playerWins = 0;
        }

        if (ISC.toWin8Ball == 0)
        {
            BallTournamentText.text = "You won 8 Ball Tournament";
        }

        if (ISC.toWinS == 0)
        {
            STournamentText.text = "You won Snooker Tournament";
        }

        if (ISC.toWin8Ball < 2)
        {
            BallTournamentInfo.text = "Finals";
        }
        else if (ISC.toWin8Ball < 4)
        {
            BallTournamentInfo.text = "Half-finals";
        }
        else if (ISC.toWin8Ball < 7)
        {
            BallTournamentInfo.text = "Quoter-finals";
        }
        else
        {
            BallTournamentInfo.text = "Eliminations";
        }

        if (ISC.toWinS < 2)
        {
            STournamentInfo.text = "Finals";
        }
        else if (ISC.toWinS < 4)
        {
            STournamentInfo.text = "Half-finals";
        }
        else if (ISC.toWinS < 7)
        {
            STournamentInfo.text = "Quoter-finals";
        }
        else
        {
            STournamentInfo.text = "Eliminations";
        }

        if (drop.value == 4)
        {
            e0.SetActive(true);
            e1.SetActive(true);
            e2.SetActive(true);
        }
        else
        {
            e0.SetActive(false);
            e1.SetActive(false);
            e2.SetActive(false);
        }
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        ISC.SaveFile();
        Application.Quit();
    }

    void NewTournament()
    {
        ISC.Restart8Ball();
    }

    void StartTournament()
    {
        ISC.Start8BallToournament();
    }

    void SNewTournament()
    {
        ISC.RestartS();
    }

    void SStartTournament()
    {
        ISC.StartSnookerToournament();
    }

    void ValChang(Dropdown chang)
    {
        ISC.difficultyChange();
    }
}
