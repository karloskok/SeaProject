using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {

#region PlayerStats
public string playerName = "";
	public List<Ship> listOfShips;
	public int gold;
	public int wood;
	public int pearl;
	public int rum;
	public int level = 1;
	public int experience = 1;
#endregion

	

	public int currentShipSelected=0;
	public GameObject currentShip;

	public void Save_inventory()
    {
        PlayerStats playerStats = new PlayerStats();
        playerStats.listOfShips = listOfShips;
		playerStats.gold = gold;
		playerStats.pearls = pearl;
		playerStats.wood = wood;
		playerStats.rum = rum;
		playerStats.level = level;
		playerStats.experience = experience;
        
        IO.Save<PlayerStats>(playerStats, "Player_stats");
        Debug.Log("stats was saved");
    }
    public void Load_inventory()
    {
        if (IO.File_exist("Player_stats"))
        {
            PlayerStats id = IO.Load<PlayerStats>("Player_stats");
            listOfShips = id.listOfShips;
			gold = id.gold;
			pearl = id.pearls;
			wood = id.wood;
			rum = id.rum;
			experience = id.experience;
			level = id.level;

            Debug.Log("stats was loaded");
        }
        else
            Debug.Log("stats wasn't loaded, because there is no file");
    }

    public void Add_to_shipList(Ship ship)
    {
		listOfShips.Add(ship);
    }
	public void Remove_from_shipList(Ship ship)
	{
		listOfShips.Remove(ship);
	}

	public float levelExpMin, levelExpMax;
	void CheckPlayerLevel(){
		ClaculateLevelExpRange();
		if(experience >= levelExpMax){
			if(level < 12)
				level++;
		}
			
		ClaculateLevelExpRange();

	} 
	public void AddExp(int value){
		experience += value;
		CheckPlayerLevel();

	}
	
	void ClaculateLevelExpRange(){
		levelExpMin = Mathf.Pow(2, level-1) * 100 * (level-1);
		levelExpMax = Mathf.Pow(2, level) * 100 * (level);
		

	}

	// Use this for initialization
	void Start () 
	{
		//Debug.Log( Application.persistentDataPath);  //-> C:/Users/MS Studio/AppData/LocalLow/DefaultCompany/SeaProject
		//Add_to_shipList(new Ship("Brig royal", 1, 5, 10, 420, 10, 6));
		//Add_to_shipList(new Ship("Cutter Royal", 1, 7, 12, 380, 10, 4));

		//load
		Load_inventory();
		foreach (var item in listOfShips)
		{
			Debug.Log("Ship: " + item.shipName + "\n");	
		}

		selectShip(0);
		ClaculateLevelExpRange();
	}


	public void selectShip(int i)
	{
//		Debug.Log("Selected ships: " + i);
		if(currentShip != null)
		{
			GameObject g = currentShip;
			currentShip = null;

			GameObject.DestroyImmediate(g, false);
		}

		GameObject loadObject = Resources.Load(listOfShips[i].pathTo) as GameObject;
		currentShip = Instantiate(loadObject, Vector3.zero, Quaternion.identity);
		Debug.Log(listOfShips[i].shipName);


		currentShipSelected = i;

		
		
	} 
	

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.F5))
        {
            Save_inventory();

        }

		if (Input.GetKeyDown(KeyCode.K))
        {
            selectShip((currentShipSelected == 1) ? 0 : 1);

        }

		


	}
}

