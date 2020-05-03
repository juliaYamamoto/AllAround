using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    private float horizontalInput;
    private float verticalInput;
    public float speed;

    //Game Controller
    public GameController gameController;

    private void Update()
    {
        //Movement
        transform.Translate(new Vector3(horizontalInput, verticalInput));
    }
    
    private void FixedUpdate()
    {
        //Movement
        horizontalInput = (Input.GetAxis("Horizontal") * speed) * Time.fixedDeltaTime;
        verticalInput = (Input.GetAxis("Vertical") * speed) * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            gameController.PlayerGotFruit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameController.PlayerDied();
            gameObject.SetActive(false);
        }
    }
}
