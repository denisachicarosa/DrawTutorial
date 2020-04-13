using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTranslate : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    private TranslateText tr =  new TranslateText();
    void Start()
    {
        tr.setText(text);
        tr.translate();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
