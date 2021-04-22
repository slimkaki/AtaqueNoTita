using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour  {

    Animator animator;
    GameObject eren;
    GameManager gm;
    void Start() {
        gm = GameManager.GetInstance();
        animator = GetComponent<Animator>();
        eren = GameObject.FindWithTag("Player");
    }

    void Update(){
        if(gm.gameState != GameManager.GameState.GAME) return;
        float dist = Vector3.Distance(eren.transform.position, this.transform.position);
        if (dist < 5f && !animator.GetBool("CloseEren")) {
            animator.SetBool("CloseEren", true);
        } else if (dist > 5f && animator.GetBool("CloseEren"))  {
            animator.SetBool("CloseEren", false);
        } 
    }

}
