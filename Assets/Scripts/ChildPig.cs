using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ChildPig : MonoBehaviour
{
   
    [SerializeField] private GameObject pig;
    [SerializeField] private Transform enemy;
    [SerializeField] private float SPEED;
    [SerializeField] private GameObject squarePrefab;
    private bool isGameStarted;
    private void Update()
    {
  
        if (!isGameStarted) { return; }

        var direction = transform.position - enemy.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.position = Vector2.MoveTowards(transform.position, enemy.position, Time.deltaTime * SPEED);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    public void SetGameStart(bool gameStart) { this.isGameStarted = gameStart; }
    public void SetSpeed(float SPEED) { this.SPEED =  SPEED; }
}
