using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrafficSign : MonoBehaviour
{
    private MotorcycleControl motorcycleControl;
    private PlayerControl playerControl;
    public TextMeshProUGUI trafficSignText;

    public enum SignType
    {
        SpeedLimit,
        NoParking,
        NoStopping,
        SpeedMinimum
    }

    public SignType signType;
    public float speedLimit = 15f;
    public int scorePenalty;
    public float noStoppingDuration = 5f;

    private bool violated = false;
    private bool hasPenaltyBeenApplied = false;

    public GameObject popUpScore;
    public TMP_Text subScoreText;
    public Transform parentTransform;

    void Start()
    {
        motorcycleControl = FindObjectOfType<MotorcycleControl>();
        playerControl = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Motorcycle"))
        {
            violated = false;
            GetTrafficSignText();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Motorcycle") && !violated)
        {
            switch (signType)
            {
                case SignType.SpeedLimit:
                    CheckSpeedLimit();
                    break;
                case SignType.NoParking:
                    CheckNoParking();
                    break;
                case SignType.NoStopping:
                    CheckNoStopping();
                    break;
                case SignType.SpeedMinimum:
                    CheckSpeedMinimum();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Motorcycle"))
        {
            violated = false;
            RemoveTrafficSignText();
        }
    }

    private void CheckSpeedLimit()
    {
        StartCoroutine(DelayedSpeedLimitCheck());
    }

    private IEnumerator DelayedSpeedLimitCheck()
    {
        yield return new WaitForSeconds(3f); // Adjust the delay duration as needed

        if (motorcycleControl.currentSpeed > speedLimit && !violated)
        {
            GameManager.Instance.SubtractScore(scorePenalty);
            Debug.Log("-" + scorePenalty);
            subScoreText.text = "Melanggar Kecepatan\n-" + scorePenalty.ToString();
            Instantiate(popUpScore, parentTransform);
            violated = true;
        }
    }

    private void CheckSpeedMinimum()
    {
        StartCoroutine(DelayedSpeedMinimumCheck());
    }

    private IEnumerator DelayedSpeedMinimumCheck()
    {
        yield return new WaitForSeconds(5f); // Adjust the delay duration as needed

        if (motorcycleControl.currentSpeed < speedLimit && !violated)
        {
            GameManager.Instance.SubtractScore(scorePenalty);
            Debug.Log("-" + scorePenalty);
            subScoreText.text = "Melanggar Kecepatan\n-" + scorePenalty.ToString();
            Instantiate(popUpScore, parentTransform);
            violated = true;
        }
    }

    private void CheckNoParking()
    {
        // Check if the player is on foot and the motorcycle is placed
        if (!playerControl.IsMounted)
        {
            // Penalize the player for parking
            GameManager.Instance.SubtractScore(scorePenalty);
            Debug.Log("-" + scorePenalty);
            subScoreText.text = "Dilarang Parkir\n-" + scorePenalty.ToString();
            Instantiate(popUpScore, parentTransform);
            violated = true;
        }
    }

    private void CheckNoStopping()
    {
        if (!hasPenaltyBeenApplied)
        {
            StartCoroutine(NoStoppingCoroutine());
        }
    }

    private IEnumerator NoStoppingCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < noStoppingDuration)
        {
            // Check if the player is still stopping
            if (motorcycleControl.currentSpeed < 0.1f)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                elapsedTime = 0f; // Reset the timer if the player starts moving
            }

            yield return null;
        }

        if (!hasPenaltyBeenApplied)
        {
            // Penalize the player for stopping
            GameManager.Instance.SubtractScore(scorePenalty);
            Debug.Log("-" + scorePenalty);
            subScoreText.text = "Dilarang Berhenti\n-" + scorePenalty.ToString();
            Instantiate(popUpScore, parentTransform);

            violated = true;
            hasPenaltyBeenApplied = true; // Set the flag to true after applying the penalty
        }
    }

    public void GetTrafficSignText()
    {
        trafficSignText.enabled = true;
    }

    public void RemoveTrafficSignText()
    {
        trafficSignText.enabled = false;
    }
}
