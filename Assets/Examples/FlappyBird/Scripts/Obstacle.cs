using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    public float velocity = -3;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
    }
}