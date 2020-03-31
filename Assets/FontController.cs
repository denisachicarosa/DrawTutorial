using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class FontController : MonoBehaviour
{
    private List<string> _fonts;
    private List<string> _extensions;
    public TMP_Dropdown _dropdown;
    public TMP_InputField _textArea;
    public Button _go;
    public Camera myCamera;
    
    public GameObject objText;
    public TextMesh textMesh;
    public Font fontResource;

    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        _dropdown.ClearOptions();

        _textArea = GetComponentInChildren<TMP_InputField>();
        
        _go = GetComponentInChildren<Button>();
        
        _fonts = new List<string>();
        
        PopulateList();
        _go.onClick.AddListener(() => CreateImageFromText());
    }

    void PopulateList()
    {
        DirectoryInfo dir = new DirectoryInfo("./Assets/Fonts");
        FileInfo[] info = dir.GetFiles("*.*");
        _extensions = new List<string>();
        string filename;
        
        foreach (FileInfo file in info)
        {
            filename = Path.GetFileNameWithoutExtension(file.ToString());
            _fonts.Add(filename);
            _extensions.Add(Path.GetExtension(file.ToString()));
        }
        
        _dropdown.AddOptions(_fonts);
    }

    void CreateImageFromText()
    {
        string font = _fonts[_dropdown.value] + _extensions[_dropdown.value];
        string text = _textArea.text;
        
        if (string.IsNullOrEmpty(text))
        {
            // EditorGUILayout.HelpBox("Text cannot be empty!", MessageType.Error);
            EditorUtility.DisplayDialog("You're dirty...", "Text cannot be empty!", "Ok");
        }
        else
        {
            _CreateImageFromText(font, text);    
        }
    }

    void _CreateImageFromText(string font, string text)
    {
        int imageWidth = 1240;
        int imageHeight = 1754;
        int fontSize = 100;

        fontResource  = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Fonts/" + font, typeof(Font)) as Font;
        
        Texture2D output = new Texture2D(imageWidth, imageHeight);
        RenderTexture renderTexture = new RenderTexture(imageWidth, imageHeight,24);
        RenderTexture.active = renderTexture;
        
        myCamera.orthographic = true;
        myCamera.orthographicSize = 100;
        myCamera.targetTexture = renderTexture;
        
        GUIText guiText = new GUIText();
        guiText.text = text;
        guiText.anchor = TextAnchor.LowerLeft;
        guiText.alignment = TextAlignment.Left;
        guiText.lineSpacing = 1;
        guiText.pixelOffset = new Vector2(50,50);
        guiText.font = fontResource;
        
        myCamera.Render();
        
        output.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        output.Apply();
        
        RenderTexture.active = null;

        // Encode texture into PNG
        byte[] bytes = output.EncodeToPNG();
        Object.Destroy(output);

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/Assets/Resources/Textures/font.png", bytes);
    }
    
    private Texture2D MakeTex( int width, int height, Color col ){
        Color[] pix = new Color[width * height];
        for( int i = 0; i < pix.Length; ++i ){
            pix[ i ] = col;
        }
        Texture2D result = new Texture2D( width, height );
        result.SetPixels( pix );
        result.Apply();
        return result;
    }

    void StartDrawing()
    {
        // PlayerPrefs.SetString("font", font);
        // PlayerPrefs.SetString("text", text);
        PlayerPrefs.SetInt("isFont", 1);

        SceneManager.LoadScene("ARScene");
    }
    
    void Destroy()
    {
        _go.onClick.RemoveListener(() => CreateImageFromText());
    }
}
