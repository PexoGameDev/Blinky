using UnityEngine;

public class Autodestruct : MonoBehaviour {

    #region Variables
    // FIELDS //
    [SerializeField]
    float TimeOfLife = 1f;
	#endregion

	#region Unity Methods
	void Start () {
        Destroy(gameObject, TimeOfLife);
	}
	#endregion
}
