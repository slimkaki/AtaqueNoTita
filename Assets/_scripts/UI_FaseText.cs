using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FaseText : MonoBehaviour {
   Text textComp;
   GameManager gm;
   private float tempo;
   void Start() {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();
       tempo = Time.time;
   }
   
   void Update() {
       if (gm.gameState == GameManager.GameState.MENU) tempo = Time.time;
       if (Time.time - tempo >= 5.0f) {
            textComp.text = "";
            return;
        }
       textComp.text = $"Missão: Abata os titãs e chegue até a base";
   }
}