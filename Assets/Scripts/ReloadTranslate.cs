using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTranslate : MonoBehaviour
{
    // Start is called before the first frame update
    // public Text text;
    private GameObject[] texts;
    string response;
    public int nrTexts;
    void Start()
    {
        string toTranslate = "";
        var renderer = GetComponentsInChildren<Text>().ToArray();
        foreach (var t  in renderer)
        {   if(t.text == ""){
                toTranslate += "*. ";
            }else{
             toTranslate += t.text + ". ";
            }
            
         
        }
        Debug.Log("To translate: " + toTranslate);
        translate(toTranslate,renderer);

       
        // tr.setText(text);
        // tr.translate();
    }
        public void translate(string myText, Text[] texts){
        Language lang = new Language();
        // string myText = text.text;
        // Debug.Log(myText);
        string first = lang.prefLang("Auto");
        // if (!PlayerPrefs.HasKey("language"))
        //     PlayerPrefs.SetString("language", "English");
        string lang_name = PlayerPrefs.GetString("language");
        string second = lang.prefLang(lang_name);
        Translator translator = Translator.Create(first,second);
        Debug.Log("Am initializat translatorul");
        //
        // translator.Run(myText, results =>
        //       {
        //           // Debug.Log("TR: " + results[0].translated);
        //
        //           // text.text = results[0].translated;
        //       });
        int i = 0;
        translator.Run(myText, results => {
                foreach (var result in results){
                    response = response + result.translated;
                    Debug.Log("Mesele"+response);
                   
                }
                Debug.Log("Mergeeee"+response);
                string[] textTranslate = response.Split('.');
                for(i=0;i<texts.Length;i++){
                    Debug.Log("Split: "+i+"  "+textTranslate[i]);
                    if(textTranslate[i]!="*")
                        texts[i].text = textTranslate[i]; 
                }   
            }); 

        }
    // Update is called once per frame
    void Update()
    {
       
    }
}