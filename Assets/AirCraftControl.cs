using UnityEngine;
using System.Collections;

public class AirCraftControl : MonoBehaviour {
    public int maxBullets = 10;
    public int currentBullets;
    private float reloadInterval = 4f;
    private float reloadStart = 0f;
    bool startreload = false;

    private float fireInterval = 0.5f;
    private float lastFired=0;

    private int HP = 10;
    private GameObject m_Object;
    private Transform m_PivotTrans;
    private Vector3 m_Pos;
    private Transform m_Trans;
    private Transform m_CameraTrans;
    private Transform m_BodyTrans;
    private float m_Speed;
    private float m_RotateSpeed;
    private float yAngle = 0f;
    private float xAngle = 0f;
    private float xAngleMax = 45f;
    private float xAngleMin = -45f;
    private float yPivotAngle = 0f;
    private float xPivotAngle = 0f;
    private float xPivotAngleMax = 90f;
    private float xPivotAngleMin = -90f; 
    private bool pivotRested = false;
    private AudioSource m_EngineAudio;
    public Terrain m_Terrain;
    public Transform m_Bullet;
    // Use this for initialization
    void Start () {
        //GameObject.FindGameObjectWithTag("Respawn").transform.Translate(new Vector3(0,200,0));
        currentBullets = maxBullets;
        m_Object = gameObject;
        m_Trans = m_Object.GetComponent<Transform>();
        m_PivotTrans = GameObject.FindGameObjectWithTag("CameraPivot").transform;
        m_CameraTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
        m_BodyTrans = GameObject.FindGameObjectWithTag("AirCraftBody").transform;
        //m_Pos = m_Trans.position;
        m_Speed = 10f;
        m_RotateSpeed = 300f;
        foreach (var d in m_Object.GetComponents<AudioSource>())
        {
            if (d.clip.name == "JetEngine")
            { 
                m_EngineAudio = d;
                m_EngineAudio.pitch = 0.6f;
                m_EngineAudio.Stop();
                //Debug.Log("found!");
            }
        }
        //Debug.Log("not found!");

    }

    // Update is called once per frame

    void Abandoned()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    if (!pivotRested)
        //    {
        //        pivotRested = true;
        //        yPivotAngle = 0;
        //        xPivotAngle = 0;
        //        m_PivotTrans.rotation = m_Trans.rotation;
        //        //Debug.Log(m_PivotTrans.rotation);
        //    }
        //    yAngle += Input.GetAxis("Mouse X") * m_RotateSpeed * Time.deltaTime;
        //    xAngle -= Input.GetAxis("Mouse Y") * m_RotateSpeed * Time.deltaTime;
        //    if (xAngle > xAngleMax)
        //        xAngle = xAngleMax;
        //    else if (xAngle < xAngleMin)
        //        xAngle = xAngleMin;
        //    //m_Trans.Rotate(new Vector3(0, yAngle, 0));
        //    //m_Trans.Rotate(new Vector3(xAngle, 0, 0));
        //    m_Trans.rotation = Quaternion.Euler(new Vector3(xAngle, yAngle, 0));
        //    m_PivotTrans.rotation = Quaternion.Euler(new Vector3(xAngle, yAngle, 0));
        //    //m_BodyTrans.rotation = Quaternion.Euler(new Vector3(xAngle, yAngle, 0));
        //    yPivotAngle = yAngle;
        //    xPivotAngle = xAngle;
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    pivotRested = false;
        //    yPivotAngle += Input.GetAxis("Mouse X") * m_RotateSpeed * Time.deltaTime;
        //    xPivotAngle -= Input.GetAxis("Mouse Y") * m_RotateSpeed * Time.deltaTime;
        //    if (xPivotAngle > xPivotAngleMax)
        //        xPivotAngle = xPivotAngleMax;
        //    else if (xPivotAngle < xPivotAngleMin)
        //        xPivotAngle = xPivotAngleMin;
        //    //m_Trans.Rotate(new Vector3(0, mousX, 0));
        //    //m_Tans.Rotate(new Vector3(-mousY, 0, 0));
        //    m_PivotTrans.rotation = Quaternion.Euler(new Vector3(xPivotAngle, yPivotAngle, 0));
        //    //Debug.Log(m_PivotTrans.rotation);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    if(m_CameraTrans.transform.position.z>-15)
        //        m_CameraTrans.Translate(Vector3.back * Time.deltaTime * m_Speed * 0.1f);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    if (m_CameraTrans.transform.position.z < -8)
        //        m_CameraTrans.Translate(Vector3.forward * Time.deltaTime * m_Speed * 0.1f);
        //}
    }

    void CheckBullets()
    {
        if (currentBullets > 0)
            return;
        if (!startreload)
        {
            if(maxBullets>2)
                maxBullets -= 2;
            startreload = true;
            reloadStart = Time.time;
        }
        else
        {
            if (Time.time - reloadStart > reloadInterval)
            {
                startreload = false;
                currentBullets = maxBullets;
            }
        }

    }

    void Update () {
        CheckBullets();
        //if (m_Object.transform.position.y < 0)
        //    m_Object.transform.position = new Vector3(m_Object.transform.position.x, 0, m_Object.transform.position.z);
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (v != 0 || h != 0)
        {
            //if (!m_EngineAudio.isPlaying)
            //    m_EngineAudio.Play();
            m_EngineAudio.pitch = 0.6f;
            m_Trans.Translate(new Vector3(h, v, 0).normalized * Time.deltaTime * m_Speed);
        }
        //else if(m_EngineAudio.isPlaying)
        //    m_EngineAudio.Stop();

        
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentBullets == 0)
                return;
            //m_Trans.Translate(m_Trans.up * Time.deltaTime * m_Speed * 0.3f);
            if(Time.time - lastFired>fireInterval)
            {
                lastFired = Time.time; 
                Vector3 position = m_Trans.position;
                position.y++;
                Transform bullet = Instantiate(m_Bullet, position, m_Trans.rotation) as Transform;
                bullet.gameObject.GetComponent<BulletAI>().SetTarget("Monster",0);
                currentBullets--;
            }

        }
    }

    public void Damaged()
    {
        HP--;
        GameObject.Find("HP").GetComponent<UIHPControl>().HPChange(HP);
        if (HP == 0)
        {
            GameObject.FindGameObjectWithTag("Respawn").transform.Translate(new Vector3(0, -600, 0));
            GameObject.Destroy(gameObject);

        }
    }
}
