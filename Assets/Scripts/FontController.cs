using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class FontController : MonoBehaviour
{
    private string currentScene;
    private string previousScene = "MenuScene";
    
    public List<string> _fonts;
    public List<string> _extensions;
    public TMP_Dropdown _dropdown;
    public TMP_InputField _textArea;
    public Button _go;
    public Camera myCamera;
    
    public GameObject objText;
    public Font fontResource;
    
    private bool submitted = false; 
    private Rect windowRect = new Rect (0, (Screen.height)/2, Screen.width, 50);

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("previousScene", previousScene);
        
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
        DirectoryInfo dir = new DirectoryInfo("./Assets/Resources/Fonts");
        FileInfo[] info = dir.GetFiles("*.*");
        _extensions = new List<string>();
        string filename;
        
        foreach (FileInfo file in info)
        {
            filename = Path.GetFileNameWithoutExtension(file.ToString());
            var ext = Path.GetExtension(file.ToString());
            if (ext != ".meta")
            {
                _fonts.Add(filename);
                _extensions.Add(ext);    
            }
        }
        
        _dropdown.AddOptions(_fonts);
    }

    void CreateImageFromText()
    {
        submitted = true;
        string font = _fonts[_dropdown.value] + _extensions[_dropdown.value];
        string text = _textArea.text;
        
        if (!string.IsNullOrEmpty(text))
        {
            // TextToImg._CreateImageFromText(font, text);   
            StartDrawing(font, text);
        }
    }

    private void OnGUI()
    {
        if (submitted && string.IsNullOrEmpty(_textArea.text))
        {
            GUI.backgroundColor = Color.red;
            GUI.color = Color.white;
            windowRect = GUI.Window(0, windowRect, DialogWindow, "Text cannot be empty!");
        }
    }
    
    // This is the actual window.
    void DialogWindow (int windowID)
    {
        float height = 20;
        float y = windowRect.height - height;

        if(GUI.Button(new Rect(5, y, windowRect.width - 10, height), "Ok"))
        {
            submitted = false;
        }
    }

    void _CreateImageFromText(string font, string text)
    {
        // WWWForm data = new WWWForm();
        // data.AddField("font", font);
        // data.AddField("text", text);
        // string Uri = "https://uri-to-api.com";
        // UnityWebRequest.Post(Uri, data);

        StartDrawing(font, text);
        
        // int imageWidth = 1240;
        // int imageHeight = 1754;
        // int fontSize = 100;
        //
        // Texture2D output = new Texture2D(imageWidth, imageHeight);
        // RenderTexture renderTexture = new RenderTexture(imageWidth, imageHeight,24);
        // RenderTexture.active = renderTexture;
        //
        // myCamera.orthographic = true;
        // myCamera.orthographicSize = 100;
        // myCamera.targetTexture = renderTexture;
        //
        // TextMeshPro tmp = new TextMeshPro();
        //
        // tmp.text = text;
        // tmp.material = Resources.Load<Material>("Materials/transparent");
        // tmp.alignment = TextAlignmentOptions.Justified;
        // tmp.lineSpacing = 1;
        // tmp.fontSize = 56;
        // tmp.richText = true;
        // tmp.font = TMP_FontAsset.CreateFontAsset(fontResource);
        //
        // myCamera.Render();
        //
        // output.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        // output.Apply();
        //
        // RenderTexture.active = null;
        //
        // // Encode texture into PNG
        // byte[] bytes = output.EncodeToPNG();
        // Object.Destroy(output);
        // Debug.Log("bytes length = " + bytes.Length);
        //
        // // For testing purposes, also write to a file in the project folder
        // File.WriteAllBytes(Application.dataPath + "/Assets/Resources/Textures/font.png", bytes);
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

    void StartDrawing(string font, string text)
    {
        PlayerPrefs.SetString("font", font);
        PlayerPrefs.SetString("text", text);
        PlayerPrefs.SetInt("isFont", 1);

        SceneManager.LoadScene("ARScene");
    }
    
    void Destroy()
    {
        _go.onClick.RemoveListener(() => CreateImageFromText());
        Object.Destroy(_dropdown);
        Object.Destroy(_go);
    }
}
