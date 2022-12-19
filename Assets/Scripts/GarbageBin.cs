using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBin : MonoBehaviour, IInteractable
{
    bool isCleaning;
    float timer;
    Player player;
    [SerializeField] float weight;

    [SerializeField] float cleaningTime;

    public static float currentGarbageWeight;
    bool isCleaned;

    public void Interact(Player player)
    {
        if (isCleaned)
            return;

        this.player = player;
        player.PlayerMovement.BlockControl = true;
        isCleaning = true;
        JobSlider.Instance.ShowSlider(cleaningTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCleaning)
        {
            timer += Time.deltaTime;

            if (timer > cleaningTime)
            {
                player.PlayerMovement.BlockControl = false;
                player.Garbage.SetActive(true);
                currentGarbageWeight = weight;
                isCleaned = true;
                isCleaning = false;
                timer = 0f;
            }
        }
    }
}
