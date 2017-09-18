using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatStasis : MonoBehaviour {

    public float t = .2f;
    public float tiempo;
    Vector2 velocity = new Vector2(0.0f, 0.0f);
    public Transform dir;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.velocity = (rb.velocity/4) * 3;
            //velocity = rb.velocity;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            // other.transform.position = Vector2.SmoothDamp(other.gameObject.transform.position, dir.gameObject.transform.position, ref velocity, t, 1000, Time.fixedDeltaTime);
            velocity = rb.velocity;
            Vector2.SmoothDamp(other.gameObject.transform.position, dir.gameObject.transform.position, ref velocity, t, 1000, Time.fixedDeltaTime);
            rb.velocity = velocity;
        }
    }
}
