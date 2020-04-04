using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonController : MonoBehaviour
{
    private Button back;
    // Start is called before the first frame update
    void Start()
    {
        back = GetComponent<Button>();
        back.onClick.AddListener(GoToPreviousScene);
    }

    private void GoToPreviousScene()
    {
        if (PlayerPrefs.HasKey("previousScene"))
        {
            string previousScene = PlayerPrefs.GetString("previousScene");
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            string errorMsg = "Eroare! Scena precedenta nu a fost setata!";
            throw new System.ApplicationException(errorMsg);
        }
        
        throw new System.NotImplementedException();
    }
}
