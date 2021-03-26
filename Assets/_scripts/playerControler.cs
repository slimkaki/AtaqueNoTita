using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    // GameManager gm;
    private float positionX, positionY;
    private bool jumpFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        // gm = GameManager.GetInstance();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (jumpFlag) {
            positionY += 0.01f;
        }                
        if (Input.GetKey(KeyCode.LeftArrow)) {
            positionX -= 0.01f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            positionX += 0.01f;
        }
        if (Input.GetAxisRaw("Jump") != 0){
            jumpFlag = true;
        }
        
        
        

        this.transform.position = new Vector3(positionX, positionY, this.transform.position.z);
    }
    
}
