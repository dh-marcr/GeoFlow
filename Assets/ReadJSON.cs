using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ReadJSON : MonoBehaviour {

	public void addToFile(){

		TileInfo newTile = new TileInfo ();

		newTile.ID = int.Parse(id.text);
		newTile.position = new Vector3 (float.Parse(position[0].text), float.Parse(position[1].text), float.Parse(position[2].text));
		newTile.maxWeight = float.Parse(maxWeight.text);
		newTile.currentWeight = float.Parse(currentWeight.text);

		myObject.tileInfo.Add (newTile);
	}

	public void createJSON(){

		myNewUtil = JsonUtility.ToJson (myObject, true);
		writeJSON ("my new level", myNewUtil);
	}

	public void writeJSON(string in_fileName, string in_file){

		if (!Directory.Exists(Application.persistentDataPath + "/Levels/")) {
			Directory.CreateDirectory(Application.persistentDataPath + "/Levels");
		}

		string dataPath = Application.persistentDataPath + "/Levels/" + in_fileName + ".json";

		if (File.Exists (dataPath)) {
			Debug.Log ("File exsists, replacing!");
			//File.AppendAllText (dataPath, in_file);
		} else {
			Debug.Log ("Writing new json file");

			using (FileStream stream = new FileStream (dataPath, FileMode.Create)) {
				BinaryWriter writer = new BinaryWriter (stream);
				writer.Write (in_file);
				stream.Close ();
			}
		}
	}

	public void readJSON(){
		text.text = myNewUtil;
	}

	public void findDataPath(){
		text.text = Application.persistentDataPath;
	}

	public MyObject myObject;

	public Text text;
	public string myNewUtil;

	public InputField id;
	public InputField[] position = new InputField[3];
	public InputField maxWeight;
	public InputField currentWeight;
}

[System.Serializable]
public class MyObject{

	public List<TileInfo> tileInfo = new List<TileInfo> ();
}

[System.Serializable]
public class TileInfo{

	public int ID;
	public Vector3 position;
	public float maxWeight;
	public float currentWeight;
}
