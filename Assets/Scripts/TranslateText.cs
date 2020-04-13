using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateText : MonoBehaviour
{  
    public string response;
    public Text text;
    
    public void Start()
    {
        translate();
    }

    public void Update()
    {
        
    }

    public void translate(){
        Language lang = new Language();
        string myText = text.text;
        // Debug.Log(myText);
        string first = lang.prefLang("Auto");
        // if (!PlayerPrefs.HasKey("language"))
        //     PlayerPrefs.SetString("language", "English");
        string lang_name = PlayerPrefs.GetString("language");
        string second = lang.prefLang(lang_name);
        Translator translator = Translator.Create(first,second);
        Debug.Log("Am initializat translatorul");
        
        translator.Run(myText, results =>
              {
                  // Debug.Log("TR: " + results[0].translated);

                  text.text = results[0].translated;
              });

        // translator.Run(myText, results => {
        //         foreach (var result in results){
        //             response = response + result.translated;
        //             
        //         }
        //         
        //     }); 
        
        // Debug.Log(text.text);
   }

    public void setText(Text t)
    {
        text = t;
    }
}
