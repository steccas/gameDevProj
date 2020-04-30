using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		public Transform detectPoint;
		public LayerMask playerLayer;

		IAstarAI ai;
		public float detectRange;

		private bool playerDetected = false;

		public bool getDetected()
		{
			return playerDetected;
		}

		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			if (target != null && ai != null && playerDetected) { ai.isStopped = false; ai.destination = target.position; }
			else ai.isStopped = true;
		}

		private void FixedUpdate()
		{
			playerDetected = Detect();
		}

		private bool Detect ()
		{
			//detect
			Collider2D detection = Physics2D.OverlapCircle(detectPoint.position, detectRange, playerLayer);

			if (detection == null) return false;
			else {
				Debug.Log("Found " + detection);
				return true; 
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (detectPoint == null)
				return;

			Gizmos.DrawWireSphere(detectPoint.position, detectRange);
		}
	}
}
