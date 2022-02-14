using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PickupController : MonoBehaviour
{
    public int points = 10;
    public int timeBonus = 10;
    private Vector3 rot = new Vector3(30, 30, 30);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // on every frame we rotate the object according to vector and slow down factor Time.deltaTime (dt between frames)
        //This is done in spin script
        //transform.Rotate(rot*Time.deltaTime);
        
    }
    // onCollision... events work when both objects have rigidbody
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
    // OnTrigger... events work when me or other does not have the rigidbody
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag.Equals("Player"))
        if (other.CompareTag("Player"))
        {
            Scores.points += points;
            Scores.timeBonus += timeBonus;
            Debug.Log("OnTriggerEnter with Player, total points: "+Scores.points);
            Destroy(gameObject);
        }
    }

    
}
