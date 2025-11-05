using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedLimit : MonoBehaviour
{
    private MotorcycleControl motorcycleControl;

    [Tooltip("1 = 6 K/M")]
    public float speedLimit = 15f;
    public int scoreLose;
    public bool violate = false;
    public GameObject popUpScore;
    public TMP_Text subScoreText;
    public Transform parentTransform;

    void Start()
    {
        motorcycleControl = FindObjectOfType<MotorcycleControl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Motorcycle"))
        {
            violate = false;
            UIControl.Instance.GetSpeedLimitText();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Motorcycle") && motorcycleControl.currentSpeed > speedLimit && violate == false)
        {
            GameManager.Instance.SubtractScore(scoreLose);
            Debug.Log("-" + scoreLose);
            subScoreText.text = "Melanggar Kecepatan\n-" + scoreLose.ToString();
            Instantiate(popUpScore, parentTransform);
            violate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Motorcycle"))
        {
            violate = false;
            UIControl.Instance.RemoveSpeedLimitText();
        }
    }
}
