using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;

public class HttpManager : MonoBehaviour
{
    [SerializeField]
    private string URL;

    [SerializeField]
    TextMeshProUGUI [] scores;
    [SerializeField]
    TextMeshProUGUI[] ids;
    List<int> scoresList = new List<int>(); 
    List<string> idsList= new List<string>();
    
    void Start()
    {
       
    }

    // Update is called once per frame
   

    public void ClickGetScore()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = URL + "/scores";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("Network Error"+ www.error);
        }
        else if (www.responseCode == 200)
        {
            Debug.Log(www.downloadHandler.text);
            ScoresData scored = JsonUtility.FromJson<ScoresData>(www.downloadHandler.text);

            foreach(Puntaje s in scored.scores)
            {
                Debug.Log(s.user_id + " | " + s.score);
               
                scoresList.Add(s.score);
                idsList.Add(s.user_id);
              
                
            }

        }
        else
        {
            Debug.Log(www.error);
        }


    }

    void Update()
    {
        //puntajes
        scores[0].text = scoresList.ElementAt(0).ToString();
        scores[1].text = scoresList.ElementAt(1).ToString();
        scores[2].text = scoresList.ElementAt(2).ToString();
        scores[3].text = scoresList.ElementAt(3).ToString();
        scores[4].text = scoresList.ElementAt(4).ToString();

        //nombbres
        ids[0].text = idsList.ElementAt(0).ToString();
        ids[1].text = idsList.ElementAt(1).ToString();
        ids[2].text = idsList.ElementAt(2).ToString();
        ids[3].text = idsList.ElementAt(3).ToString();
        ids[4].text = idsList.ElementAt(4).ToString();


        
    }

}

[System.Serializable]
public class Puntaje
{
    public string user_id;
    public int score;
}

[System.Serializable]
public class ScoresData
{
    public Puntaje[] scores;
}
