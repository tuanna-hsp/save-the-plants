using UnityEngine;
using System.Collections;

public class PlacePlant : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject plantSelectorPrefab;
    public GameObject rangePrefab;

    private GameObject upgradeMenu;
    private GameObject plant;
    private GameObject rangePreview;
    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        upgradeMenu = GameObject.Find("UpgradePanel");

        // Add offset to make range preview always behind selector
        rangePreview = (GameObject) Instantiate(
            rangePrefab, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        rangePreview.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseUp()
    {
        if (plant == null)
        {
            showPlantSelector();
        }
        else
        {
            showUpgradeMenu();
        }
        //if (canPlaceMonster ()) {
        //    plant = (GameObject) Instantiate(plantPrefab, transform.position, Quaternion.identity);

        //  		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //	audioSource.PlayOneShot(audioSource.clip);

        //	gameManager.Gold -= plant.GetComponent<MonsterData>().CurrentLevel.cost;
        //} else if (canUpgradeMonster()) {
        //	plant.GetComponent<MonsterData>().increaseLevel();
        //	AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //	audioSource.PlayOneShot(audioSource.clip);

        //	gameManager.Gold -= plant.GetComponent<MonsterData>().CurrentLevel.cost;
        //}
    }

    private void showPlantSelector()
    {
        GameObject plantSelector = (GameObject) Instantiate(plantSelectorPrefab, transform.position, Quaternion.identity);
        SelectorBehaviour behaviour = plantSelector.GetComponent<SelectorBehaviour>();
        behaviour.plantCreateDelegate = onCreatePlant;
        behaviour.plantPreviewDelegate = onPreviewPlant;

        // By default the selector show above current object, 
        // check and adjust selector position y to display correctly
        if (shouldShowPopUpBelow())
        {
            RectTransform rectTransform = (RectTransform) plantSelector.transform;
            rectTransform.position -= new Vector3(0, rectTransform.rect.height);
        }
    }

    // Whether current open spot lied in the upper part of the screen
    private bool shouldShowPopUpBelow()
    {
        return transform.position.y > 0;
    }

    private void showUpgradeMenu()
    {
        RectTransform rectTransform = (RectTransform) upgradeMenu.transform;
        Vector2 position = transform.position;
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(position);
        rectTransform.position = viewportPoint; 
        //if (shouldShowPopUpBelow())
        //{
        //    //newPosition -= new Vector3(0, rectTransform.rect.height);
        //}
        //upgradeMenu.transform.position = newPosition;

        UpgradeBehaviour behaviour = upgradeMenu.GetComponent<UpgradeBehaviour>();
        behaviour.OnPlantSold = sellPlant;
        behaviour.OnPlantUpgraded = upgradePlant;
        behaviour.CurrentPlant = plant;

        upgradeMenu.SetActive(true);
    }

    private void onCreatePlant(GameObject selector, PlantType plantType)
    {
        Destroy(selector);
        switch (plantType)
        {
            case PlantType.PLANT1:
                plant = (GameObject) Instantiate(plantPrefab, transform.position, Quaternion.identity);
                break;
            case PlantType.PLANT2:
                break;
            case PlantType.PLANT3:
                break;
        }
        if (plant != null)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
        rangePreview.SetActive(false);
    }

    private void onPreviewPlant(PlantType type)
    {
        Debug.Log("Previewing plant");
        // TODO: change prefab base on position
        float rangeRadius = 0;
        switch (type)
        {
            case PlantType.PLANT1:
                rangeRadius = plantPrefab.GetComponent<CircleCollider2D>().radius;
                break;
            case PlantType.PLANT2:
                break;
            case PlantType.PLANT3:
                break;
        }

        float currentRadius = rangePreview.GetComponent<Renderer>().bounds.size.x / 2;
        float scaleRatio = rangeRadius / currentRadius;
        rangePreview.transform.localScale *= scaleRatio;
        rangePreview.SetActive(true);
    }

    private bool canPlaceMonster()
    {
        int cost = plantPrefab.GetComponent<PlantData>().levels[0].cost;
        return plant == null && gameManager.Gold >= cost;
    }

    private void upgradePlant()
    {
        plant.GetComponent<PlantData>().increaseLevel();
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

        gameManager.Gold -= plant.GetComponent<PlantData>().CurrentLevel.cost;
    }

    private void sellPlant()
    {
        // Sell for half-price
        gameManager.Gold += plant.GetComponent<PlantData>().CurrentLevel.cost / 2;
        Destroy(plant);
    }
}
