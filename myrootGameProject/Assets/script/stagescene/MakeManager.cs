﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class MakeManager : MonoBehaviour { //オブジェクト生成を行うクラス。

	float blocklength = Config.blocklength;

	int slidespace = 4;
	float groundhight;
	float instancehight;

	[SerializeField]
	Meditator meditator;

	[SerializeField]
	PrefabContainer objectcontainer;

	distinationSetter distinationmaker;

	void Start () { //オブジェクトの生成位置の取得
		groundhight = objectcontainer.getground ().transform.position.y;
		instancehight = groundhight + 0.5f;
		distinationmaker = gameObject.AddComponent<distinationSetter> ();
	}

	public void instanciatesetdistination (int[, ] _leveldesigndata, int i, int j) {
		GameObject[] instanceObjects = objectcontainer.getinstanceObjects();
		GameObject popobject;
		if (_leveldesigndata[i, j] != 0) { //0はアイテムなし
			popobject = Instantiate (instanceObjects[_leveldesigndata[i, j]], settingObjectPos (i, j, instancehight), Quaternion.identity) as GameObject;
			if (popobject.GetComponent<Goal> ()) {
				distinationmaker.setgoalobject (popobject);
			} else if (popobject.GetComponent<TargetMove> ()) {
				distinationmaker.addtargetobject (popobject);

			} else if (popobject.GetComponent<AidCharactor> ()) {
				distinationmaker.setAIDobject (popobject);
			}

		}
	}

	public void instanciateAllMapObject (int[, ] _leveldesigndata) { //playerやブロックなどのオブジェクトを生成するメソッド。
		for (int j = 0; j < _leveldesigndata.GetLength (1); ++j) {
			for (int i = 0; i < _leveldesigndata.GetLength (0); ++i) {
				if (_leveldesigndata[i, j] != 0) { //0はアイテムなし
					instanciatesetdistination (_leveldesigndata, i, j);
				}
			}
		}
		distinationmaker.setReachGoalMethod (meditator.getclearmanager().addRecentEatcount); //method化
		distinationmaker.setAidReachGoalMethod (meditator.getclearmanager().decleaseRecentEatcount); //method化
	}

	Vector3 settingObjectPos (int x, int y, float z) //i,jのインデックスからマップオブジェクトの生成位置を設定する処理。
	{
		Vector3 returnPos = new Vector3 (x * blocklength, z, y * blocklength);
		return returnPos;
	}
	public float getObjecthight () {
		return instancehight;
	}

	public GameObject InstanciateandGetRef (int onjectindex, Vector3 instancepos) { //オブジェクトを生成して参照を返すメソッド
		GameObject objectref;
		GameObject[] instanceObjects = objectcontainer.getitemObjects ();
		objectref = Instantiate (instanceObjects[onjectindex], instancepos, Quaternion.identity) as GameObject;
		return objectref;
	}

}