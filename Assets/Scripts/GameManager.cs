using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int whoturn;
    public int turnCount;
    public GameObject[] turnIcons;
    public Sprite[] playerIcons;
    public Button[] tictactoebtns;
    public int[] markedSpaces;
    public TextMeshProUGUI winText,xScoreText, oScoreText;
    public GameObject[] winningLines;
    public GameObject winnerPannel;
    public int xScore = 0, oScore = 0;
    public Button xButton, oButton;

    void Start()
    {
        GameSetUp();
    }

    void GameSetUp()
    {
        whoturn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for(int i = 0; i< tictactoebtns.Length;i++)
        {
            tictactoebtns[i].interactable = true;
            tictactoebtns[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -1000;
        }
    }
    // Update is called once per frame
   public void TicTacToeButton(int whichNumber)
    {
        xButton.interactable = false;
        oButton.interactable = false;
        tictactoebtns[whichNumber].image.sprite = playerIcons[whoturn];
        tictactoebtns[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoturn+1;
        turnCount++;

        if(turnCount > 4)
        {
            bool isWon = winnerCheck();
            if (turnCount == 9 && isWon == false)
            {
                Draw();
            }
        }

        if (whoturn == 0)
        {
            whoturn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else { whoturn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool winnerCheck() {

        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i =0;i< solutions.Length; i++)
        {
            if (solutions[i]== 3*(whoturn +1))
            {
                winnerDisplay(i);
                return true;
            }    
        }
        return false;
    }

    void winnerDisplay(int solNumber)
    {
        winnerPannel.SetActive(true);
        if (whoturn == 0)
        {
            winText.text = "Player X wins";
            xScore++;
            xScoreText.text = xScore.ToString();

        }
        else if(whoturn == 1)
        {
            winText.text = "Player O wins";
            oScore++;
            oScoreText.text = oScore.ToString();
        }
        winningLines[solNumber].SetActive(true);
    }


    public void Rematch()
    {
        GameSetUp();
        for(int i = 0; i< winningLines.Length;i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPannel.SetActive(false);
        xButton.interactable = true;
        oButton.interactable = true;
    }

    public void Restart()
    {
        Rematch();
        xScore = 0;
        oScore = 0;
        xScoreText.text = "0";
        oScoreText.text = "0";
    }

    public void switchPlay(int whichPlayer)
    {
        if(whichPlayer == 0)
            {
            whoturn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);

        }
        else if(whichPlayer == 1)
        {
            whoturn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void Draw()
    {
        winnerPannel.SetActive(true);
        winText.text = "Draw";
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
