using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {

    private bool gameWin = false;

    public Transform m_Monster;

    private int pointer = 0;

    private float lastSpawn;

    private float spawnInterval = 1.7f;

    private List<Vector3> vecList;

    private System.Random rd;

    private float startTime;

    private List<GameObject> m_MonsterList;

	// Use this for initialization
	void Start () {
        m_MonsterList = new List<GameObject>();
        startTime = Time.time;
        rd = new System.Random();
        lastSpawn = Time.time;
        vecList = new List<Vector3>();
        vecList.Add(new Vector3(1, 1, 75));
        vecList.Add(new Vector3(-10,1,75));
        vecList.Add(new Vector3(10, 1, 75));
        //for (int i = 0; i < vecList.Count; i++)
          //  Instantiate(m_Monster, vecList[i], Quaternion.Euler(new Vector3(0,180,0)));
            //Transform monster = Instantiate(m_Monster, new Vector3(1,1,75), Quaternion.Euler(Vector3.zero)) as Transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameWin)
            return;
        if (Time.time - startTime > 5)
        {
            if (spawnInterval > 0.5)
                ;// spawnInterval -= 0.1f;
        }
        if (Time.time - lastSpawn > spawnInterval)
        {
            lastSpawn = Time.time;
            Vector3 position = new Vector3(rd.Next(-9,9),rd.Next(-1,6),100);
            Transform monster = Instantiate(m_Monster, position, Quaternion.Euler(new Vector3(0, 180, 0))) as Transform;
            int type = rd.Next(-100,100);
            //Debug.Log(type);
            monster.gameObject.GetComponent<MonsterAI>().ChangeType(type);
            m_MonsterList.Add(monster.gameObject);

        }
	}

    public void Win()
    {
        foreach (var d in m_MonsterList)
        {
            GameObject.Destroy(d);
        }
        gameWin = true;
    }


}
