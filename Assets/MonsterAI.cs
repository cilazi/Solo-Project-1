using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour {
    public Transform m_Bullet;

    private float lastFire;
    private float fireInterval = 2f;
    private Transform m_Trans;

    private int Type = 0;
    //0:follow
    //1:non follow


    // Use this for initialization
    void Start() {
        lastFire = Time.time;
        m_Trans = gameObject.transform;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - lastFire > fireInterval)
        {
            lastFire = Time.time;
            Fire();
        }
    }

    private void Move()
    {
        gameObject.transform.Translate(new Vector3(0,0,5));
    }

    private void Fire()
    {
        Move();
        if (Type>0)
            m_Trans.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        Transform bullet = Instantiate(m_Bullet, m_Trans.position, m_Trans.rotation) as Transform;
        bullet.gameObject.GetComponent<BulletAI>().SetTarget("Player", Type);
    }

    public void ChangeFire(float fire)
    {
        fireInterval = fire;
    }

    public void ChangeType(int type)
    {
        //Debug.Log(type);
        Type = type;
    }
}
