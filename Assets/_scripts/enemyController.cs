using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour  {

    Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update(){
        GameObject eren = GameObject.FindWithTag("Player");
        float dist = Vector3.Distance(eren.transform.position, this.transform.position);
        if (dist < 5f && !animator.GetBool("CloseEren")) {
            animator.SetBool("CloseEren", true);
        } else if (dist > 5f && animator.GetBool("CloseEren"))  {
            animator.SetBool("CloseEren", false);
        }
    }
}
