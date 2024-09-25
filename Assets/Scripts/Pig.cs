using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public static readonly string ACTION = "Collision";
    [SerializeField] private float SPEED;
    [SerializeField] private GameObject point;
    [SerializeField] private float spaceBetweenPoints;
    [SerializeField] private int numberOfPoints;
    [SerializeField] private float lanchForce;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float duration;
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private GameManager xp;

    [SerializeField] private GameObject squares;
    [SerializeField] private SlowMotion slowmotion;
   
    private bool isGameStarted;
    
    private int squareLength;
    private Vector2 pigPos;

    GameObject[] points;
    Vector2 direction;

    public void SetStartGame(bool isGameStarted)
    {
        this.isGameStarted = isGameStarted;
    }

    private void Start()
    {
        
        squareLength = squares.transform.childCount;

        
        pigPos = transform.position;
        
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (!isGameStarted) { return; }
        Vector3 pigPosition = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePosition - pigPosition;

        transform.right = direction;

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
        /*if(Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(LerpPosition());
        }*/
        MovePlayer(mousePosition);
        
    }
  
    Vector2 PointPosition(float t)
    {
        Vector2 pos = (Vector2)shotPoint.position + (direction.normalized * lanchForce * t) +(0.5f * Physics2D.gravity * t*t);
        return pos;
    }
    

    private IEnumerator LerpPosition()
    {
        
        GameObject dashInstance = Instantiate(dashEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(dashInstance);
    }
    private void MovePlayer(Vector3 mousePos)
    {
        Vector2 direction = transform.position - mousePos;
        direction.Normalize();

        
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.position = Vector2.MoveTowards(transform.position, mousePos, Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    public Vector2 GetPigPos()
    { return pigPos; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        
        if(this != null)
        {
            StartCoroutine(LerpPosition());
        }
        if (collision.tag == "Enemy")
        {
            EventBus.Trigger<Action>(ACTION, new Action(Collision.COLLIDE_WITH_ENEMY, null));
            
        }
        if (collision.gameObject.CompareTag("XP"))
        {
            
            EventBus.Trigger<Action>(ACTION, new Action(Collision.COLLECT, collision.gameObject.GetComponent<ChildPig>()));
        }
        print("You hit the collider");
        //slowmotion.SetToNormal();
    }

    public class Action
    {
        public Collision collision; 
        public ChildPig child;

        public Action(Collision collision, ChildPig child)
        {
            this.collision = collision;
            this.child = child;
        }
    }

    public enum Collision
    {
        COLLIDE_WITH_ENEMY, COLLECT
    }
}
