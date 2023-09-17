using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTower : MonoBehaviour
{
    [Header (" Elements ")]
    [SerializeField] Transform crowdHolder;
    [SerializeField] List<Transform> players;
    
    [Header (" Settings ")]
    [SerializeField] int floorCountLimit;
    [SerializeField] float xDistance;
    [SerializeField] float ydistance;

    int playerCount;
    WaitForSeconds waitingTime = new WaitForSeconds (0.009f);

    public void StartBuilding()
    {
        playerCount = crowdHolder.childCount;
        //InsertPlayer();

        StartCoroutine(nameof(InstertingPlayer));
    }

    IEnumerator InstertingPlayer ()
    {
        for (int i = 0; i < playerCount; i++)
        {
            players.Add(crowdHolder.GetChild(i));
        }

        Vector3 _positoin;
        // for placing the first player in all row 
        float positionX = xDistance;
        
        // used for making pyramid
        float formation = xDistance;
        float positiony = 0;
        
        // for addding more rows
        int counter = 1;

        for (int i = 0; i < players.Count; i++)
        {
            _positoin = new Vector3(positionX, positiony, 0);
            players[i].localPosition = _positoin;

            positionX -= xDistance / 2;
            counter++;

            if (counter == floorCountLimit)
            {
                positionX = xDistance;
                positiony += ydistance;

                counter = 1;

                // 15 players to make a pyramid of 5 layer
                if (players.Count - i < 15)
                {
                    floorCountLimit--;
                    if(floorCountLimit >= 2)
                    {
                        // sliding the formation of what 1 player adds to it
                        formation -= 0.2f;
                    }
                    positionX = formation;
                }
            }
            yield return waitingTime;
        }
    }

    // TESTING---------------------------------------------------
    private void InsertPlayer()
    {
        for (int i = 0; i < playerCount; i++)
        {
            players.Add(crowdHolder.GetChild(i)); 
        }

        Vector3 _positoin;
        float positionX = xDistance;
        float positiony = 0;
        int counter = 1;
        
        for (int i = 0; i < players.Count; i++)
        {
            _positoin = new Vector3(positionX, positiony, 0);
            players[i].localPosition = _positoin;

            positionX -= xDistance;
            counter++;

            if (counter == floorCountLimit)
            {
                positionX = 0.9f;
                positiony += ydistance;

                counter = 1;
                if (players.Count - i < 15)
                {
                    floorCountLimit--;
                }
            }
        }
    }
}

[System.Serializable]
public class FloorLevel
{
    public List<Transform> floor;
}