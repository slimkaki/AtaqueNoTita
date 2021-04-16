// Todo o script de health bar é de propriedade intelectual do autor Brackeys, video disponivel em: https://youtu.be/BLfNP4Sc_iA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

	public Slider slider;
    GameManager gm;
	private void Start()
    {
        gm = GameManager.GetInstance();
        SetMaxTime();
    }
	public void SetMaxTime()
	{
		slider.maxValue = gm.gas;
		slider.value = gm.gas;

	}

    public void Setgas(int gas)
	{
		slider.value = gas;

	}
	public void Update(){

		Setgas(gm.gas);

	}

}
