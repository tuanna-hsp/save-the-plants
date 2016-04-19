using UnityEngine;
using System.Collections;

public class PlantItemBehaviour : MonoBehaviour
{
    public string description;
    public int position; 

    public delegate void PlantHighlightDelegate(string description, int position);
    public PlantHighlightDelegate plantHighlightDelegate;

    public delegate void PlantSelectedDelegate(int position);
    public PlantSelectedDelegate plantSelectedDelegate;

    public string Description
    {
        get {
            return description.Replace("\\n", "\n");
        }
    }

    private int pressCount = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (pressCount == 0 && plantHighlightDelegate != null)
        {
            plantHighlightDelegate(Description, position);
        }
        else if (pressCount == 1 && plantSelectedDelegate != null)
        {
            plantSelectedDelegate(position);
        }
        pressCount++;
    }

    public void clearHighlight()
    {
        pressCount = 0;
    }
}
