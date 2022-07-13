using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float speed;

	private Vector2 startPos;
	private Camera camera;

	private float targetPos;


    void Start()
    {
		camera = GetComponent<Camera>();
		targetPos = transform.position.x;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) startPos = camera.ScreenToWorldPoint(Input.mousePosition);
		else if(Input.GetMouseButton(0))
		{
			float pos = camera.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
			targetPos = Mathf.Clamp(transform.position.x - pos, -1000f, 1000f);
		}
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos, speed * Time.deltaTime), transform.position.y, transform.position.z);

    }
}
