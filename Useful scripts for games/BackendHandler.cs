using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BackendHandler : MonoBehaviour
{
    /*const string jsonTestStr = "{ " +
        "\"scores\":[ " +
        "{\"id\":1, \"playername\":\"Matti\", \"score\":20, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":2, \"playername\":\"Hankka\", \"score\":30, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":3, \"playername\":\"Ismo\", \"score\":40, \"playtime\": \"2020-21-11 08:20:00\"} " +
        "] }";
    */
    //const string urlBackendHighScoresFile = "http://localhost/gamewithphpbackend/api/v1/highscores.json";
    const string urlBackendHighScores = "http://localhost/gamewithphpbackend/api/v1/highscores.php";
    
    const string urlBackendHighScoresFile = "http://172.30.139.32/php-game/api/v1/highscores.json"; //Address for virtual machine that is running the highscore database
    //const string urlBackendHighScores = "http://172.30.139.32/php-game/api/v1/HighScores.php";


    // HighScore table
    HighScores.HighScores hs = null;

    // Logging info
    string log = "";
    int fetchCounter = 0;

    bool updateHighScoresText = false;

    //UI elements
    public UnityEngine.UI.Text Loggingtext;
    public UnityEngine.UI.Text HighScoresText;
    public UnityEngine.UI.InputField PlayerNameInput;
    public static UnityEngine.UI.Text ScoreInput;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BackendHandler starter");

        // testing json conversion
        /*hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
        Debug.Log("HighScore winner name:" + hs.scores[0].playername);
        Debug.Log("HighScores as Json:" + JsonUtility.ToJson(hs));
        */
        InsertToLog("Game started.");
        
    }

    // Update is called once per frame
    void Update()
    {
        Loggingtext.text = log;
        
        if(updateHighScoresText)
        {
            HighScoresText.text = CreateHighScoresList();
            updateHighScoresText = false;
        }
    }
    string CreateHighScoresList()
    {
        string hsList = "";

        if(hs != null)
        {
            int len = (hs.scores.Length < 3) ? hs.scores.Length : 3;
            for( int i = 0; i < len; i++)
            {

                hsList += string.Format("[ {0} ] {1,15} {2,5} {3,15}\n",
                    (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
            }
        }

        return hsList;
    }
    public void FetchHighScoresJSONFILE()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSONFILE clicked");
        StartCoroutine(GetRequestForHighScores(urlBackendHighScoresFile));
    }
    public void FetchHighScoresJSON()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSON clicked");
        StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }
    public void PostGamesResult()
    {
        HighScores.HighScore hsItem = new HighScores.HighScore();

        if (PlayerNameInput.text.Length > 0 && GameEnder.hcscore > 0)
        {
            hsItem.playername = PlayerNameInput.text;
            hsItem.score = float.Parse(GameEnder.hcscore.ToString());

            

            Debug.Log("PostgamesResult sent: " + PlayerNameInput.text + " with scores " + GameEnder.hcscore);
            StartCoroutine(PostReguestForHighScores(urlBackendHighScores, hsItem));
            PlayerNameInput.text = "";
            //ScoreInput.text = "";
        }
        
    }
    string InsertToLog(string s)
    {
        return log = "[" +fetchCounter + "]" + s + "\n" + log;
    }
    IEnumerator GetRequestForHighScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            InsertToLog("Request sent to " + uri);
            // set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            // Request and wait for reply
            yield return webRequest.SendWebRequest();
            // get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
            if (webRequest.isNetworkError)
            {
                InsertToLog("Error encountered: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // create HighScore item from json string
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);

                updateHighScoresText = true;
                InsertToLog("Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr);
                Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
//                Debug.Log("Received(HS) name: " + hs.scores[0].playername);

            }
        }
    }
    IEnumerator PostReguestForHighScores(string uri, HighScores.HighScore hsItem)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, JsonUtility.ToJson(hsItem)))
        {
            InsertToLog("Post request sent to " + uri);
            // set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            // Request and wait for reply
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                InsertToLog("Error encountered in Post: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                InsertToLog("Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr);
            }
        }
    }
}
