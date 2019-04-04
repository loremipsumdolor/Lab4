using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private static List<Vector2> directionList = new List<Vector2>(){Vector2.up, Vector2.down, Vector2.left, Vector2.right};
    private Vector2 direction;
    private bool foodEaten = false;
    public GameObject tailPrefab;
    private List<GameObject> tail = new List<GameObject>();
    public Transform snakeCharacter;
    private bool collisionDetected = false;

    private void Start() {
        direction = directionList[Random.Range(0, directionList.Count)];
        InvokeRepeating("Move", 0, 0.15f);
    }

    private void Update() {
        if (Input.GetAxis("Vertical") > 0)
        {
            direction = Vector2.up;
        } else if (Input.GetAxis("Vertical") < 0)
        {
            direction = Vector2.down;
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            direction = Vector2.left;
        } else if (Input.GetAxis("Horizontal") > 0)
        {
            direction = Vector2.right;
        }
    }

    private void Move() {
        Vector2 currentPos = transform.position;
        transform.Translate(direction);
        if (foodEaten || tail.Count < 5) {
            GameObject newTail = Instantiate(tailPrefab, currentPos, Quaternion.identity, snakeCharacter);
            tail.Insert(0, newTail);
            foodEaten = false;
        } else if (tail.Count > 0) {
            tail[tail.Count - 1].transform.position = currentPos;
            tail.Insert(0, tail[tail.Count - 1]);
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Food"))
        {
            foodEaten = true;
            Destroy(collision.gameObject);
        } else {
            if(!collisionDetected)
            {
                CancelInvoke();
                collisionDetected = true;
                Blink();
            }
        }
    }

    private void Blink() {
        snakeCharacter.gameObject.SetActive(!snakeCharacter.gameObject.activeSelf);
        Invoke("Blink", 1f);
    }
}