using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectorBehaviour : MonoBehaviour
{
    public Text infoText;

    public delegate void PlantCreateDelegate(GameObject selector, PlantType type);
    public PlantCreateDelegate plantCreateDelegate;

    private List<PlantItemBehaviour> plantBehaviourList = new List<PlantItemBehaviour>();

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "PlantItem")
            {
                PlantItemBehaviour behaviour = child.GetComponent<PlantItemBehaviour>();
                behaviour.plantHighlightDelegate = onPlantHighlight;
                behaviour.plantSelectedDelegate = onPlantSelected;
                if (behaviour.position == 0)
                {
                    setInfoText(behaviour.description);
                }

                plantBehaviourList.Add(behaviour);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setInfoText(string text)
    {
        infoText.GetComponent<Text>().text = text;
    }

    void onPlantHighlight(string description, int position)
    {
        setInfoText(description);

        foreach (PlantItemBehaviour behaviour in plantBehaviourList)
        {
            if (behaviour.position != position)
            {
                behaviour.clearHighlight();
            }
        }
    }

    void onPlantSelected(int position)
    {
        if (plantCreateDelegate != null)
        {
            // TODO: More type
            plantCreateDelegate(gameObject, PlantType.PLANT1);
        }
    }
}
