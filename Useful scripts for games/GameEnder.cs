using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnder : MonoBehaviour
{
    public Canvas CanvasLevelOver;
    public Text TextEndMessage;

    public static int hcscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            CanvasLevelOver.enabled = true;
            //Time.timeScale = 0;
            
            hcscore = Scores.points * LevelController.TimeLeft;
            TextEndMessage.text = "Level completed! with points: " + hcscore;
            Debug.Log("Game ended with points: "+ hcscore);
            
            
        }
    }
 
}
