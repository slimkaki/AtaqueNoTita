using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuPrincipal : MonoBehaviour
{
  GameObject Eren;
  GameManager gm;
  SpriteRenderer sR;

  void Start()
  {
    Eren = GameObject.FindWithTag("Player");
    sR = Eren.GetComponent<SpriteRenderer>();
    
  }

  private void OnEnable()
  {
      gm = GameManager.GetInstance();
  }
 
  public void Comecar()
  {
      Debug.Log("Comecei!");
      gm.ChangeState(GameManager.GameState.GAME);
      sR.enabled = true; 
      Eren.transform.position = gm.position;

  }
}