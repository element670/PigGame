using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetToNormal()
    {
        Time.timeScale = Mathf.Lerp(1, 0.1f, 5);
    }

    public void SlowDown()
    {
        Time.timeScale = Mathf.Lerp(0.1f, 1, 5);
    }
}
