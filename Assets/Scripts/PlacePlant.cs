using UnityEngine;
using System.Collections;

public class PlacePlant : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject plantSelectorPrefab;
    public GameObject rangePrefab;

    private GameObject plant;
    private GameObject rangePreview;
    private GameManagerBehavior gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
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
        RectTransform rectTransform = (RectTransform) plantSelector.transform;
        rectTransform.position -= new Vector3(0, rectTransform.rect.height);
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

    private bool canUpgradeMonster()
    {
        if (plant != null)
        {
            PlantData monsterData = plant.GetComponent<PlantData>();
            PlantLevel nextLevel = monsterData.getNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    private bool canPlaceMonster()
    {
        int cost = plantPrefab.GetComponent<PlantData>().levels[0].cost;
        return plant == null && gameManager.Gold >= cost;
    }
}
