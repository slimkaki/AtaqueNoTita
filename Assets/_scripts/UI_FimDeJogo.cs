using UnityEngine;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour
{
   public Text message;

    GameManager gm;
   private void OnEnable()
   {
       gm = GameManager.GetInstance();

       
       if(gm.time <= 0 && gm.pontos >= 40){
           message.text = $"Missão completa!! \n   Pontuação: {gm.pontos}";
         
       }
       else if(gm.time <= 0)
       {
           message.text = "Missão fracassada!!!";
       }
       else
       {
           message.text = "Você morreu!!";
       }
   }
   public void Voltar()
{
   gm.ChangeState(GameManager.GameState.MENU);
}
}