using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telhadoSpawner : MonoBehaviour
{
  public GameObject Telhado_G;
  public GameObject Telhado_M;
  public GameObject Telhado_P;
//   GameManager gm;

  void Start()
  {
    //   gm = GameManager.GetInstance();
    //   GameManager.changeStateDelegate += Construir;
      Construir();
  }

  void Construir()
  {
     

    //    if (gm.gameState == GameManager.GameState.MENU)
    //   {
          foreach (Transform child in transform) {
              GameObject.Destroy(child.gameObject);
          }
          for(int i = 0; i < 2; i++)
          {
            Vector3 posicao = new Vector3(-4 + 5.533f*i,-4.161f,0.0f);
            GameObject tile = Instantiate(Telhado_G, posicao, Quaternion.identity, transform);
          }
      }
//   }

  void Update()
  {
    //   if (transform.childCount <= 0 && gm.gameState == GameManager.GameState.GAME)
    //   {
    //       gm.ChangeState(GameManager.GameState.ENDGAME);
    //   }
  }

}