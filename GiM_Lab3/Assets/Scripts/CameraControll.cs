using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraControll : MonoBehaviour {
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
 
    private Rigidbody _selfRigidbody;

    private float _x = 0.0f;
    private float _y = 0.0f;

    // Use this for initialization
    void Start()
    {
        var angles = transform.eulerAngles;
        _x = angles.y;
        _y = angles.x;

        _selfRigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (_selfRigidbody != null)
        {
            _selfRigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            _x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            _y = ClampAngle(_y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(_y, _x, 0);
            var negDistance = new Vector3(0.0f, 0.0f, -distance);
            var position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
