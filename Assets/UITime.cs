using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITime : MonoBehaviour {

    public float MaxTime = 120;
    public float TimetoWin;
    private float lastUpdate;
    private Text timeShown;
    public Transform monsterControl;
    private MonsterManager m_MonsterManager;

    private bool gameWin = false;

	// Use this for initialization
	void Start () {
        m_MonsterManager = monsterControl.GetComponent<MonsterManager>();
        timeShown = gameObject.GetComponent<Text>();
        TimetoWin = MaxTime;
        lastUpdate = Time.time;
        timeShown.text = System.Convert.ToString(TimetoWin);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameWin)
            return;

        if (TimetoWin == 0)
        {
            timeShown.text = "Congrats!";
            m_MonsterManager.Win();
            gameWin = true;
        }

        if (Time.time - lastUpdate > 1)
        {
            lastUpdate = Time.time;
            TimetoWin--;
            timeShown.text = System.Convert.ToString(TimetoWin);
        }
	}
}
