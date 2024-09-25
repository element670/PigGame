using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private Animator cam;
    public void CameraShake()
    {
        cam.SetTrigger("Shake");
    }
}
