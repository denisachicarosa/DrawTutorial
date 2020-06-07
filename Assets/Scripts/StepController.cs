using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class StepController : MonoBehaviour
{
    private string previousScene;
    private string currentScene;
    
    public List<Material> Materials = null;
    
    private Renderer renderer;
    private GameObject book;

    private bool mSwapModel = false;
    private bool _isFont = false;
    
    public int step = 0;
    public int number_of_steps;
    
    public String drawingName;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        
        Materials = new List<Material>();

        if (PlayerPrefs.HasKey("isFont") && PlayerPrefs.GetInt("isFont") == 1)
        {
            previousScene = "TextScene";
            _isFont = true;
            
            string font = PlayerPrefs.GetString("font");
            string text = PlayerPrefs.GetString("text");
            
            Font fontResource  = Resources.Load("Fonts/" + font, typeof(Font)) as Font;
        
            TextToImg textToImg = new TextToImg();
            Debug.Log(font + "\n" + text);
            Debug.Log(fontResource == null);
            Materials.Add(textToImg.createMaterial(fontResource, text));
            PlayerPrefs.DeleteKey("isFont");
        }
        else
        {
            previousScene = "GalleryScene";
            drawingName = PlayerPrefs.GetString("drawing");
            String path;
            path = "Materials/" + drawingName;
            Materials = Resources.LoadAll(path, typeof(Material)).Cast<Material>().ToList();
        }

        number_of_steps = Materials.Count - 1;
        book = GameObject.Find("Quad");
        renderer = book.GetComponent<Renderer>();

        // set first material
        renderer.material = Materials[0];
        // set previous scene
        PlayerPrefs.SetString("previousScene", previousScene);
    }

    // Update is called once per frame
    void Update () {
        if (mSwapModel)
        {
            if (step >= number_of_steps)
            {
                SceneManager.LoadScene("GalleryScene");
            }
            else
            {
                step = step + 1;
            }
            
            SwapModel(step);
            mSwapModel = false;
        }
    }
    
    void OnGUI()
    {
        // button style
        GUI.backgroundColor = Color.black;
        GUI.color = Color.white;

        if (_isFont == false)
        {
            if (GUI.Button (new Rect(50,50,120,40), "Next Step")) {
                mSwapModel = true;
                GUIStyle style = new GUIStyle(GUI.skin.button);
                foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(style))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(style);
                    Debug.Log(name + " = " + value);
                }
                // Debug.Log("number of steps = " + number_of_steps + " --- current step = " + step);
            }    
        }

        if (GUI.Button(new Rect(50, Screen.height - 90, 50, 50), 
            (Texture) Resources.Load("Images/Logos/back_button_2"), GUIStyle.none))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
    
    private void SwapModel(int step)
    {
        renderer.material = Materials[step];
    }
}
