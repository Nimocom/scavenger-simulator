using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IInteractable
{
    [SerializeField] float loadingTime;
    Player player;
    bool isLoading;
    float timer;

    public void Interact(Player player)
    {
        if (!player.Garbage.activeSelf)
            return;

        this.player = player;
        player.PlayerMovement.BlockControl = true;
        isLoading = true;
        JobSlider.Instance.ShowSlider(loadingTime);
        transform.root.GetComponent<Animator>().SetBool("IsOpen", true);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;

            if (timer >= loadingTime)
            {
                player.Garbage.SetActive(false);
                player.PlayerMovement.BlockControl = false;
                Truck.Instance.LoadGarbage(GarbageBin.currentGarbageWeight);
                isLoading = false;
                timer = 0f;
                transform.root.GetComponent<Animator>().SetBool("IsOpen", false);
            }
        }
    }
}
