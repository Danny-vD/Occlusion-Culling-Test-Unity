using Attributes;
using UnityEngine;
using VDFramework;

namespace Utility.PhysicsUtil
{
	public class SpeedMeasurer : BetterMonoBehaviour
	{
		[SerializeField, InspectorReadOnly]
		private float speed;
		
		/// <summary>
		/// Get the current speed of the object in m/s
		/// <para>Will return the rigidbody velocity if one is present</para>
		/// </summary>
		public float Speed => TryGetComponent(out Rigidbody rigidbdy) ? rigidbdy.velocity.magnitude : speed;
		
		private Vector3 oldPosition;

		private void Start()
		{
			speed = 0;
		}

		private void LateUpdate()
		{
			speed = CalculateSpeed();

			oldPosition = CachedTransform.position;
		}

		private float CalculateSpeed()
		{
			Vector3 currentPosition = CachedTransform.transform.position;
			float deltaMovement = Vector3.Distance(currentPosition, oldPosition);

			return deltaMovement / Time.deltaTime;
		}
	}
}