using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem instance;

    public List<Quest> QuestList = new List<Quest>();

    Quest activeQuest;

    bool playerIsHere;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsHere)
        {
            AddQuest();
        }
    }

    // Start is called before the first frame update
    void AddQuest()
    {
        if(activeQuest == null)
        {
            activeQuest = QuestList[Random.Range(0, QuestList.Count)];

            Debug.Log("Quest Start");
        }
    }

    public int ReadActiveQuest()
    {
        return activeQuest.id;
    }

    public void CompleteQuest(int _id)
    {
        if(activeQuest == null)
        {
            return;
        }

        if(activeQuest.id == _id)
        {
            //complete

            Debug.Log("Quest Number" + activeQuest.id + "is COMPLETE");

            activeQuest = null;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsHere = true;
            Debug.Log("player here");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsHere = false;
            Debug.Log("player not here");
        }
    }

}
