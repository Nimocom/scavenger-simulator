using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    public GameObject Garbage;

    private void Awake()
    {
        Instance = this;

        NavMeshAgent = GetComponent<NavMeshAgent>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

}
