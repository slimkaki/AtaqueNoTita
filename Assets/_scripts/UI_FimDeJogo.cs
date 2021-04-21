using UnityEngine;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour
{
   public Text message;

    GameManager gm;
   private void OnEnable()
   {
       gm = GameManager.GetInstance();

       if(gm.vidas <= 0) {
           Debug.Log("gm.vidas == 0");
            if(gm.pontos>0){
                message.text = $"Você morreu, mas a humanidade lembrará dos seus feitos!\nAbates: {gm.pontos}";
            }else{
                message.text = $"Você morreu, missão fracassada!!!";
            }
            
       }  else {
            if(gm.pontos >= 2){
                message.text = $"Missão completa!!\nAbates: {gm.pontos}";
                
            }else{
                message.text = $"Missão incompleta!!\nAbates: {gm.pontos}";
                
            }
        }
       
   }
   public void Voltar()
{
   gm.ChangeState(GameManager.GameState.MENU);
}
}