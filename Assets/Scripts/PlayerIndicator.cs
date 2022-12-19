using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactionLayer;
    Collider[] interationTargets;
    int targets;

    private void Awake()
    {
        interationTargets = new Collider[4];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targets = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, interationTargets, interactionLayer.value);

            if (targets > 0)
            {
                print("DF2");
                interationTargets[0].GetComponent<IInteractable>().Interact(Player.Instance);
            }
        }


    }
}

public interface IInteractable
{
    public void Interact(Player player);
}