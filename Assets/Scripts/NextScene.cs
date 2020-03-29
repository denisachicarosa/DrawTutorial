using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    private Button go;
    // Start is called before the first frame update
    void Start()
    {
        go = GetComponent<Button>();
        go.onClick.AddListener(GoToNextScene);
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene ("ARScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
