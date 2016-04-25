using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectorBehaviour : MonoBehaviour
{
    public Text infoText;

    public delegate void PlantCreateDelegate(GameObject selector, PlantType type);
    public PlantCreateDelegate plantCreateDelegate;

    public delegate void PlantPreviewDelegate(PlantType type);
    public PlantPreviewDelegate plantPreviewDelegate;

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
                    setInfoText(behaviour.Description);
                }

                plantBehaviourList.Add(behaviour);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if mouse clicked outside the selector, if it is then destroy this game object
            RectTransform rectTransform = GetComponent<RectTransform>();
            // Convert mouse pos to this object local space
            Vector3 mousePos = rectTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // rectTransform.rect is in local space
            if (!rectTransform.rect.Contains(mousePos))
            {
                Destroy(gameObject);
            }
        }
    }

    void setInfoText(string text)
    {
        infoText.GetComponent<Text>().text = text;
    }

    void onPlantHighlight(string description, int position)
    {
        setInfoText(description);
        if (plantPreviewDelegate != null)
        {
            plantPreviewDelegate(PlantType.PLANT1);
        }

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
            switch (position)
            {
                case 0:
                    plantCreateDelegate(gameObject, PlantType.PLANT1);
                    break;
                case 1:
                    plantCreateDelegate(gameObject, PlantType.PLANT2);
                    break;
            }
        }
    }
}
