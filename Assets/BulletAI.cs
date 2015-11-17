using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

    public AudioSource m_Explosion;

    //private bool m_ToBeDestroied = false;

    private string m_TargetTag;
    private Transform m_Transform;
    private float m_Speed = 100f;
    private float m_Lifetime = 10f;
    private float m_Borntime;
	// Use this for initialization
	void Start () {
        //m_Speed = 1f;
        m_Borntime = Time.time;
        m_Transform = gameObject.transform;
        
	}
	
	// Update is called once per frame
	void Update () {

        m_Transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
        if (Time.time - m_Borntime > m_Lifetime)
            GameObject.Destroy(gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (other.gameObject.transform.root.gameObject.tag == m_TargetTag)
        {
            if (m_TargetTag == "Monster")
            {
                Destroy(other.gameObject.transform.root.gameObject);

                m_Explosion.Play();
            }
            else if (m_TargetTag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AirCraftControl>().Damaged();
            }
            //Destroy(gameObject);
        }
    }

    public void SetTarget(string tag, int type)
    {
        m_TargetTag = tag;

        Renderer rend = gameObject.GetComponent<Renderer>();
        if (tag == "Player")
        {
            if (type > 0)
            {
                rend.material.color = Color.red;
                m_Speed = 100f;
            }
            else
            {
                rend.material.color = Color.blue;
                m_Speed = 50f;
            }
        }
        if (tag == "Monster")
        {
            rend.material.color = Color.green;
            m_Speed = 100f;
        }

    }
}
