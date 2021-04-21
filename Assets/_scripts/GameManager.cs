using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
   private static GameManager _instance;
   public enum GameState { MENU, GAME, PAUSE, TUTORIAL, ENDGAME };

   public GameState gameState { get; private set; }
   public int vidas;
   public int pontos;
   public int gas, tanque_de_gas;
   public Vector3 position;
   public bool pressQ = false;
   public bool trapped = false;
   public bool titanAtk = false;
   public bool[] firstPlay = {true, true, true}; // pos[0] -> inicio game; pos[1] -> reload do gas; pos[2] -> matar tita

   public static GameManager GetInstance()
   {
       if(_instance == null)
       {
           _instance = new GameManager();
       }

       return _instance;
   }
   private GameManager()
   {
        vidas = 10;
        pontos = 0;
        gas = 100;
        tanque_de_gas = 4;
        position = new Vector3(0.0f,-1.4f,0.0f);
        gameState = GameState.MENU; 
   }
    public delegate void ChangeStateDelegate();

    public static ChangeStateDelegate changeStateDelegate;

    public void ChangeState(GameState nextState)
    {

       if (nextState == GameState.MENU) Reset();
        
        if(gameState == GameState.PAUSE && nextState == GameState.MENU){
            GameObject.FindWithTag("Player").transform.position = position;
            
        }
        if(gameState == GameState.ENDGAME && nextState == GameState.MENU){
            GameObject.FindWithTag("Player").transform.position = position;
            
        }
        
        gameState = nextState;
        
        changeStateDelegate();
        
    }

    private void Reset()
    {
    vidas = 10;
    pontos = 0;
    gas = 100;
    tanque_de_gas = 4;
    }
}