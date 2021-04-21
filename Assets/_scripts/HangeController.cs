using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangeController : MonoBehaviour{    
    
    GameManager gm;
    // Falar a missão no início do game/fase
    // Como reloadar o gás
    // Como atacar os titãs

    [SerializeField]
    public Text message;
  
    void OnEnable()  {
        gm = GameManager.GetInstance();
        message.text = $"tutorial";
        
        if (gm.gas <= 0 && gm.firstPlay[1]) {
            message.text = $"O seu gás acabou! Para recarregar, aperte a tecla R!!!";
        }
        if (gm.firstPlay[2]) {
            message.text = $"Você acabou de encntrar seu primeiro titã! Para mata-lo, basta chegar em sua nuca e apertar a tecla Q!";
        }
    }

    public void BackToGame() {
        gm.ChangeState(GameManager.GameState.GAME);
    }

}
