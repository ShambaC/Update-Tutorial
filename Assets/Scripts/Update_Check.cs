using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Update_Check : MonoBehaviour
{
    public string URL;
    public Text update_text;

    private int Current_Ver;

    // Start is called before the first frame update
    void Start()
    {
        Current_Ver = PlayerPrefs.GetInt("Version", 10);
        StartCoroutine(GetRequest(URL));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.isNetworkError)
            {
                update_text.text = "Error :" + webRequest.error;
            }
            else
            {
                string Update_ver = webRequest.downloadHandler.text;                
                if(int.Parse(Update_ver) > Current_Ver)
                {                   
                    update_text.text = "Update Required!";
                }
                else
                {
                    update_text.text = "You are up to date!";
                }
            }
        }
    }
}
