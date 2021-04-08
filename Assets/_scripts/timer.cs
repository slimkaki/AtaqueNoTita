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
		slider.maxValue = gm.time;
		slider.value = gm.time;

	}

    public void SetTime(int time)
	{
		slider.value = time;

	}
	public void Update(){

		SetTime(gm.time);

	}

}
