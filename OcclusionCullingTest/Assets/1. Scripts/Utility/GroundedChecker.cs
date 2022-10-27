using System.Collections.Generic;
using UnityEngine;
using VDFramework;

namespace Utility
{
	[DisallowMultipleComponent]
	public class GroundedChecker : BetterMonoBehaviour
	{
		[SerializeField, Header("Layers that will never count as grounded when touched")]
		private LayerMask IgnoreLayer = 1 << 10 | 1 << 5; // Player and UI Layer

		[SerializeField, Header("The maximum allowed slope in degrees for it to still count as grounded"), Range(0, 90)]
		private float slopeThreshold = 30;

		[SerializeField, Tooltip("Above this distance any contact points with a surface will be ignored" +
								 "(useful when OnCollisionExit does not trigger correctly)")]
		private float objectHeight = 1.0f;

		private bool isGrounded;

		/// <summary>
		/// Will check whether any touched surface normal is within the grounded angle threshold
		/// </summary>
		public bool IsGrounded => checkedThisFrame ? isGrounded : CheckGrounded();

		/// <summary>
		/// Returns a copy of all the contact points
		/// </summary>
		public List<ContactPoint> ContactPoints => new List<ContactPoint>(contactPoints);

		private bool checkedThisFrame = false;

		private readonly List<ContactPoint> contactPoints = new List<ContactPoint>();

		private bool CheckGrounded()
		{
			checkedThisFrame = true;
			isGrounded       = false;

			// Remove the nulls and any points above a certain distance (in case collission exit was not triggered correctly)
			contactPoints.RemoveAll(point =>
				point.otherCollider == null || point.otherCollider.gameObject == null ||
				Vector3.Distance(CachedTransform.position, point.point) > objectHeight);

			foreach (ContactPoint point in contactPoints)
			{
				float normalAngle = Vector3.Angle(point.normal, Vector3.up);

				Physics.Raycast(point.point, -point.normal, out RaycastHit hit, 0.2f, ~IgnoreLayer);

				float raycastAngle = Vector3.Angle(hit.normal, Vector3.up);

				if (normalAngle < slopeThreshold || raycastAngle < slopeThreshold)
				{
					isGrounded = true;
					break;
				}
			}

			return isGrounded;
		}

		private void FixedUpdate()
		{
			checkedThisFrame = false;
		}

		private void OnCollisionEnter(Collision other)
		{
			if ((1 << other.gameObject.layer & IgnoreLayer) > 0)
			{
				return;
			}

			foreach (ContactPoint contact in other.contacts)
			{
				contactPoints.Add(contact);
			}
		}

		private void OnCollisionStay(Collision other)
		{
			if ((1 << other.gameObject.layer & IgnoreLayer) > 0)
			{
				return;
			}

			// If otherCollider is null, their gameobject is null or we left the collider of the other gameobject, remove all points from them
			contactPoints.RemoveAll(point =>
				point.otherCollider == null || point.otherCollider.gameObject == null ||
				point.otherCollider.gameObject == other.gameObject);

			foreach (ContactPoint contact in other.contacts)
			{
				contactPoints.Add(contact);
			}
		}

		private void OnCollisionExit(Collision other)
		{
			if ((1 << other.gameObject.layer & IgnoreLayer) > 0)
			{
				return;
			}

			// If otherCollider is null, their gameobject is null or we left the collider of the other gameobject, remove all points from them
			contactPoints.RemoveAll(point =>
				point.otherCollider == null || point.otherCollider.gameObject == null ||
				point.otherCollider.gameObject == other.gameObject);
		}
	}
}