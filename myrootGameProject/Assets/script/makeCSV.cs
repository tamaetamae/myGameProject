﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class makeCSV//CSVデータ作成クラス。x,y,kindの列データを座標の数だけ作成
{
	public int MaxtileCount = LevelDesignCreate.maxColumn;
	StreamWriter sw;

	public void logSave(string aDatapath,GameObject[,] writtenData){//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		sw = fi.AppendText();
		writeLogData(writtenData);
		sw.Flush();
		sw.Close();
	}
	void writeLogData(GameObject[,] writtenData){//実際にログデータを書く部分、流れとしてはオブジェクトのデータを取得し、それを書いていくだけなので、int[,]がもらえればいいだけの話。
		for (int j = 0; j < MaxtileCount; j++)
		{
			for (int i = 0; i < MaxtileCount; i++)
			{
				sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[i,j].GetComponent<LevelButton>().returnThisState());
				//sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[j * 10 + i].GetComponent<LevelButton>().returnThisState());
			}
		}
	}
}
