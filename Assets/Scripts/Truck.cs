using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Truck : MonoBehaviour, IInteractable
{
    public static Truck Instance { get; private set; }

    public float StorageFullness;
    public float StorageCapacity;
    public float money;
    [SerializeField] Transform rightDoorPoint;
    public TruckController TruckController { get; private set; }
    new Rigidbody rigidbody;

    Player player;
    bool hasDriver;

    [SerializeField] GameObject collisionEffect;
    [SerializeField] Slider fullnessSlider;
    [SerializeField] TextMeshProUGUI capacityText;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        Instance = this;

        TruckController = GetComponent<TruckController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(Player player)
    {
        if (player.Garbage.activeSelf)
            return;

        this.player = player;
        player.PlayerMovement.BlockControl = true;
        player.NavMeshAgent.SetDestination(rightDoorPoint.position);
        StartCoroutine(CheckForPlayerPosition());
    }

    // Start is called before the first frame update
    void Start()
    {
        fullnessSlider.maxValue = StorageCapacity;
        capacityText.SetText(StorageCapacity.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDriver)
            if (rigidbody.velocity.magnitude < 0.3f)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.transform.position = rightDoorPoint.position;
                    player.gameObject.SetActive(true);
                    player.PlayerMovement.BlockControl = false;
                    CameraController.Instance.SetTarget(player.transform, 0);
                    hasDriver = false;
                    TruckController.IsActive = false;
                }
    }

    IEnumerator CheckForPlayerPosition()
    {
        while (Vector3.Distance(player.transform.position, rightDoorPoint.position) > 1.1f)
        {
            yield return null;
        }

        player.gameObject.SetActive(false);
        CameraController.Instance.SetTarget(transform, 1);
        hasDriver = true;
        TruckController.IsActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(collisionEffect, collision.contacts[0].point, Quaternion.identity);
    }

    public void LoadGarbage(float weight)
    { 
        StorageFullness += weight;

        if (StorageFullness > StorageCapacity)
            StorageFullness = StorageCapacity;

        fullnessSlider.value = StorageFullness;
    }

    public void UnloadGarbage(int costPerKg)
    {
        money += StorageFullness * costPerKg;
        moneyText.SetText(money.ToString() + "$");
        StorageFullness = 0f;
        fullnessSlider.value = StorageFullness;
    }
}
