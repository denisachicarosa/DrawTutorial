﻿using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class StepController : MonoBehaviour
{
    public Material[] Materials = null;
    
    private Renderer renderer;
    private GameObject book;

    private bool mSwapModel = false;
    
    public int step = 0;
    public int number_of_steps;
    
    public String drawingName;

    // Start is called before the first frame update
    void Start()
    {
        drawingName = PlayerPrefs.GetString("drawing");
        String path = "Materials/" + drawingName;
        
        Materials = Resources.LoadAll(path, typeof(Material)).Cast<Material>().ToArray();

        number_of_steps = Materials.Length - 1;
        book = GameObject.Find("Quad");
        renderer = book.GetComponent<Renderer>();

        // set first material
        renderer.material = Materials[0];
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
    
    private void SwapModel(int step)
    {
        renderer.material = Materials[step];
    }
}