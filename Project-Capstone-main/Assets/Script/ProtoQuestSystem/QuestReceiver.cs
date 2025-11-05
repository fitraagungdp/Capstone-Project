using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    public int questId;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            QuestSystem.instance.CompleteQuest(questId);
        }
    }

    //private void OnTriggerExit(Collider col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        playerIsHere = false;
    //    }
    //}
}
