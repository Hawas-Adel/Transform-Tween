using NaughtyAttributes;
using UnityEngine;

namespace TransformTween
{
	[AddComponentMenu("Transform Tween/Tween Transform")]
	public class TweenTransform : TransformTweenBase
	{
		// Edge Positions
		[SerializeField] private TransformValues StartTransform = default;
		[Button]
		private void SetStartTransform()
		{
			StartTransform.FromTransform(transform);
			_T = 0;
		}

		[SerializeField] private TransformValues EndTransform = default;
		[Button]
		private void SetEndTransform()
		{
			EndTransform.FromTransform(transform);
			_T = 1;
		}

		/// <summary>
		/// Lerp Parameter
		/// </summary>
		public override float T
		{
			get => _T;
			set
			{
				_T = value;
				var Lerped = TransformValues.Lerp(StartTransform, EndTransform, _T);
				if (RelativeMovement)
				{
					Lerped += StartingValue;
				}
				Lerped.ToTransform(transform);
			}
		}

		private TransformValues StartingValue = default;
		private void Awake()
		{
			StartingValue = new TransformValues();
			StartingValue.FromTransform(transform);
		}
	}

	[System.Serializable]
	public class TransformValues
	{
		public Vector3 Position;
		public Vector3 Rotation;
		public Vector3 Scale;

		public void FromTransform(Transform transform)
		{
			Position = transform.localPosition;
			Rotation = transform.localEulerAngles;
			Scale = transform.localScale;
		}
		public void ToTransform(Transform transform)
		{
			transform.localPosition = Position;
			transform.localEulerAngles = Rotation;
			transform.localScale = Scale;
		}

		public static TransformValues Lerp(TransformValues A, TransformValues B, float T)
			=> new TransformValues
			{
				Position = Vector3.Lerp(A.Position, B.Position, T),
				Rotation = Vector3.Lerp(A.Rotation, B.Rotation, T),
				Scale = Vector3.Lerp(A.Scale, B.Scale, T)
			};

		public static TransformValues operator +(TransformValues L, TransformValues R)
			=> new TransformValues
			{
				Position = L.Position + R.Position,
				Rotation = L.Rotation + R.Rotation,
				Scale = L.Scale + R.Scale
			};
	}

}