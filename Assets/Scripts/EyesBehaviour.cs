using UnityEngine;
using System.Collections;

public class EyesBehaviour : MonoBehaviour
{
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject centerEye;
    public GameObject irritateEye;

    private GameObject lastEnabledEye;

    // Use this for initialization
    void Start()
    {
        lastEnabledEye = centerEye;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LookLeft()
    {
        if (lastEnabledEye != leftEye)
        {
            leftEye.SetActive(true);
            lastEnabledEye.SetActive(false);
            lastEnabledEye = leftEye;
        }
    }

    public void LookRight()
    {
        if (lastEnabledEye != rightEye)
        {
            rightEye.SetActive(true);
            lastEnabledEye.SetActive(false);
            lastEnabledEye = rightEye;
        }
    }

    public void LookCenter()
    {
        if (lastEnabledEye != centerEye)
        {
            centerEye.SetActive(true);
            lastEnabledEye.SetActive(false);
            lastEnabledEye = centerEye;
        }
    }

    public void LookIrritate()
    {
        if (lastEnabledEye != irritateEye)
        {
            irritateEye.SetActive(true);
            lastEnabledEye.SetActive(false);
            lastEnabledEye = irritateEye;
        }
    }
}
