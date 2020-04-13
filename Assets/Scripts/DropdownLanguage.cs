using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Dropdown))]
public class DropdownLanguage : MonoBehaviour
{   public Translator script;
    public Dropdown dropdown;
    public Button contact, info;
    void Start()
    {   TranslateText trans = new TranslateText();
        string raspuns = trans.translate("M-am plictisit deja");
        //Debug.Log("MEsaj: "+raspuns);
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();
        List<string> items = new List<string>();
        items.Add("English");
        items.Add("Spanish");
        items.Add("French");
        items.Add("Italian");
        items.Add("German");
        items.Add("Portuguese");
        items.Add("Turkish");
        items.Add("Russian");
        items.Add("Japanese");
        items.Add("Romanian");
        items.Sort();

        dropdown.AddOptions(items);

        int index = dropdown.value;
        PlayerPrefs.SetString("language", dropdown.options[index].text);

        dropdown.onValueChanged.AddListener(delegate
        {
            selectvalue(dropdown);
        });
        
    }

    void selectvalue(Dropdown dropdown){
        int index = dropdown.value;
        PlayerPrefs.SetString("language", dropdown.options[index].text);
    }

    public void Contact() {
        SceneManager.LoadScene("FormScene"); 
        }
    public void AboutUs(){
        SceneManager.LoadScene("DemoScene");
    }
}
