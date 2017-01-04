using UnityEngine;

public class Spinner : MonoBehaviour
{
	void Start()
	{
		SamplingDistance = UseFrustrumWidth/Camera.main.aspect * 0.5f / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_isSwiping = true;
			_start = GetWorldMousePosition();
			_lastPosition = _start;
		}

		else if (Input.GetMouseButtonUp(0))
		{
			_isSwiping = false;
		}

		else if (_isSwiping)
		{
			Vector3 newPosition = GetWorldMousePosition();
			Vector3 swipeDelta = newPosition - _lastPosition;
			_lastPosition = newPosition;
			Vector3 xRotationAxis;
			Vector3 yRotationAxis;
			if(RotateRelativeToCamera)
			{
				xRotationAxis = Camera.main.transform.up;
				yRotationAxis = Camera.main.transform.right;
			}
			else
			{
				xRotationAxis = Vector3.up;
				yRotationAxis = -Vector3.right;
			}
			#if UNITY_ANDROID
			// inversion needed here
			#else
			xRotationAxis *= -1f; // Fix for inversion
			#endif
			Vector3 xComponent = -Vector3.Project(swipeDelta, yRotationAxis);
			Vector3 yComponent = Vector3.Project(swipeDelta, xRotationAxis);
			if (swipeDelta.magnitude > 0)
			{
				transform.RotateAround(transform.position, xRotationAxis, Vector3.Dot(xComponent.normalized, yRotationAxis) * xComponent.magnitude * SwipeDampening.x);
				transform.RotateAround(transform.position, yRotationAxis, Vector3.Dot(yComponent.normalized, xRotationAxis) * yComponent.magnitude * SwipeDampening.y);
			}
		}

		if(_isSwiping && !RestrictWhileDragging)
		{
			return;
		}
		EnforceEulerLimits();
	}

	void EnforceEulerLimits()
	{
		if(_isSwiping && !RestrictWhileDragging)
		{
			return;
		}
		Vector3 eulerCorrection = new Vector3();
		Vector3 currentEuler = transform.eulerAngles;
		float timeScale = Time.deltaTime / STEPSIZE;
		eulerCorrection.x = GetEulerCorrection(currentEuler.x, MinimumEulerAngles.x, MaximumEulerAngles.x, CorrectionRate.x * timeScale);
		eulerCorrection.y = GetEulerCorrection(currentEuler.y, MinimumEulerAngles.y, MaximumEulerAngles.y, CorrectionRate.y * timeScale);
		eulerCorrection.z = GetEulerCorrection(currentEuler.z, MinimumEulerAngles.z, MaximumEulerAngles.z, CorrectionRate.z * timeScale);
		if (eulerCorrection.magnitude != 0)
		{
			transform.rotation = transform.rotation * Quaternion.Euler(eulerCorrection);
		}
	}

	Vector3 GetWorldMousePosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = SamplingDistance;
		if(Input.multiTouchEnabled)
		{
			mousePosition.x = Screen.width - mousePosition.x;
			mousePosition.y = Screen.height - mousePosition.y;
		}
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	float GetEulerCorrection(float in_value, float in_minimumMagnitude, float in_maximumMagnitude, float in_rate)
	{
		if(in_rate == 0)
		{
			return 0;
		}
		float diff = Mathf.DeltaAngle(in_value, 0);
		if (diff > in_maximumMagnitude)
		{
			return Mathf.LerpAngle(0, diff - in_maximumMagnitude, in_rate);
		}
		else if (diff < in_minimumMagnitude)
		{
			return Mathf.LerpAngle(0, diff - in_minimumMagnitude, in_rate);
		}
		else
		{
			return 0;
		}
	}

	public bool RotateRelativeToCamera = false;
	public bool RestrictWhileDragging = false;
	public Vector3 MaximumEulerAngles = new Vector3(0, 0, 0);
	public Vector3 MinimumEulerAngles = new Vector3(0, 0, 0);
	public Vector3 CorrectionRate = new Vector3(0, 0, 0.5f);
	public Vector2 SwipeDampening = new Vector2(1.0f, 1.0f);
	public float UseFrustrumWidth = 5000;
	public float SamplingDistance;

	private Vector3 _lastPosition;

	private bool _isSwiping = false;
	private Vector3 _start = new Vector3();

	private const float STEPSIZE = 0.01f;
}