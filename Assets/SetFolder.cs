using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFolder : MonoBehaviour
{
    public Text text;

    public String folderName;
    // Start is called before the first frame update
    void Start()
    {
        folderName = PlayerPrefs.GetString("drawing");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = folderName;
    }
}
