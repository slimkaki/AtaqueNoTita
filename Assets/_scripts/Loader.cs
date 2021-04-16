using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Foi utilizado o tutorial: https://www.youtube.com/watch?v=3I5d2rUJ0pE
public class Loader : MonoBehaviour{ 

    
    public void SwitchScene(int cena) {
        SceneManager.LoadScene(cena);
    }
}
