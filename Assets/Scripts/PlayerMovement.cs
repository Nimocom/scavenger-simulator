using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    new Camera camera;

    RaycastHit raycastHit;
    Ray ray;

    Vector3 targetPoint;

    int animatorIDMove;

    public bool BlockControl;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        camera = Camera.main;

        animatorIDMove = Animator.StringToHash("Move");
    }

    private void Start()
    {
        targetPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BlockControl)
        {
            if (Input.GetMouseButton(1))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 30f))
                {
                    targetPoint = raycastHit.point;
                }
            }

            agent.SetDestination(targetPoint);
        }

        if (agent.desiredVelocity != Vector3.zero)
            animator.SetBool(animatorIDMove, true);
        else
            animator.SetBool(animatorIDMove, false);
    }

    private void OnEnable()
    {
        targetPoint = transform.position;
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.velocity;
    }
}
