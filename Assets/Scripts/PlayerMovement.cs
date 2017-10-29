using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region Variables
    // FIELDS //
    [SerializeField]
    GameObject BlinkParticles;
    Rigidbody2D MyRb;
    Vector2 MousePosition;
    Touch MyTouch;

    float MovementSpeed = 1f;
    // PUBLIC PROPERTIES //


    // PRIVATE PROPERTIES //

    #endregion

    #region Unity Methods
    void Start ()
    {
		
	}
        
    
    void Update()
    {
#if UNITY_EDITOR

            if (Input.GetMouseButton(0))
            {
                Movement();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Blink();
            }

#elif UNITY_ANDROID

            MyTouch = Input.GetTouch(0);
            if (MyTouch.phase != TouchPhase.Canceled || MyTouch.phase != TouchPhase.Ended)
            {
                Movement();
            }
            if (MyTouch.tapCount >= 1)
            {
                Blink();
            }

#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<IObstacle>() is IObstacle)
        {
            DestroyPlayer();
        }
    }
#endregion

    #region Public Methods
    // PUBLIC METHODS //
    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    #endregion

#region Private Methods
    // PRIVATE METHODS //
    void Movement()
    {
#if UNITY_EDITOR
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
        MousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        if (MousePosition.x > -130 && MousePosition.x < -80)
        {
            transform.position = Vector3.MoveTowards(transform.position, MousePosition, MovementSpeed);
        }
    }


    void Blink()
    {
#if UNITY_EDITOR
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
        MousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        Instantiate(BlinkParticles, transform.position, Quaternion.identity);

        if (MousePosition.x > -130 && MousePosition.x < -80)
        {
            transform.position = new Vector3(MousePosition.x, MousePosition.y, transform.position.z);
        }
    }
#endregion
}
