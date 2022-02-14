using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    // either public, or if you want to keep private fields: [SerializeField]
    public Canvas CanvasLevelOver;
    public Text TextEndMessage;
    public InputField InputFieldPoints;
    public InputField InputFieldTimeLeft;
    public static int StartTime = 120;
    public static int TimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        //Assets.Scripts.Scores.points = 0;
        CanvasLevelOver.enabled = false;    // level over menu hidden
        TimeLeft = StartTime;   // reset the level time left
        InputFieldTimeLeft.text = TimeLeft.ToString(); // show initial time left
        InputFieldPoints.text = Scores.points.ToString();    // update screen points field
        Time.timeScale = 1; // unfreeze time
        StartCoroutine("CheckLevel");   // start the periodic checks
    }

    IEnumerator CheckLevel()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(1.0f);  // first we wait one second
            if(TimeLeft>0)
                TimeLeft--;
            InputFieldTimeLeft.text = TimeLeft.ToString();
            InputFieldPoints.text = Scores.points.ToString();    // update screen points field

            if (Scores.timeBonus != 0)
            {
                TimeLeft += Scores.timeBonus;
                Scores.timeBonus = 0;
            }
            
            // if (Scores.points == 100) // no pickups left? --> win level!
            // {
            //     TextEndMessage.text = "Level completed!";
            //     int ElapsedTime = StartTime - TimeLeft;
            //     if (PlayerPrefs.HasKey(Scores.HighScoreTimeKey)) // score time already exists?
            //     {
            //         int OldScoreTime = PlayerPrefs.GetInt(Scores.HighScoreTimeKey);
            //         if (ElapsedTime < OldScoreTime)  // got a new score time?
            //         {
            //             TextEndMessage.text += " New high score: "+ ElapsedTime.ToString() + " !";
            //             PlayerPrefs.SetInt(Scores.HighScoreTimeKey, ElapsedTime);
            //         }
            //     }
            //     else
            //     {
            //         TextEndMessage.text += " New high score: " + ElapsedTime.ToString() + " !";
            //         PlayerPrefs.SetInt(Scores.HighScoreTimeKey, ElapsedTime);
            //     }
            //     CanvasLevelOver.enabled = true;  // level over menu visible
            //     Time.timeScale = 0; // freeze time
            //     break;
            
            
            if (TimeLeft <= 0)   // lost level (and game)!
            {
                CanvasLevelOver.enabled = true;  // level over menu visible
                TextEndMessage.text = "Game over!";
                //Time.timeScale = 0; // freeze time
                Cursor.visible = true;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // button event handler should be: public void and should have 0..1 input params (just built in data types accepted: string and num types mainly)
    public void RestartLevel()
    {
        // reload the scene we currently are 
        Scores.points = 0;
        StartTime = 120;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMain()
    {
        //SceneManager.LoadScene(0);   // usually main scene's build index is 0
        SceneManager.LoadScene("Main"); // but we can use the scene name
        //BackendHandler.ScoreInput.text = GameEnder.hcscore.ToString();
    }
    public void DeleteScores()
    {
//        PlayerPrefs.DeleteKey(Assets.Scripts.Scores.HighScoreTimeKey);  // if we want to delete separate keys
        PlayerPrefs.DeleteAll();
    }
}
