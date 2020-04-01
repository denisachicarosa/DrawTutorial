using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TextToImg : MonoBehaviour
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
        text.color = Color.white;
        text.fontSize = 100;

        text.text = _text;

        textMaterial.mainTexture = texture;

        AssetDatabase.SaveAssets();
        return textMaterial;
    }
}