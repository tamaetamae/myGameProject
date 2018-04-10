﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MathMass : MonoBehaviour {
	int m_number;
	massstate m_state;
	Vector2 m_pos;
	ReactiveProperty<bool> wasGothrough = new ReactiveProperty<bool>();

	[SerializeField]
	TextMesh m_masskindtext;
	[SerializeField]
	TextMesh m_masscounttext;

	enum massstate {
		add,
		substract,
		multiplicate,
		divide,
	}

	//値に自分の数字を投げた時に答えを返してほしい
	public int caliculate(int oldnum) {
		if(m_state == massstate.add){
			return oldnum + m_number;
		}
		if (m_state == massstate.substract) {
			return oldnum - m_number;
		}
		if (m_state == massstate.multiplicate) {
			return oldnum * m_number;
		}
		if (m_state == massstate.divide) {
			return oldnum / m_number;
		}
		else return 999999;
	}

	public bool isGoThrough() {
		return wasGothrough.Value;
	}

	public void ChangeThrough() {
		wasGothrough.Value = true;
		deliteTextObject();
		this.transform.Rotate(90f,0f,0f);
	}


	public void ChangeDarkColor() {
		if (wasGothrough.Value == true) {//ここでの条件判定をするのではなく、subscribeのところのwhereで判定すべき。注意
			Color newColor = this.GetComponent<Renderer>().material.color;
			newColor.r = 0.1f;
			newColor.g = 0.1f;
			newColor.b = 0.1f;
			newColor.a = 0.6f;
			this.GetComponent<Renderer>().material.color = newColor;
		}
	}

	public void deliteTextObject() {
		Destroy(m_masskindtext.gameObject);
		Destroy(m_masscounttext);
}

	public void ChangeNormalColor() {
		Color newColor = this.GetComponent<Renderer>().material.color;
		newColor.r = 1f;
		newColor.g = 1f;
		newColor.b = 1f;
		newColor.a = 1f;
		this.GetComponent<Renderer>().material.color = newColor;
	}

	private void Start() {
		wasGothrough.AsObservable().Subscribe(_ => ChangeDarkColor());
	}

	private void RenewText() {
		m_masskindtext.text = GetMyString();
		m_masscounttext.text = m_number.ToString();
	}

	public void ChangeMynumber(int num) {
		 m_number = num;
		RenewText();

	}
	public void ChangeMyKind(int kindnum) {
		 m_state = (massstate)Enum.ToObject(typeof(massstate), kindnum);
		RenewText();
	}

	private String GetMyString() {
		if (m_state == massstate.add) {
			return "+";
		}
		if (m_state == massstate.substract) {
			return "-";
		}
		if (m_state == massstate.multiplicate) {
			return "×";
		}
		if (m_state == massstate.divide) {
			return "÷";
		}
		else {
			return "error";
		}
	}

	public void SetMyPos(int posX, int posY) {
		m_pos.x = posX;
		m_pos.y = posY;
	}


	public Vector2 GetMyPos() {
		return m_pos;
	}
}
