﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataCheck : MonoBehaviour {//データマネージャーに保存されている値との整合性を確認するクラス
	[SerializeField]
	MapDataManager datamanager;
	[SerializeField]
	MassDealer massdealer;


	public Vector2[] getOverRidePoint(Vector3 myposition, float blocklength)
	{//移動オブジェクトの存在する4点の座標を返すメソッド
		Vector2[] overridepoints = new Vector2[4];
		Vector2 cehckvector2 = massdealer.parseVector3XYZtoVector2XZ(myposition);
		int highx, lowx, highy, lowy;
		highx = (int)Math.Ceiling(cehckvector2.x / blocklength);
		highy = (int)Math.Ceiling(cehckvector2.y / blocklength);
		lowx = (int)Math.Floor(cehckvector2.x / blocklength);
		lowy = (int)Math.Floor(cehckvector2.y / blocklength);
		overridepoints[0].x = lowx; overridepoints[0].y = lowy;
		overridepoints[1].x = lowx; overridepoints[1].y = highy;
		overridepoints[2].x = highx; overridepoints[2].y = lowy;
		overridepoints[3].x = highx; overridepoints[3].y = highy;
		return overridepoints;
	}
	public bool checkCanSet(Vector3 checkvector3)//そこにおけるかを返すメソッド。
	{
		bool[,] cansetdatas = datamanager.getcanSetDatas();
		Vector2 checkvector2 = massdealer.parseVector3XYZtoVector2XZ(checkvector3);
		if (checkinIndex(checkvector2)) {
			Debug.Log(String.Format("{0}{1}{2}", (int)checkvector2.x, (int)checkvector2.y, cansetdatas[(int)checkvector2.x, (int)checkvector2.y].ToString()));
			return cansetdatas[(int)checkvector2.x, (int)checkvector2.y];
		}
		else { return false; }
	}
	private bool checkinIndex(Vector3 checkvector3)
	{
		Vector2 checkvector2 = massdealer.parseVector3XYZtoVector2XZ(checkvector3);
		return checkvector2.x >= 0 && (int)checkvector2.x < Config.maxGridNum && checkvector2.y >= 0 && (int)checkvector2.y < Config.maxGridNum;
	}
	void debugdatacheck() {//デバッグ用なので、無視して問題ない。
		bool[,] cansetdatas = datamanager.getcanSetDatas();
		for (int j = 0; j < cansetdatas.GetLength(1); j++) {
			for (int i = 0; i < cansetdatas.GetLength(0); i++) {
				Debug.Log(String.Format("{0}{1}{2}",i, j, cansetdatas[i, j]));
			}
		}
	}
}