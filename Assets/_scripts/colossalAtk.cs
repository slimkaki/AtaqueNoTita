using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class colossalAtk : MonoBehaviour
{
    public float agroRange; 
    Vector2 originalPos= new Vector2( -5.82f ,0.63f ); // EDITAR POS ORIGINAL
    private float cooldown, atkTime, backTime;
    private bool attacking, backBool;
    private Vector3 initialPos;
    
    Rigidbody2D rb;
    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
        cooldown = 5f;
        atkTime = Time.time;
        backBool = attacking = false;
        rb = GetComponent<Rigidbody2D>();
        initialPos = this.transform.position;
    }

    void Update() {
        if (GameObject.FindWithTag("Player").transform.position.x - 0.5f > this.transform.position.x) {
            return;
        }
        float distToPlayer = Vector2.Distance(this.transform.position, GameObject.FindWithTag("Player").transform.position);
        if (distToPlayer <= agroRange && !attacking){
            RotateTowards(GameObject.FindWithTag("Player").transform.position);
        }

        float randomAttack =  Random.Range(0.0f,1.0f);
        if (randomAttack > 0.85f) {
            if (Time.time - atkTime > cooldown && distToPlayer <= 6f) {
                attacking = true;
                gm.titanAtk = true;
                attack(GameObject.FindWithTag("Player").transform.position);
                atkTime = Time.time;
            }
        }
        if (Time.time - atkTime > 0.1f  && attacking) {
            attacking = false;
            backBool = true;
            rb.velocity = new Vector2(0,0);
            backAttack(GameObject.FindWithTag("Player").transform.position);
            backTime = Time.time;
        }

        if (Time.time - backTime > 0.1f && backBool) {
            rb.velocity = new Vector2(0,0);
            if (Vector3.Distance(this.transform.position, initialPos) != 0.0f) {
                this.transform.position = initialPos;
            }
            backBool = false;
        }

    }

    // Função retirada de uma questão presente no fórum da unity 
    // https://answers.unity.com/questions/1592029/how-do-you-make-enemies-rotate-to-your-position-in.html
    private void RotateTowards(Vector2 target) {
        if (target.x - 0.5f > this.transform.position.x) {
            return;
        }
        var offset = 0f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;       
        this.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
   
    void attack(Vector2 target) {
        Vector2 direction = target - (Vector2)transform.position;
        rb.velocity = new Vector2(direction.x*(2f),direction.y*(2f));
    }

    void backAttack(Vector2 target) {
        Vector2 direction = target - (Vector2)transform.position;
        rb.velocity = new Vector2(-direction.x*(2f),-direction.y*(2f));
        // rb.velocity = new Vector2(0,0);

    }
   
}
