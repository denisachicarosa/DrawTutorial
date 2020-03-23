using System;
using System.Linq;
using UnityEngine;
using Vuforia;

public class StepController : MonoBehaviour
{
    private Material[] Materials;
    
    private Renderer renderer;
    private GameObject book;

    private bool mSwapModel = false;
    
    public int step = 0;
    private int number_of_steps;

    // Start is called before the first frame update
    void Start()
    {
        Materials = Resources.LoadAll("Materials/Pikachu", typeof(Material)).Cast<Material>().ToArray();
        number_of_steps = Materials.Length - 1;
        book = GameObject.Find("Quad");
        renderer = book.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        if (mSwapModel)
        {
            if (step >= number_of_steps)
            {
                step = 0;
            }
            else
            {
                step = step + 1;
            }
            
            SwapModel(step);
            mSwapModel = false;
        }
    }
    
    void OnGUI() {
        if (GUI.Button (new Rect(50,50,120,40), "Swap Model")) {
            mSwapModel = true;
            Debug.Log("number of steps = " + number_of_steps + " --- current step = " + step);
        }
    }
    
    private void SwapModel(int step)
    {
        renderer.material = Materials[step];
    }
}
