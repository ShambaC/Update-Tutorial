using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Update_Check : MonoBehaviour
{
    public string URL;
    public Text update_text;
    public int Set_Version;
    public Text VersionTXT;
    public GameObject Download_Button;

    private int Current_Ver;
    private string PatchURL;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Version", Set_Version);
        Current_Ver = PlayerPrefs.GetInt("Version", 10);
        StartCoroutine(GetRequest(URL));
        VersionTXT.text = "version: " + Current_Ver;
        Download_Button.SetActive(false);
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
                string File_Data = webRequest.downloadHandler.text;
                string[] TextArr = File_Data.Split('@');
                string Update_ver = TextArr[0];              
                if(int.Parse(Update_ver) > Current_Ver)
                {                   
                    update_text.text = "Update Required!";
                    Download_Button.SetActive(true);
                    PatchURL = TextArr[1];
                }
                else
                {
                    update_text.text = "You are up to date!";
                }
            }
        }
    }

    public void Download_Button_Method()
    {
        Application.OpenURL(PatchURL);
    }
}
