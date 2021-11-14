using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static string lastScene;
    public static bool moveInMarket;
    public static bool isGamePaused = false;
    public PauseMenu pauseMenu;

    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public LayerMask exit;

    public int facing = 1; //Player Facing Direction (UP, RIGHT, DOWN, LEFT)
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        facing = 1; //Set to facing right by default
        spriteRenderer.sprite = sprites[facing];
        movePoint.parent = null;

        if (moveInMarket)
        {
            if (lastScene == "Pond")
            {
                transform.position = new Vector3(0, 4, 0);
                movePoint.position = new Vector3(0, 4, 0);
                facing = 2;
            }
            if (lastScene == "Lake")
            {
                transform.position = new Vector3(0, -3, 0);
                movePoint.position = new Vector3(0, -3, 0);
                facing = 0;
            }
            if (lastScene == "River")
            {
                transform.position = new Vector3(-9, 0, 0);
                movePoint.position = new Vector3(-9, 0, 0);
                facing = 1;
            }
            if (lastScene == "Ocean")
            {
                transform.position = new Vector3(9, 0, 0);
                movePoint.position = new Vector3(9, 0, 0);
                facing = 3;
            }
            moveInMarket = false; 
            spriteRenderer.sprite = sprites[facing];
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePaused = false;
                pauseMenu.hideMenu();
            }
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePaused = true;
                pauseMenu.showMenu();
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(Input.GetAxisRaw("Horizontal") > 0)
                {
                    facing = 1; //RIGHT
                    spriteRenderer.sprite = sprites[facing];
                } else
                {
                    facing = 3; //LEFT
                    spriteRenderer.sprite = sprites[facing];
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    facing = 0; //UP
                    spriteRenderer.sprite = sprites[facing];
                }
                else
                {
                    facing = 2; //DOWN
                    spriteRenderer.sprite = sprites[facing];
                }
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene("Ocean");
            lastScene = "Ocean";
        }
        else if (collision.gameObject.tag == "Market")
        {
            moveInMarket = true;
            SceneManager.LoadScene("Market");
        }
        else if (collision.gameObject.tag == "Lake")
        {
            SceneManager.LoadScene("Lake");
            lastScene = "Lake";
        }
        else if (collision.gameObject.tag == "River")
        {
            SceneManager.LoadScene("River");
            lastScene = "River";
        }
        else if (collision.gameObject.tag == "Pond")
        {
            SceneManager.LoadScene("Pond");
            lastScene = "Pond";
        }
    }
}
