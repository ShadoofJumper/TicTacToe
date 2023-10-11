using DG.Tweening;
using UnityEngine;

namespace Controllers.SceneView
{
    public class HintMark : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private const float BeamTime = 1.5f;
        private Tween _tweenFadeUp;
        private Tween _tweenFadeOut;

        private float _startFade;
        
        private void Awake()
        {
            _startFade = _spriteRenderer.color.a;
            Color fadeColor = _spriteRenderer.color;
            fadeColor.a = 0;
            _spriteRenderer.color = fadeColor;
        }

        void Start()
        {
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _tweenFadeUp = _spriteRenderer.DOFade(_startFade, BeamTime)
                .OnComplete(() =>
            {
                _tweenFadeOut = _spriteRenderer.DOFade(0, BeamTime)
                    .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }

        private void OnDestroy()
        {
            _tweenFadeUp?.Kill();
            _tweenFadeOut?.Kill();
        }
    }
}


