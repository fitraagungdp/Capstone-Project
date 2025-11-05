using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public GameObject[] pickupPoints;
    public GameObject[] dropPoints;
    public bool isTutorial;

    public float timeLimit = 60.0f; // Time limit for each quest
    private float remainingTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartRandomQuest();
        remainingTime = timeLimit;
        Time.timeScale = 1;
    }

    private void Update()
    {
        // Decrease the remaining time
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            if (isTutorial)
            {
                
            }
            else
            {
                GameManager.Instance.GameOver();
            }   
        }
    }

    public void StartRandomQuest()
    {
        // Initialize remaining time for the new quest
        remainingTime = timeLimit;
        ActivateRandomPickupPoint();
    }

    public void ActivateRandomPickupPoint()
    {
        if (pickupPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, pickupPoints.Length);
            pickupPoints[randomIndex].SetActive(true);
        }
    }

    public void ActivateRandomDropPoint()
    {
        if (dropPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, dropPoints.Length);
            dropPoints[randomIndex].SetActive(true);
        }
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

}
