using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateText : MonoBehaviour
{  public string response;
   public string translate(string myText){
        Language lang = new Language();
        string first = lang.prefLang("Auto");
        string second = lang.prefLang("French");
        Translator translator = Translator.Create(first,second);
        
      //   translator.Run(myText, results => { results[0].translated;});

        translator.Run(myText, results => {
                foreach (var result in results){
                   
                    response = response + result.translated;}
            });
                    return myText;
   }
}
