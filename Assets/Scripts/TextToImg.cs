using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TextToImg
{
    int imageWidth = 1240;
    int imageHeight = 1754;
    int fontSize = 100;

    public Material createMaterial(Font _font, string _text)
    {
        var textMaterial = Resources.Load<Material>("Textures/TextMaterial");
        var texture = Resources.Load<Texture>("Textures/TextTexture");

        // Canvas canvas = GameObject.FindGameObjectWithTag("RenderCanvas").GetComponentInChildren<Canvas>();
        Text text = GameObject.Find("TextForTexture").GetComponent<Text>();

        text.font = _font;
        text.color = Color.black;
        text.fontSize = 250;

        text.text = _text;

        Debug.Log("Created material: " + text.font + "\n" + text.text);
        Debug.Log(textMaterial == null);
        Debug.Log(texture == null);
        textMaterial.mainTexture = texture;
        
        return textMaterial;
    }
}