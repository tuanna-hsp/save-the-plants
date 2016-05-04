using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeBehaviour : MonoBehaviour
{
    public Text upgradeText;
    public GameObject upgradeButton;
    public Text upgradeCostText;
    public Text sellValueText;
    
    public GameObject CurrentPlant
    {
        set {
            PlantData monsterData = value.GetComponent<PlantData>();
            nextLevel = monsterData.getNextLevel();

            upgradeText.GetComponent<Text>().text = getNextLevelInfo();
            if (nextLevel == null)
            {
                // Hide upgrade button when level already max
                upgradeButton.SetActive(false);
            }
            else
            {
                upgradeButton.SetActive(true);
                upgradeCostText.GetComponent<Text>().text = nextLevel.cost + "";
            }
            sellValueText.GetComponent<Text>().text = monsterData.CurrentLevel.cost / 2 + "";
        }
    }

    public delegate void PlantUpgraded();
    public PlantUpgraded OnPlantUpgraded;

    public delegate void PlantSold();
    public PlantSold OnPlantSold;

    private PlantLevel nextLevel;    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if mouse clicked outside the selector, if it is then destroy this game object
            RectTransform rectTransform = GetComponent<RectTransform>();
            // Convert mouse pos to this object local space
            Vector3 mousePos = rectTransform.InverseTransformPoint(Input.mousePosition);
            // rectTransform.rect is in local space
            if (!rectTransform.rect.Contains(mousePos))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private string getNextLevelInfo()
    {
        if (nextLevel != null)
        {
            return "Next level\nDamage: 20\nFire rate: " + (nextLevel.fireRate * 10)
            + "\nCost: " + nextLevel.cost;
        }
        return "Max level.";
    }

    public void UpgradePlant()
    {
        if (OnPlantUpgraded != null)
        {
            OnPlantUpgraded();
        }
        // Close upgrade menu
        gameObject.SetActive(false);
    }

    public void SellPlant()
    {
        if (OnPlantSold != null)
        {
            OnPlantSold();
        }
        gameObject.SetActive(false);
    }
}
