using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController gameController;
    public Rigidbody2D rb;
    bool isPlay;
    public Vector2 velocity;
    int getTouch = 36;
    private Vector2 startingPoint;
    public Vector3 pastPlayer;
    public GameObject bgSta;
    public Camera _cam;
    public float speed;
    public bool isFinish;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        _cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        speed = gameController.speed;
        isPlay = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
            rb.velocity = velocity;
        else
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Touch t = Input.GetTouch(i);
                Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
                if (t.phase == TouchPhase.Began)
                {
                    if (t.position.y < Screen.height * 3 / 4)
                    {
                        getTouch = t.fingerId;
                        startingPoint = touchPos;
                        pastPlayer = transform.position;
                    }    
                }
                else if (t.phase == TouchPhase.Moved && getTouch == t.fingerId)
                {
                    Vector2 change = transform.position - pastPlayer;
                    pastPlayer = transform.position;
                    startingPoint += change;
                    Vector2 offset = touchPos - startingPoint;
                    Vector2 direction = Vector2.ClampMagnitude(offset, 0.7f);
                    velocity = direction;
                    transform.position = new Vector3(bgSta.transform.position.x + direction.x, bgSta.transform.position.y + direction.y, transform.position.z);
                }
                else if (t.phase == TouchPhase.Ended && getTouch == t.fingerId)
                {
                    float len = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
                    if (len != 0)
                    {
                        velocity.x = speed * ( - velocity.x / len);
                        velocity.y = speed * ( - velocity.y / len);
                    }
                    isPlay = true;
                    getTouch = 36;
                }
                ++i;
            }
        }
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return _cam.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
    public void resetPlay()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector3(bgSta.transform.position.x, bgSta.transform.position.y, transform.position.z);
        isPlay = false;
        isFinish = false;
        velocity = new Vector2(0, 0);
    }    
    public void setVelocity(Vector2 n)
    {
        gameController.checkPoint();
        rb.velocity = new Vector2(0, 0);
        float len = n.x * n.x + n.y * n.y;
        float x = (n.x * n.x * velocity.x + n.x * n.y * velocity.y) / len;
        float y = (n.y * n.y * velocity.y + n.x * n.y * velocity.x) / len;
        velocity = -new Vector2(2 * x - velocity.x, 2 * y - velocity.y);
        Debug.Log(velocity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFinish && collision.gameObject.CompareTag("Finish"))
        {
            rb.velocity = new Vector2(0, 0);
            velocity = new(0, 0);
            isFinish = true;
            if (gameController.count == gameController.countLevel[gameController.idxLv])
            {
                gameController._win.SetActive(true);
            }    
            else
            {
                gameController._lose.SetActive(true);
            }    
        }
    }
}