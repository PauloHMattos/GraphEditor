using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tail : GridMovement
{
    public GameObject tailPrefab;
    public GameObject foodPrefab;
    public List<Transform> tail;
    private bool ate;

    protected override void Start()
    {
        base.Start();
        tail = new List<Transform>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith(foodPrefab.name))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        //else
        {
            // ToDo 'You lose' screen
        }
    }

    protected override void Move()
    {
        var v = transform.position;
        base.Move();
        
        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
}
