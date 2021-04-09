using UnityEngine;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour
{
   public Text message;

    GameManager gm;
   private void OnEnable()
   {
       gm = GameManager.GetInstance();

       
       if(gm.pontos >= 2){
           message.text = $"Missão completa!!\nAbates: {gm.pontos}";
         
       }else{
           if(gm.vidas == 0) {
               message.text = $"Missão incompleta!!\nVocê morreu!\nAbates: {gm.pontos}";
           } else {
                message.text = $"Missão incompleta!!\nAbates: {gm.pontos}";
           }
         
       }

       if(gm.time <= 0)
       {
           message.text = "Missão fracassada!!!";
       }
       
   }
   public void Voltar()
{
   gm.ChangeState(GameManager.GameState.MENU);
}
}