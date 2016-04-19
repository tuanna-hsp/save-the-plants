using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlantLevel {
  public int cost;
  public GameObject visualization;
  public GameObject bullet;
  public float fireRate;
}

public enum PlantType
{
    PLANT1, PLANT2, PLANT3
}

public class PlantData : MonoBehaviour {

	public List<PlantLevel> levels;

	private PlantLevel currentLevel;
    
	public PlantLevel CurrentLevel {
		get {
			return currentLevel;
		}
		set {
			currentLevel = value;
			int currentLevelIndex = levels.IndexOf(currentLevel);
			
			GameObject levelVisualization = levels[currentLevelIndex].visualization;
			for (int i = 0; i < levels.Count; i++) {
				if (levelVisualization != null) {
					if (i == currentLevelIndex) {
						levels[i].visualization.SetActive(true);
					} else {
						levels[i].visualization.SetActive(false);
					}
				}
			}
		}
	}

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	void OnEnable() {
 		CurrentLevel = levels[0];
	}

	public PlantLevel getNextLevel() {
		int currentLevelIndex = levels.IndexOf (currentLevel);
		int maxLevelIndex = levels.Count - 1;
		if (currentLevelIndex < maxLevelIndex) {
			return levels[currentLevelIndex+1];
		} else {
			return null;
		}
	}
	
	public void increaseLevel() {
		int currentLevelIndex = levels.IndexOf(currentLevel);
		if (currentLevelIndex < levels.Count - 1) {
			CurrentLevel = levels[currentLevelIndex + 1];
		}
	}
}
