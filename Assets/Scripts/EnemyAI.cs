using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    private bool isGameStarted;

    CameraZoom cameraZoom;
    float distance;
    private void Awake()
    {
        cameraZoom = GetComponent<CameraZoom>();
    }

    public void SetStartGame(bool isGameStarted)
    {
        this.isGameStarted = isGameStarted;
    }
    
    private void Update()
    {
        if (!isGameStarted) { return; }
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 3.5f);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
 
    

}
