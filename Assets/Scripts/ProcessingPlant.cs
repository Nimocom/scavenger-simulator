using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingPlant : MonoBehaviour
{
    [SerializeField] int costPerKg;
    [SerializeField] float unloadingTime;
    Truck truck;
    new Collider collider;
    float timer;

    bool isEntered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEntered)
        {
            print(collider.attachedRigidbody.velocity.magnitude);
            if (collider.attachedRigidbody.velocity.magnitude < 1f)
            {
                timer += Time.deltaTime;

                if (timer >= unloadingTime)
                {
                    truck.UnloadGarbage(costPerKg);
                    isEntered = false;
                }
            }
            else
            {
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        truck = other.GetComponent<Truck>();
        collider = other;
        isEntered = truck;
    }

    private void OnTriggerExit(Collider other)
    {
        isEntered = false;
        timer = 0f;
    }
}
