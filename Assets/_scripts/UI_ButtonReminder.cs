using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonReminder : MonoBehaviour
{
   Text textComp;
   GameManager gm;
   void Start()
   {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();
   }
   
    void Update()
   {
        if (gm.pressQ) {
            textComp.text = $"Aperte Q para matar o Titã";
        } else {
            textComp.text = "";
        }
    }
}