using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropPoint : MonoBehaviour
{
    private bool questCompleted = false;
    public int scoreReward;
    public PlayerControl playerControl;
    public bool isTutorial;

    public GameObject popUpScore;
    public TMP_Text subScoreText;
    public Transform parentTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the quest is not already completed
            if (!questCompleted)
            {
                // Handle player interaction (e.g., pressing "E" to complete the quest)
                // For now, just set a boolean to indicate the quest is completed
                questCompleted = true;
                Debug.Log("questCompleted");
                GameManager.Instance.AddScore(scoreReward);
                Invoke("ResetQuestCompleted", 0.1f);

                // Deactivate this drop point
                gameObject.SetActive(false);
                Debug.Log("+" + scoreReward);
                subScoreText.text = "Misi Selesai\n+" + scoreReward.ToString();
                Instantiate(popUpScore, parentTransform);

                playerControl.SetCarryingPackage(false);
                playerControl.GetBoolCarryAnimationFalse();
                // Activate a random pickup point for the next quest
                PhoneSystem.Instance.GetButton().SetActive(true);


                // If it's a tutorial, trigger the tutorialOver function
                if (isTutorial)
                {
                    GameManager.Instance.TutorialDone();
                }
            }
        }
    }
    private void ResetQuestCompleted()
    {
        questCompleted = false;
    }
}