using UnityEngine;
using System.Collections;

public class PlacePlant : MonoBehaviour
{
    public GameObject plant1Prefab;
    public GameObject plant2Prefab;
    public GameObject plant3Prefab;
    public GameObject topSelectorPrefab;
    public GameObject bottomSelectorPrefab;
    public GameObject rangePrefab;

    private GameObject topUpgradeMenu;
    private GameObject bottomUpgradeMenu;
    private GameObject plant;
    private GameObject rangePreview;
    private GameManagerBehavior gameManager;
    private Canvas hudCanvas;
    private GameObject plantSelector;
    private bool isAmbientEnabled;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        topUpgradeMenu = GameObject.Find("UpgradePanelTop");
        bottomUpgradeMenu = GameObject.Find("UpgradePanelBottom");
        hudCanvas = GameObject.Find("HUD Canvas").GetComponent<Canvas>();

        // Add offset to make range preview always behind selector
        rangePreview = (GameObject) Instantiate(
            rangePrefab, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        rangePreview.SetActive(false);

        isAmbientEnabled = PersistantManager.IsAmbientEnabled();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseUp()
    {
        Debug.Log("Open spot clicked");
        if (plant == null)
        {
            if (plantSelector == null)
            {
                Debug.Log("Show plant selector");
                showPlantSelector();
            }
        }
        else
        {
            showUpgradeMenu();
        }
    }

    private void showPlantSelector()
    {
        // By default the selector show above current object, 
        // check and adjust selector position y to display correctly
        if (shouldShowPopUpBelow())
        {
            plantSelector = (GameObject) Instantiate(bottomSelectorPrefab, transform.position, Quaternion.identity);
            RectTransform rectTransform = (RectTransform) plantSelector.transform;
            rectTransform.position -= new Vector3(3, rectTransform.rect.height);
        }
        else
        {
            plantSelector = (GameObject) Instantiate(topSelectorPrefab, transform.position, Quaternion.identity);
            RectTransform rectTransform = (RectTransform) plantSelector.transform;
            rectTransform.position -= new Vector3(4.5f, 0);
        }

        SelectorBehaviour behaviour = plantSelector.GetComponent<SelectorBehaviour>();
        behaviour.plantCreateDelegate = onCreatePlant;
        behaviour.plantPreviewDelegate = onPreviewPlant;
    }

    // Whether current open spot lied in the upper part of the screen
    private bool shouldShowPopUpBelow()
    {
        return transform.position.y > 0;
    }

    private void showUpgradeMenu()
    {
        GameObject upgradeMenu;
        RectTransform rectTransform;
        bool shouldShowBelow = shouldShowPopUpBelow();
        if (shouldShowBelow)
        {
            rectTransform = (RectTransform) bottomUpgradeMenu.transform;
        }
        else 
        {
            rectTransform = (RectTransform) topUpgradeMenu.transform;
        }


        Vector3 targetPosition = transform.position;
        Vector3 viewportPoint = Camera.main.WorldToScreenPoint(targetPosition);
        rectTransform.position += (viewportPoint - rectTransform.position);
        
        float halfHeight = (rectTransform.rect.height / 2) * hudCanvas.scaleFactor * rectTransform.localScale.y;
        if (shouldShowBelow)
        {
            float xOffset = 0 * hudCanvas.scaleFactor * rectTransform.localScale.x;
            Vector3 offset = new Vector3(xOffset, halfHeight, 0);
            upgradeMenu = bottomUpgradeMenu;
            rectTransform.position -= offset;
        }
        else
        {
            float xOffset = -(115 * hudCanvas.scaleFactor * rectTransform.localScale.x);
            Vector3 offset = new Vector3(xOffset, halfHeight, 0);
            upgradeMenu = topUpgradeMenu;
            rectTransform.position += offset;
        }

        UpgradeBehaviour behaviour = upgradeMenu.GetComponent<UpgradeBehaviour>();
        behaviour.OnPlantSold = sellPlant;
        behaviour.OnPlantUpgraded = upgradePlant;
        behaviour.CurrentPlant = plant;

        upgradeMenu.SetActive(true);
    }

    private void onCreatePlant(GameObject selector, PlantType plantType)
    {
        Destroy(plantSelector);
        plantSelector = null;
        switch (plantType)
        {
            case PlantType.PLANT1:
                plant = (GameObject) Instantiate(plant1Prefab, transform.position, Quaternion.identity);
                break;
            case PlantType.PLANT2:
                plant = (GameObject) Instantiate(plant2Prefab, transform.position, Quaternion.identity);
                break;
            case PlantType.PLANT3:
                plant = (GameObject) Instantiate(plant3Prefab, transform.position, Quaternion.identity);
                break;
        }
        if (plant != null)
        {
            if (isAmbientEnabled)
            {
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
            }
            gameManager.Gold -= plant.GetComponent<PlantData>().CurrentLevel.cost;
        }
        rangePreview.SetActive(false);

        // Disable idle animation
        GetComponent<Animator>().SetBool("HasPlant", true);
    }

    private void onPreviewPlant(PlantType type)
    {
        Debug.Log("Previewing plant");
        // TODO: change prefab base on position
        float rangeRadius = 0;
        switch (type)
        {
            case PlantType.PLANT1:
                rangeRadius = plant1Prefab.GetComponent<CircleCollider2D>().radius;
                break;
            case PlantType.PLANT2:
                rangeRadius = plant2Prefab.GetComponent<CircleCollider2D>().radius;
                break;
            case PlantType.PLANT3:
                rangeRadius = plant3Prefab.GetComponent<CircleCollider2D>().radius;
                break;
        }

        float currentRadius = rangePreview.GetComponent<Renderer>().bounds.size.x / 2;
        float scaleRatio = rangeRadius / currentRadius;
        rangePreview.transform.localScale *= scaleRatio;
        rangePreview.SetActive(true);
    }

    private bool canPlaceMonster()
    {
        int cost = plant1Prefab.GetComponent<PlantData>().levels[0].cost;
        return plant == null && gameManager.Gold >= cost;
    }

    private void upgradePlant()
    {
        plant.GetComponent<PlantData>().increaseLevel();
        if (isAmbientEnabled)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }

        gameManager.Gold -= plant.GetComponent<PlantData>().CurrentLevel.cost;
    }

    private void sellPlant()
    {
        // Sell for half-price
        gameManager.Gold += plant.GetComponent<PlantData>().CurrentLevel.cost / 2;
        Destroy(plant);

        GetComponent<Animator>().SetBool("HasPlant", false);
    }
}
