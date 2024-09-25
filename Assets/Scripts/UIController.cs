using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TEXT;
    [SerializeField] private ProgressBar progressbar;
    
    [SerializeField] private GameManager gm;

    // Start is called before the first frame update

    private void Awake()
    {
        TEXT = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        TEXT.gameObject.SetActive(false);
        
    }


    public void SetMessage(string message)
    {
        TEXT.gameObject.SetActive(true);
        TEXT.text = message;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gm.StartGame();
        }
    }

    public void IncrementProg(float val)
    {
        progressbar.IncrementProgress(val);
    }
}
