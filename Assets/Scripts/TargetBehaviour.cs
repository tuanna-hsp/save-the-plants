using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour
{
    private const int IRRITATE_DURATION = 2;

    public GameObject leftEye;
    public GameObject rightEye;

    private EyesBehaviour leftEyeBehaviour;
    private EyesBehaviour rightEyeBehaviour;
    private float irritateStartTime = 0;

    // Use this for initialization
    void Start()
    {
        leftEyeBehaviour = leftEye.GetComponent<EyesBehaviour>();
        rightEyeBehaviour = rightEye.GetComponent<EyesBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (irritateStartTime != 0 && (Time.time - irritateStartTime) > IRRITATE_DURATION)
        {
            irritateStartTime = 0;
            leftEyeBehaviour.LookCenter();
            rightEyeBehaviour.LookCenter();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            leftEyeBehaviour.LookLeft();
            rightEyeBehaviour.LookLeft();
            irritateStartTime = 0;

            other.GetComponent<EnemyDestructionDelegate>().enemyDelegate = OnEnemyDestroyed;
        }
    }

    void OnEnemyDestroyed(GameObject enemy)
    {
        leftEyeBehaviour.LookIrritate();
        rightEyeBehaviour.LookIrritate();
        irritateStartTime = Time.time;
        Debug.Log("Irritate");
    }
}
