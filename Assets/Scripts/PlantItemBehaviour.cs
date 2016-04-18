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

    private int pressCount = 0;

    // Use this for initialization
    void Start()
    {
        description = description.Replace("\\n", "\n");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (pressCount == 0 && plantHighlightDelegate != null)
        {
            plantHighlightDelegate(description, position);
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
