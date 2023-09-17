using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public Renderer rend;
    public GameObject rotato;
    public Rigidbody2D rb;
    public LayerMask wallLayer;
    public Color sky;
    public float fov;
    public int rays;
    public float drawDistance;
    public bool updateRays;

    List<GameObject> rayList = new List<GameObject>();
    Color[] rayColors;
    float[] rayDistance;
    Vector2[] rayNormals;
    GameObject[] rayHitList;
    void Awake()
    {
        Initialize();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, (transform.rotation.eulerAngles + new Vector3(0, 0, -fov / 2)).normalized * 100);
        Debug.DrawRay(transform.position, (transform.rotation.eulerAngles + new Vector3(0, 0, fov / 2)).normalized * 100);

        if (updateRays)
        {
            updateRays = false;
            Initialize();
        }
        FireRays();
    }
    void Initialize()
    {
        if(rayList.Count > 0)
        {
            for(int i = 0; i < rayList.Count; i++)
            {
                Destroy(rayList[i]);
            }
            rayList.Clear();
        }
        for(int i = 0; i < rays; i++)
        {
            float rot = fov / rays;
            GameObject _obj = Instantiate(rotato, rb.position, Quaternion.Euler(0, 0, rb.rotation + (-fov / 2) + rot * (i + 0.5f)), transform);
            rayList.Add(_obj);
        }
        rayColors = new Color[rayList.Count];
        rayDistance = new float[rayList.Count];
        rayNormals = new Vector2[rayList.Count];
        rayHitList = new GameObject[rayList.Count];
        Camera.main.backgroundColor = sky;
        rend.Initialize(rays);
    }
    void FireRays()
    {
        RaycastHit2D hit;
        for(int i = 0; i < rayList.Count; i++)
        {
            hit = Physics2D.Raycast(transform.position, rayList[i].transform.right, Mathf.Infinity, wallLayer);

            if(hit.collider != null)
            {
                rayColors[i] = hit.collider.GetComponent<SpriteRenderer>().color;
                rayDistance[i] = hit.distance + 1;
                rayNormals[i] = hit.normal;
                rayHitList[i] = hit.collider.gameObject;
            }
            else
            {
                rayColors[i] = sky;
                rayDistance[i] = 1;
                rayNormals[i] = Vector2.zero;
                rayHitList[i] = null;
            }
        }
        rend.UpdateFrame(rayColors, rayDistance, rayNormals, transform.right, rayHitList);
    }
}
