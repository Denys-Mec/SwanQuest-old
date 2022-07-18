using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowHoldEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image _img; 

	public void OnPointerEnter(PointerEventData eventData)
	{
		_img.gameObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_img.gameObject.SetActive(false);
	}
}
