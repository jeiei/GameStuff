using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("JuhoScene");   //Remember that all the scenes that are to be loaded by script, have to be in build settings!
        Scores.points = 0;
        LevelController.StartTime = 120;
        Cursor.visible = false;
    }
    public void ExitGame()
    {
        Application.Quit(); // works only when game exported, not in unity editor
    }
}
