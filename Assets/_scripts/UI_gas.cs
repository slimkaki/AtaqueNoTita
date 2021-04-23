using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_gas : MonoBehaviour
{
   Text textComp;
   GameManager gm;
   void Start() {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();
       GetComponent<Text>().enabled = false;
   }
   
   void Update() {
        if(gm.gameState != GameManager.GameState.GAME) {
            GetComponent<Text>().enabled = false;
            return;
        }

        if(gm.gas <= 0)
            GetComponent<Text>().enabled = true;
        else
            GetComponent<Text>().enabled = false;
   }
}