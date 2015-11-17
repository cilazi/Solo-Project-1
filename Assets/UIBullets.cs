using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBullets : MonoBehaviour {

    public Transform m_Player;
    private AirCraftControl m_Aircraft;
    private Text textShown;

	// Use this for initialization
	void Start () {
        textShown = gameObject.GetComponent<Text>();
        m_Aircraft = m_Player.gameObject.GetComponent<AirCraftControl>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Aircraft.currentBullets > 0)
        {
            textShown.text = System.Convert.ToString(m_Aircraft.currentBullets);
        }
        else
        {
            textShown.text = "Reloading..";
        }
	}
}
