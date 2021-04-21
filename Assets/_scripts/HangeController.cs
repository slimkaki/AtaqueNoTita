using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangeController : MonoBehaviour{    
    
    GameManager gm;
    public GameObject eren, controles;
    // Falar a missão no início do game/fase
    // Como reloadar o gás
    // Como atacar os titãs

    [SerializeField]
    public Text message;
  
    void OnEnable()  {
        gm = GameManager.GetInstance();
        controles = GameObject.FindWithTag("ControlesImage");
        controles.GetComponent<Image>().enabled = false;
        message.text = $"tutorial";
        if (gm.firstPlay[0]) {
            controles.GetComponent<Image>().enabled = true;
            message.text = $"Missão: Abata todos os titãs e chegue até a base!";
        } else if (gm.gas <= 0 && gm.firstPlay[1]) {
            message.text = $"O seu gás acabou! Para recarregar, aperte a tecla R!!!";
        } else if (gm.firstPlay[2]) {
            message.text = $"Você acabou de encontrar seu primeiro titã! Para matá-lo, basta chegar em sua nuca e apertar a tecla Q!";
        }
        
    }

    public void BackToGame() {
        controles.GetComponent<Image>().enabled = false;
        // Unpause
        eren = GameObject.FindWithTag("Player");
        eren.GetComponent<playerControler>().UnpauseErenPhysics();
        gm.ChangeState(GameManager.GameState.GAME);
    }

}
