using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent agent;
    

    Stopwatch timer = new Stopwatch();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        timer.Start();

        print("started Navmesh timer");
    }

    private void Update()
    {

        agent.destination = movePositionTransform.position;

        float distance = Vector3.Distance(agent.transform.position, movePositionTransform.position);
        if (distance < 3.5)
        {
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;

            print("Navmesh Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
        }
    }
}
