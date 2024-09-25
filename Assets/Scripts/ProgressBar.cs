using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float FILLSPEED;
    [SerializeField] private ParticleSystem particles;
    
    [SerializeField] private GameManager gameManager;

    public static TextMeshProUGUI sliderValueText;
    private Slider slider;
    private float targetProgress = 0;

    private void Awake()
    {
    
        particles = GameObject.Find("Progress Bar Particles").GetComponent<ParticleSystem>();
        slider = gameObject.GetComponent<Slider>();
        sliderValueText = GetComponentInChildren<TextMeshProUGUI>();
    }
   
    private void FixedUpdate()
    {
        sliderValueText.text = slider.value.ToString();

        if (slider.value < targetProgress)
        {
            slider.value += FILLSPEED * Time.fixedDeltaTime;
            particles.Play();
        }
        else
            particles.Stop();
    }

    public void IncrementProgress(float progress)
    {
        targetProgress = slider.value + progress;
        print("Target Progress: " + targetProgress);
    }
}
