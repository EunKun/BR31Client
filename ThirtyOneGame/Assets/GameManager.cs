using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager ins;

    [System.Serializable]
    public class Obj
    {
        public GameObject charaSelectPanel;
        public GameObject inGamePanel;
    }
    public Obj obj;

    public int finalScore;
    public Text finalScoreText;
    public Text scriptPanelText;
    string[] scriptPanelString = {"빨리 빨리","느긋하게 해봐야 답은 없다고","가즈아~!", "나는 아니겠지?" };
    public Animator animScript;
    public Text debugText;

    public enum GameState    {        title, play, gameover    }
    public GameState gameState;

    public PlayerNum playerNum;

    [System.Serializable]
    public class Player
    {
        public string name;
        public GameObject playerObj;
    }
    public List<Player> player;


    private void Awake()
    {
        if (ins == null)
            ins = this;
        else if (ins != null)
            Destroy(this);
    }

    public void Btn_Scoreing(int score)
    {
        finalScore += score;

        if (finalScore >= 31)
        {
            finalScore -= score;
            gameState = GameState.gameover;
            debugText.text = finalScore + "가 되어 플레이어 " + playerNum.ToString() + "가 걸렸습니다.";
        }

        if (gameState != GameState.gameover)
        {
            RefreshScore_Script();

            switch (playerNum)
            {
                case PlayerNum.one:
                    playerNum = PlayerNum.two;
                    break;
                case PlayerNum.two:
                    playerNum = PlayerNum.three;
                    break;
                case PlayerNum.three:
                    playerNum = PlayerNum.four;
                    break;
                case PlayerNum.four:
                    playerNum = PlayerNum.one;
                    break;
            }

            debugText.text = "플레이어 순서 : " + playerNum.ToString();
        }
    }

    IEnumerator ScriptAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animScript.SetTrigger("isEnd");
    }

    void RefreshScore_Script()
    {
        finalScoreText.text = "현재 숫자 : " + finalScore.ToString();
        int temp = Random.Range(0, scriptPanelString.Length);
        scriptPanelText.text = scriptPanelString[temp];
        animScript.SetTrigger("isOpen");
    }

    public void Btn_GameStart()
    {
        obj.charaSelectPanel.SetActive(false);
        obj.inGamePanel.SetActive(true);
        RefreshScore_Script();
    }
}
