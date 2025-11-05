using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoint : MonoBehaviour
{
    private bool questOn = false;
    private bool waitingForInput = false;
    public PlayerControl playerControl;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // Check if the quest is not already active
            if (!questOn && !waitingForInput)
            {
                // Indicate that we are waiting for player input
                waitingForInput = true;
            }
        }
    }

    private void Update()
    {
        // Check for player input in the Update method
        if (waitingForInput && Input.GetKeyDown(KeyCode.E))
        {
            // Handle player interaction to start the quest
            // For now, just set a boolean to indicate the quest is active
            questOn = true;

            // Deactivate this pickup point
            gameObject.SetActive(false);

            playerControl.SetCarryingPackage(true);
            playerControl.GetBoolRunningAnimationFalse();

            // Reset questOn for the next quest
            waitingForInput = false;
            Invoke("ResetQuestOn", 0.1f); // You may need a short delay
            // Activate a random drop point for the quest
            QuestManager.Instance.ActivateRandomDropPoint();
        }
    }

    // Function to reset questOn
    private void ResetQuestOn()
    {
        questOn = false;
    }
}

