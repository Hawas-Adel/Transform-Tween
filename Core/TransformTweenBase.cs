using Candlelight;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TransformTween
{
	public enum TransformTweenTrigger
	{
		[InspectorName("[None]")] None,
		Tween,
		TweenBackward,
		TweenLoop,
		TweenZigZag
	}

	public abstract class TransformTweenBase : MonoBehaviour
	{
		[SerializeField] [Tooltip("Tween Is Added to the transform's initial value on Start")] protected bool RelativeMovement;

		[SerializeField] [Tooltip("Tween method in OnEnable")] private TransformTweenTrigger Trigger = default;
		private void OnEnable()
		{
			switch (Trigger)
			{
				case TransformTweenTrigger.Tween:
					Tween();
					break;
				case TransformTweenTrigger.TweenBackward:
					TweenBackward();
					break;
				case TransformTweenTrigger.TweenLoop:
					TweenLoop();
					break;
				case TransformTweenTrigger.TweenZigZag:
					TweenZigZag();
					break;
				default:
					break;
			}
		}

		// Lerp Parameter
		[SerializeField] [PropertyBackingField(typeof(RangeAttribute), 0f, 1f)] protected float _T = 0;
		public abstract float T { get; set; }

		/// <summary>
		/// Slide Speed
		/// </summary>
		[Min(0)] public float Speed = 1;

		[Foldout("Events")] public UnityEvent TweenStart;
		[Foldout("Events")] public UnityEvent TweenEnd;
		[Foldout("Events")] public UnityEvent TweenLoopIteration;

		#region Methods and their abstract Coroutines

		private Coroutine Coroutine;
		/// <summary>
		/// Smoothly move from Start value to End value
		/// </summary>
		public void Tween() => Coroutine = StartCoroutine(SlideToEndCoroutine());
		public IEnumerator SlideToEndCoroutine()
		{
			TweenStart.Invoke();
			for (float t = T ; t < 1 ; t += Time.deltaTime * Speed)
			{
				T = t;
				yield return new WaitForEndOfFrame();
			}
			T = 1;
			Coroutine = null;
			TweenEnd.Invoke();
		}

		/// <summary>
		/// Smoothly move from End value to Start value
		/// </summary>
		public void TweenBackward() => Coroutine = StartCoroutine(SlideToStartCoroutine());
		public IEnumerator SlideToStartCoroutine()
		{
			TweenStart.Invoke();
			for (float t = T ; t > 0 ; t -= Time.deltaTime * Speed)
			{
				T = t;
				yield return new WaitForEndOfFrame();
			}
			T = 0;
			Coroutine = null;
			TweenEnd.Invoke();
		}

		/// <summary>
		/// Smoothly move from Start to End, jump back to Start and repeat, Invoke TweenLoopIteration event after each cycle
		/// </summary>
		public void TweenLoop() => Coroutine = StartCoroutine(LoopCoroutine());
		public IEnumerator LoopCoroutine()
		{
			TweenStart.Invoke();
			for (float t = T ; ; t += Time.deltaTime * Speed)
			{
				if (t > 1)
				{
					t = 0;
					TweenLoopIteration.Invoke();
				}
				T = t;
				yield return new WaitForEndOfFrame();
			}
		}

		/// <summary>
		/// Smoothly move from Start to End to Start and repeat,Invoke TweenLoopIteration event at both ends
		/// </summary>
		public void TweenZigZag() => Coroutine = StartCoroutine(ZigZagCoroutine());
		public IEnumerator ZigZagCoroutine()
		{
			TweenStart.Invoke();
			while (true)
			{
				for (float t = T ; t < 1 ; t += Time.deltaTime * Speed)
				{
					T = t;
					yield return new WaitForEndOfFrame();
				}
				T = 1;
				TweenLoopIteration.Invoke();
				yield return new WaitForEndOfFrame();

				for (float t = 1 ; t > 0 ; t -= Time.deltaTime * Speed)
				{
					T = t;
					yield return new WaitForEndOfFrame();
				}
				T = 0;
				TweenLoopIteration.Invoke();
				yield return new WaitForEndOfFrame();
			}
		}

		/// <summary>
		/// Stop any Tween action in progress
		/// </summary>
		public void Stop()
		{
			if (Coroutine != null)
			{
				StopCoroutine(Coroutine);
				Coroutine = null;
				TweenEnd.Invoke();
			}
		}
		#endregion
	}
}
