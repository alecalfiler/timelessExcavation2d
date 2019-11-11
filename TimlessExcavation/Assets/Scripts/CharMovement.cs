using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    private bool facingRight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Float();
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        //DESTROY BLOCKS
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindObjectOfType<MapDestruction>().DestroyUnder(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindObjectOfType<MapDestruction>().DestroyLeft(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindObjectOfType<MapDestruction>().DestroyRight(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindObjectOfType<MapDestruction>().DestroyAbove(transform.position);
        }
    }

    void Float()
    {
        if (Input.GetButtonDown("Jump"))
        {
            
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }

    private void Drill()
    {

    }



}
