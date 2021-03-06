﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrientationPosition
{
    public Transform position;
    public Worker worker;
}

public class OrientationBuilding : MonoBehaviour
{
    [Header("Camera")]
    public Transform RoomCamera;

    [Header("Workers")]
    public WorkerInfo workerTemplate;
    public int minimumNumberOfWorkersPerDay;
    public OrientationPosition[] WorkersPositions;
    public int numberOfWorkers = 0;

    Dictionary<int, float> randomBag; 

    void InstantiateDictionary(int minimum = 2,int numberOfWorkers = 5)
    {
        randomBag = new Dictionary<int, float>(numberOfWorkers);

        for (int i = minimum; i <= numberOfWorkers; i++)
        {
            randomBag[i] = 0.0f;
        }
    }

    int GetRandomNumber()
    {
        int r;
        r = Random.Range(minimumNumberOfWorkersPerDay, WorkersPositions.Length+1);
        //randomBag[r] += 0.2f;
        return r;
    }

    void ClearWorkers()
    {
        if(numberOfWorkers > 0)
        {
            for (int i = 0; i < WorkersPositions.Length; i++)
            {
                if (WorkersPositions[i].worker != null)
                {
                    Destroy(WorkersPositions[i].worker.gameObject);
                }

                WorkersPositions[i].worker = null;
            }
        }
    }

    public void NewWorkers()
    {
        ClearWorkers();
        int r = GetRandomNumber();

        for (int i = 0; i < r; i++)
        {
            var w = workerTemplate.GetWorkerPrefab(workerTemplate.GetGender());
            GameObject s = Instantiate(w, WorkersPositions[i].position);
            WorkersPositions[i].worker = s.GetComponent<Worker>();
            WorkersPositions[i].worker.RandomizeWorker();

            WorkersPositions[i].worker.gameObject.GetComponent<SeekRoom>().wayPointCurrent = WorkersPositions[i].position.GetComponent<WayPoint>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < WorkersPositions.Length; i++)
        {
            Gizmos.DrawWireSphere(WorkersPositions[i].position.position,0.5f);
        }
    }

}
