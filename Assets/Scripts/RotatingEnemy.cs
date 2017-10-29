using UnityEngine;

public class RotatingEnemy : MonoBehaviour, IObstacle {

    #region Variables
    // FIELDS //
    [SerializeField]
    float MovementSpeed = 1f;
    [SerializeField]
    float RotationSpeed = 1f;
    [SerializeField]
    Vector3 RotationVector = new Vector3(0f,0f,-1f);

	// PUBLIC PROPERTIES //


	// PRIVATE PROPERTIES //
	
	#endregion

	#region Unity Methods
	void Start () {
		
	}
	
	void FixedUpdate () {
        transform.Rotate(RotationVector * RotationSpeed);
        transform.position += Vector3.left/10 * MovementSpeed;
	}
	#endregion

	#region Public Methods
	// PUBLIC METHODS //

	#endregion

	#region Private Methods
	// PRIVATE METHODS //

	#endregion
}
