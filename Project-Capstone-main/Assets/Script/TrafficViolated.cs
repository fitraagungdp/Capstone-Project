using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrafficViolated : MonoBehaviour
{
    public int scoreLoseTraf = 1000;
    public int scoreLoseEnv = 50;
    public int scoreLoseCar = 1000;
    public LayerMask SidewalkLayer;
    public GameObject popUpScore;
    public TMP_Text subScoreText;
    public Transform parentTransform;
    private float collisionCooldownTimer = 1.0f;

    void Update()
    {
        if (collisionCooldownTimer > 0.0f)
        {
            collisionCooldownTimer -= Time.fixedDeltaTime;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Traffic Light"))
        {
            TrafCollision();
        }

        if (collision.gameObject.CompareTag("Car") && collisionCooldownTimer <= 0.0f)
        {
            CarCollision();
        }

        if ((SidewalkLayer & 1 << collision.gameObject.layer) != 0)
        {
            EnvironmentCollision();
        }
    }

    private void TrafCollision()
    {
        GameManager.Instance.SubtractScore(scoreLoseTraf);
        Debug.Log("-" + scoreLoseTraf);
        subScoreText.text = "Melanggar Rambu Lalu Lintas\n-" + scoreLoseTraf.ToString();
        Instantiate(popUpScore, parentTransform);
    }

    private void CarCollision()
    {
        GameManager.Instance.SubtractScore(scoreLoseCar);
        Debug.Log("-" + scoreLoseCar);
        subScoreText.text = "Menabrak\n-" + scoreLoseCar.ToString();
        Instantiate(popUpScore, parentTransform);
    }

    private void EnvironmentCollision()
    {
        GameManager.Instance.SubtractScore(scoreLoseEnv);
        Debug.Log("-" + scoreLoseEnv);
        subScoreText.text = "Membentur\n-" + scoreLoseEnv.ToString();
        Instantiate(popUpScore, parentTransform);
    }
}
