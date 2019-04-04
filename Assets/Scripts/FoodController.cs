using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform topBorder;
    public Transform bottomBorder;
    public Transform leftBorder;
    public Transform rightBorder;
    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 0.1f);
    }

    public void Spawn()
    {
        if (GameObject.Find("FoodPrefab(Clone)") == null)
        {
            Instantiate(foodPrefab, new Vector2(Random.Range(leftBorder.position.x, rightBorder.position.x), Random.Range(bottomBorder.position.y, topBorder.position.y)), Quaternion.identity);
        }
    }
}
