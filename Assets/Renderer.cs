using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer : MonoBehaviour
{
    public Transform xy1; //topLeft
    public Transform xy2; //bottomRight
    public GameObject pixel;

    float width;
    float height;
    int rays;

    List<GameObject> pixelsList = new List<GameObject>();
    Color[] rayColors;
    float[] rayDistance;

    GameObject previousCollider;

    public void Initialize(int _rays)
    {
        width = xy2.position.x - xy1.position.x;
        height = xy1.position.y - xy2.position.y;

        rays = _rays;

        float spacing = width / (rays + 1);

        if(pixelsList.Count > 0)
        {
            for(int i = 0; i < pixelsList.Count; i++)
            {
                Destroy(pixelsList[i]);
            }
            pixelsList.Clear();
        }
        for(int i = 0; i < rays; i++)
        {
            float pos = xy2.position.x - spacing * (i + 1);
            GameObject _obj = Instantiate(pixel, new Vector2(pos, 0), Quaternion.Euler(0, 0, 0));
            _obj.GetComponent<SpriteRenderer>().sortingOrder = 10;
            _obj.transform.localScale = new Vector2(spacing, height);
            pixelsList.Add(_obj);
        }
    }
    public void UpdateFrame(Color[] _col, float[] _flo, Vector2[] _normals, Vector2 playerDirection, GameObject[] rayHitList) 
    {
        rayColors = _col;
        rayDistance = _flo;

        for(int i = 0; i < pixelsList.Count; i++)
        {
            pixelsList[i].GetComponent<SpriteRenderer>().color = ColorAugmentor(rayColors[i], rayDistance[i], rayHitList[i], i);
            pixelsList[i].transform.localScale = new Vector2(pixelsList[i].transform.localScale.x, height / Mathf.Sqrt(rayDistance[i]));
        }
    }
    Color ColorAugmentor(Color _color, float _distance, GameObject _collider, int _index)
    {
        if(previousCollider != _collider)
        {
            previousCollider = _collider;
            rayDistance[_index] = rayDistance[_index - 1];
            return Color.cyan;
        }
        previousCollider = _collider;
        return Color.Lerp(_color, Color.black, Mathf.Sqrt(_distance) / 2f - 1);
    }
}
