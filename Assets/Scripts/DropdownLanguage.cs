using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Dropdown))]
public class DropdownLanguage : MonoBehaviour
{   public Translator script;
    public Dropdown dropdown;
    public Button contact, info;
    void Start()
    {   
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();
        List<string> items = new List<string>();
        List<string> language = new List<string>{"English","Spanish","French","Italian","German","Portuguese","Russian","Japanese","Romanian"};
        if(PlayerPrefs.HasKey("language"))
            items.Add(PlayerPrefs.GetString("language"));
        int i=0;
        for(i=0;i<language.Count;i++){
            if(language[i]!=PlayerPrefs.GetString("language")){
                items.Add(language[i]);
            }
        }

        dropdown.AddOptions(items);
        dropdown.onValueChanged.AddListener(delegate
        {
            selectvalue(dropdown);
        });
        
    }

    void selectvalue(Dropdown dropdown){
        int index = dropdown.value;
        PlayerPrefs.SetString("language", dropdown.options[index].text);
        SceneManager.LoadScene("SettingsScene");
    }

    public void Contact() {
        SceneManager.LoadScene("FormScene"); 
        }
    public void AboutUs(){
        SceneManager.LoadScene("DemoScene");
    }
}
