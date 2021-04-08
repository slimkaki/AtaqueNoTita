// Todo o script de health bar é de propriedade intelectual do autor Brackeys, video disponivel em: https://youtu.be/BLfNP4Sc_iA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
    GameManager gm;
	private void Start()
    {
        gm = GameManager.GetInstance();
		SetMaxHealth();
    }
	public void SetMaxHealth()
	{
		slider.maxValue = gm.vidas;
		slider.value = gm.vidas;

	}

    public void SetHealth(int health)
	{
		slider.value = health;

	}
	public void Update(){

		SetHealth(gm.vidas);

	}

}
