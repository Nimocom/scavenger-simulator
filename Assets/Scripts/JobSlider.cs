using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSlider : MonoBehaviour
{
    public static JobSlider Instance;

    Slider slider;

    private void Awake()
    {
        Instance = this;
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += Time.deltaTime;
        if (slider.value == slider.maxValue)
            Invoke("Hide", 1f);
    }

    public void ShowSlider(float maxValue)
    {
        slider.value = 0f;
        slider.maxValue = maxValue;
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
