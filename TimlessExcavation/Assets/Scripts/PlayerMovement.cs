using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 7f;

    public Rigidbody2D rb;
    public Animator animator;
    private bool engineOn;
   
    Vector2 movement;

    [SerializeField]
    private Text fuelMeter;
    public static int fuel;

    [SerializeField]
    private Text goldTracker;
    public static int goldCount;

    [SerializeField]
    private Text diamondTracker;
    public static int diamondCount;

    [SerializeField]
    private Text rubyTracker;
    public static int rubyCount;

    [SerializeField]
    private Text emeraldTracker;
    public static int emeraldCount;

    [SerializeField]
    private Text draconicTracker;
    public static int draconicCount;

    [SerializeField]
    private Text lightTracker;
    public static int lightCount;
    


    private void Start()
    {
        Time.timeScale = 1f;
        engineOn = false;
        rb = GetComponent<Rigidbody2D>();

        fuel = 100;

        goldCount = 0;
        diamondCount = 0;
        rubyCount = 0;
        emeraldCount = 0;
        draconicCount = 0;
        lightCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //x movement
        movement.y = Input.GetAxisRaw("Vertical"); //y movement


        animator.SetFloat("Horizontal", movement.x); //change animation for x movement
        animator.SetFloat("Vertical", movement.y); //change for y
        animator.SetFloat("Speed", movement.sqrMagnitude);

        goldTracker.text = "Gold: " + goldCount.ToString() + "/10";
        diamondTracker.text = "Diamond: " + diamondCount.ToString() + "/5";
        rubyTracker.text = "Ruby: " + rubyCount.ToString() + "/4";
        emeraldTracker.text = "Emerald: " + emeraldCount.ToString() + "/3";
        draconicTracker.text = "Draconic: " + draconicCount.ToString() + "/2";
        lightTracker.text = "Light: " + lightCount.ToString() + "/1";

        fuelMeter.text = "Fuel: " + fuel.ToString() + "%";

        // IF ARROW KEY IS PRESSED IN DIRECTION, DESTROY THE BLOCK DRILLING INTO
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<MapDestruction>().DestroyUnder(transform.position);
        }
        // IF ANY ARROW KEYS ARE PRESSED, ENGINE IS ON, AND WILL PROC THE FUEL TO BEGIN DRAINING
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) && fuel > 0)
        {
            engineOn = true;
            StartCoroutine(DrainFuel());
        }
        // TEST IF FUEL EXCEEDS 100, IF SO SET TO 100 TO MAX IT OUT
        if (fuel > 100)
        {
            fuel = 100;
        }
        // PREVENTS NEGATIVE NUMBERED FUEL
        if (fuel <= 0)
        {
            fuel = 0;
        }
        // IF FUEL RUNS OUT.. START OVER
        if (fuel == 0)
        {
            engineOn = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        // WIN CONDITION: IF ALL GEM COLLECTING OBJECTIVES ARE COMPLETE, THEN WIN!
        if (goldCount == 10 && diamondCount == 5 && rubyCount == 4 && emeraldCount == 3 && draconicCount == 2 && lightCount == 1)
        {
            FindObjectOfType<GameManager>().winGame();
        }
        //IF ARROW KEYS ARE RELEASED, ENGINE IS NOW OFF, THUS NO FUEL SHOULD BE DRAINED
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            engineOn = false;

        }

        if (Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<MapDestruction>().DestroyOn(transform.position);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<MapDestruction>().DestroyLeft(transform.position);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<MapDestruction>().DestroyRight(transform.position);
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<MapDestruction>().DestroyAbove(transform.position);
        }
        // END OF DESTROYER

      
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        //GOLD
        if (other.gameObject.CompareTag("Gold"))
        {
            if (goldCount < 10)
            {
                goldCount += 1;
                Destroy(other.gameObject);
            }
        }
        //DIAMOND
        if (other.gameObject.CompareTag("Diamond"))
        {
            if (diamondCount < 5)
            {
                diamondCount += 1;
                Destroy(other.gameObject);
            }
        }
        //RUBY
        if (other.gameObject.CompareTag("Ruby"))
        {
            if (rubyCount < 4)
            {
                rubyCount += 1;
                Destroy(other.gameObject);
            }
        }
        //EMERALD
        if (other.gameObject.CompareTag("Emerald"))
        {
            if (emeraldCount < 3)
            {
                emeraldCount += 1;
                Destroy(other.gameObject);
            }
        }
        //DRACONIC
        if (other.gameObject.CompareTag("Draconic"))
        {
            if (draconicCount < 2)
            {
                draconicCount += 1;
                Destroy(other.gameObject);
            }
        }
        //SHADOW
        if (other.gameObject.CompareTag("Light"))
        {
            if (lightCount < 1)
            {
                lightCount += 1;
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Coal"))
        {
            if (fuel < 100)
            {
                fuel += 35;

                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator DrainFuel()
    {
        for (int i = fuel; i >=1; i--)
        {
            fuel -= 1;
            yield return new WaitForSeconds(0.1f);

            if (engineOn == false)
                break;
            
        } 
    }
}
