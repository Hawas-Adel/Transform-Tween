using UnityEngine;

namespace TransformTween
{
	[AddComponentMenu("Transform Tween/Tween Vector 3")]
	public class TweenVector3 : TransformTweenBase
	{
		[SerializeField] private TweenTargetVector3 TargetVector3 = default;
		// Edge Positions
		[SerializeField] private Vector3 StartValue = default;
		[SerializeField] private Vector3 EndValue = default;

		public override float T
		{
			get => _T;
			set
			{
				_T = value;
				Vector3 lerped = Vector3.Lerp(StartValue, EndValue, _T);
				if (RelativeMovement && TargetVector3 != TweenTargetVector3.RelativePosition)
				{
					lerped += StartingValue;
				}
				switch (TargetVector3)
				{
					case TweenTargetVector3.Position:
						transform.localPosition = lerped;
						break;
					case TweenTargetVector3.RelativePosition:
						transform.localPosition = transform.TransformVector(lerped);
						if (RelativeMovement)
						{
							transform.localPosition += StartingValue;
						}
						break;
					case TweenTargetVector3.Rotation:
						transform.localEulerAngles = lerped;
						break;
					case TweenTargetVector3.Scale:
						transform.localScale = lerped;
						break;
				}
			}
		}

		private Vector3 StartingValue = default;
		private void Awake()
		{
			switch (TargetVector3)
			{
				case TweenTargetVector3.Position:
				case TweenTargetVector3.RelativePosition:
					StartingValue = transform.localPosition;
					break;
				case TweenTargetVector3.Rotation:
					StartingValue = transform.localEulerAngles;
					break;
				case TweenTargetVector3.Scale:
					StartingValue = transform.localScale;
					break;
			}
		}
	}

	public enum TweenTargetVector3 { Position, RelativePosition, Rotation, Scale }
}
