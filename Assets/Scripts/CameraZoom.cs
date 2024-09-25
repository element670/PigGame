using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    public void ZoomIn(Transform target)
    {
        _camera.transform.position = Vector2.Lerp(_camera.transform.position, target.position, Time.deltaTime * 2.5f);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 3, Time.deltaTime * 2);
    }
    public void ZoomOut()
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 5, Time.deltaTime * 2);
    }
    
}
