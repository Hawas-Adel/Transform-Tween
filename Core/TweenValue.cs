using UnityEngine;

namespace TransformTween
{
	[AddComponentMenu("Transform Tween/Tween Value")]
	public class TweenValue : TransformTweenBase
	{
		[SerializeField] private TweenTargetValue TargetValue = default;
		// Edge Positions
		[SerializeField] private float StartValue = default;
		[SerializeField] private float EndValue = default;
		public override float T
		{
			get => _T;
			set
			{
				_T = value;
				float lerped = Mathf.Lerp(StartValue, EndValue, _T);
				switch (TargetValue)
				{
					case TweenTargetValue.PositionX:
						transform.localPosition = new Vector3(lerped, transform.localPosition.y, transform.localPosition.z);
						break;
					case TweenTargetValue.PositionY:
						transform.localPosition = new Vector3(transform.localPosition.x, lerped, transform.localPosition.z);
						break;
					case TweenTargetValue.PositionZ:
						transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, lerped);
						break;
					//case TweenTargetValue.RelativePositionX:
					//	transform.localPosition = transform.TransformVector(lerped, 0, 0);
					//	break;
					//case TweenTargetValue.RelativePositionY:
					//	transform.localPosition = transform.TransformVector(0, lerped, 0);
					//	break;
					//case TweenTargetValue.RelativePositionZ:
					//	transform.localPosition = transform.TransformVector(0, 0, lerped);
					//	break;
					//case TweenTargetValue.RotationX:
					//	transform.localEulerAngles = new Vector3(lerped, transform.localEulerAngles.y, transform.localEulerAngles.z);
					//	break;
					//case TweenTargetValue.RotationY:
					//	transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, lerped, transform.localEulerAngles.z);
					//	break;
					case TweenTargetValue.RotationZ:
						transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, lerped);
						break;
					case TweenTargetValue.ScaleX:
						transform.localScale = new Vector3(lerped, transform.localScale.y, transform.localScale.z);
						break;
					case TweenTargetValue.ScaleY:
						transform.localScale = new Vector3(transform.localScale.x, lerped, transform.localScale.z);
						break;
					case TweenTargetValue.ScaleZ:
						transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lerped);
						break;
					case TweenTargetValue.UniformScale:
						transform.localScale = new Vector3(lerped, lerped, lerped);
						break;
				}

				if (RelativeMovement)
				{
					switch (TargetValue)
					{
						case TweenTargetValue.PositionX:
							//case TweenTargetValue.RelativePositionX:
							transform.localPosition += StartingValue * Vector3.right;
							break;
						case TweenTargetValue.PositionY:
							//case TweenTargetValue.RelativePositionY:
							transform.localPosition += StartingValue * Vector3.up;
							break;
						case TweenTargetValue.PositionZ:
							//case TweenTargetValue.RelativePositionZ:
							transform.localPosition += StartingValue * Vector3.forward;
							break;
						//case TweenTargetValue.RotationX:
						//	transform.localEulerAngles += StartingValue * Vector3.right;
						//	break;
						//case TweenTargetValue.RotationY:
						//	transform.localEulerAngles += StartingValue * Vector3.up;
						//	break;
						case TweenTargetValue.RotationZ:
							transform.localEulerAngles += StartingValue * Vector3.forward;
							break;
						case TweenTargetValue.ScaleX:
							transform.localScale += StartingValue * Vector3.right;
							break;
						case TweenTargetValue.ScaleY:
							transform.localScale += StartingValue * Vector3.up;
							break;
						case TweenTargetValue.ScaleZ:
							transform.localScale += StartingValue * Vector3.forward;
							break;
						case TweenTargetValue.UniformScale:
							transform.localScale += StartingValue * Vector3.one;
							break;
					}
				}
			}
		}

		private float StartingValue = default;
		private void Awake()
		{
			switch (TargetValue)
			{
				case TweenTargetValue.PositionX:
					//case TweenTargetValue.RelativePositionX:
					StartingValue = transform.localPosition.x;
					break;
				case TweenTargetValue.PositionY:
					//case TweenTargetValue.RelativePositionY:
					StartingValue = transform.localPosition.y;
					break;
				case TweenTargetValue.PositionZ:
					//case TweenTargetValue.RelativePositionZ:
					StartingValue = transform.localPosition.z;
					break;
				//case TweenTargetValue.RotationX:
				//	StartingValue = transform.localEulerAngles.x;
				//	break;
				//case TweenTargetValue.RotationY:
				//	StartingValue = transform.localEulerAngles.y;
				//	break;
				case TweenTargetValue.RotationZ:
					StartingValue = transform.localEulerAngles.z;
					break;
				case TweenTargetValue.ScaleX:
					StartingValue = transform.localScale.x;
					break;
				case TweenTargetValue.ScaleY:
					StartingValue = transform.localScale.y;
					break;
				case TweenTargetValue.ScaleZ:
					StartingValue = transform.localScale.z;
					break;
				case TweenTargetValue.UniformScale:
					StartingValue = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
					break;
			}
		}
	}

	public enum TweenTargetValue
	{
		PositionX,
		PositionY,
		PositionZ,
		//RelativePositionX,
		//RelativePositionY,
		//RelativePositionZ,
		//RotationX,
		//RotationY,
		RotationZ,
		ScaleX,
		ScaleY,
		ScaleZ,
		UniformScale
	}
}
