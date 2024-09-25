using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            SceneManager.LoadScene("SampleScene");
        }
    }

    
}
