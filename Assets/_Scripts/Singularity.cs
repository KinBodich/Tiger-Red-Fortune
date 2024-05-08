using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Mechanics
{
    public class Singularity : MonoBehaviour
    {
        [SerializeField] private Light2D _spotLight;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            _spotLight.pointLightInnerRadius = 0f;
            _spotLight.pointLightOuterRadius = 0f;
        }

        public IEnumerator ScalePrefab(float targetScale, float duration)
        {
            float scaleChangeSpeed = Mathf.Abs((transform.localScale.x - targetScale) / duration);
            float elapsedTime = 0f;

            float initialInnerRadius = _spotLight.pointLightInnerRadius;
            float initialOuterRadius = _spotLight.pointLightOuterRadius;

            while (elapsedTime < duration)
            {
                float currentScale = Mathf.Lerp(transform.localScale.x, targetScale, elapsedTime / duration);
                transform.localScale = Vector3.one * currentScale;

                float targetInnerRadius = 0f;
                float targetOuterRadius = 0f;

                if (targetScale > 0)
                {
                    targetInnerRadius = Mathf.Lerp(initialInnerRadius, targetScale * 0.4f, currentScale / targetScale);
                    targetOuterRadius = Mathf.Lerp(initialOuterRadius, targetScale * 0.8f, currentScale / targetScale);
                }

                _spotLight.pointLightInnerRadius = targetInnerRadius;
                _spotLight.pointLightOuterRadius = targetOuterRadius;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one * targetScale;

            if (targetScale <= 0)
            {
                _spotLight.pointLightInnerRadius = 0f;
                _spotLight.pointLightOuterRadius = 0f;
            }
        }
    }
}