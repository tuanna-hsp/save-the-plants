using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {
	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;

	public float speed = 1.0f;
    public bool isFly = false;

    private bool isVibrationEnabled;
    private float totalSlowTime;
    private float maxSlowTime;
    private bool isBeingSlow;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

		lastWaypointSwitchTime = Time.time;
        if (isFly)
        {
            // Fly enemy only need start and end point
            waypoints = new GameObject[] { waypoints[0], waypoints[waypoints.Length - 1] };
        }

        RotateIntoMoveDirection();

        if (!PersistantManager.IsAmbientEnabled())
        {
            GetComponent<AudioSource>().enabled = false;
        }

        isVibrationEnabled = PersistantManager.IsVibrationEnabled();
	}
	
	// Update is called once per frame
	void Update () {
		// 1 
		Vector3 startPosition = waypoints [currentWaypoint].transform.position;
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
        // 2 
        float tempSpeed = speed;
        if (isBeingSlow)
        {
            totalSlowTime += Time.deltaTime;
            if (totalSlowTime >= maxSlowTime)
            {
                isBeingSlow = false;
                animator.SetBool("isSlow", false);
            }
            tempSpeed = tempSpeed * 60 / 100;
        }
		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / tempSpeed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
		// 3 
		if (gameObject.transform.position.Equals(endPosition)) {
			if (currentWaypoint < waypoints.Length - 2) {
				// 4 Switch to next waypoint
				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;
			
				RotateIntoMoveDirection();
			} else {
				// 5 Destroy enemy
				Destroy(gameObject);
 
				AudioSource audioSource = gameObject.GetComponent<AudioSource>();
				AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

				GameManagerBehavior gameManager =
					GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
				gameManager.Health -= 1;
                if (isVibrationEnabled)
                {
                    Handheld.Vibrate();
                }
			}
		}
	}

	private void RotateIntoMoveDirection() {
        Debug.Log("Enemy rotate");
		//1
		Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);
		//2
		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
		//3
		GameObject sprite = (GameObject)
			gameObject.transform.FindChild("Sprite").gameObject;
		sprite.transform.rotation = 
			Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}

	public float distanceToGoal() {
		float distance = 0;
		distance += Vector3.Distance(
			gameObject.transform.position, 
			waypoints [currentWaypoint + 1].transform.position);
		for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
			Vector3 startPosition = waypoints [i].transform.position;
			Vector3 endPosition = waypoints [i + 1].transform.position;
			distance += Vector3.Distance(startPosition, endPosition);
		}
		return distance;
	}

    public void MakeSlow(int timeInSecond)
    {
        isBeingSlow = true;
        maxSlowTime = timeInSecond;
        totalSlowTime = 0;
        animator.SetBool("isSlow", true);
    }
}
