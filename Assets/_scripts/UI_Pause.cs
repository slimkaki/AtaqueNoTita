using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Pause : MonoBehaviour
{

  GameManager gm;
  private GameObject eren;

  private void OnEnable()
  {
      gm = GameManager.GetInstance();
      eren = GameObject.FindWithTag("Player");
  }
 
  public void Retornar()
  {
      eren.GetComponent<playerControler>().UnpauseErenPhysics();
      gm.ChangeState(GameManager.GameState.GAME);
  }

  public void Inicio()
  {
      eren.GetComponent<playerControler>().UnpauseErenPhysics();
      gm.ChangeState(GameManager.GameState.MENU);
  }

}
