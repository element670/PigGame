using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class GameManager : MonoBehaviour
{
    public static readonly string ENEMY_ACTION = "Collision";
    [SerializeField] private ChildPig[] childPig;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Pig pig;
    [SerializeField] private float duration;
    [SerializeField] private GameObject childrenHolder;
    [SerializeField] private UIController uicontroller;
    private int quantity;
    private GameObject list;
    private Shake shake;
    private AudioManager audioManager;

    public int collectChildren = 0;



    private GameState gameState = GameState.IDLE;
    private void Awake()
    {
        quantity = childrenHolder.transform.childCount;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<Shake>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void StartGame()
    {

        gameState = GameState.START;
        EventBus.Register<Pig.Action>(Pig.ACTION, GameOver);


        for (int i = 0; i < childPig.Length; i++) { childPig[i].SetGameStart(true); }
        enemyAI.SetStartGame(true);
        pig.SetStartGame(true);
        pig.gameObject.SetActive(true);


    }

    // Update is called once per frame
    public void GameOver(Pig.Action action)
    {

        if (action.collision == Pig.Collision.COLLIDE_WITH_ENEMY)
        {
            pig.gameObject.SetActive(false);
            GameOver(GameState.GAME_OVER);
            uicontroller.SetMessage("GAME IS OVER");
            shake.CameraShake();
            audioManager.PlaySFX(audioManager.death);
            audioManager.PlaySFX(audioManager.death);
        }
        if (action.collision == Pig.Collision.COLLECT)
        {
            action.child.SetSpeed(0);
            action.child.gameObject.transform.parent = pig.gameObject.transform;
            
            audioManager.PlaySFX(audioManager.whooshSound);
            ++collectChildren;
            uicontroller.IncrementProg((float)1 / quantity);
            print("Progress" + (float)collectChildren / quantity);
            
            shake.CameraShake();
        }
    }
    private void GameOver(GameState gameState)
    {
        EventBus.Unregister(Pig.ACTION, (System.Action<Pig.Action>)GameOver);
        this.gameState = gameState;


        for (int i = 0; i < childPig.Length; i++) { childPig[i].SetGameStart(false); }
        enemyAI.SetStartGame(false);
        pig.SetStartGame(false);
    }

    void Update()
    {
        if (gameState != GameState.START)
        {
            return;
        }

        Timer(duration);
        
        if(duration < 0)
        {
            TimeIsOver();
        }
        else if(collectChildren.Equals(quantity)) 
        {
            GameIsOver();
        }


    }
    private void TimeIsOver()
    {
        GameOver(GameState.TIMEOVER);
        uicontroller.SetMessage("TIME IS OVER");
    }

    private void GameIsOver()
    {
        GameOver(GameState.GAME_OVER);
        uicontroller.SetMessage("GAME IS OVER");
    }

    public enum GameState
    {
        IDLE, START, TIMEOVER, GAME_OVER
    }

    private void Timer(float time)
    {
        if (time > 0)
        {
            duration -= Time.deltaTime;
        }
    }


}
