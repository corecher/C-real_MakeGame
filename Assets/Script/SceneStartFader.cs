using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneStartFader : MonoBehaviour
{
    [Header("UI 요소")]
    public Image fadeImage;      // 화면을 덮을 검은색 이미지
    public float fadeDuration = 1.0f;

    void Start()
    {
        // 시작하자마자 페이드 인 실행
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        if (fadeImage == null) yield break;

        fadeImage.gameObject.SetActive(true);
        
        // 시작은 완전히 검은색(Alpha 1)
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 1에서 0으로 투명도 감소
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        // 투명해진 후 뒤에 있는 게임 버튼 등을 누를 수 있게 반드시 비활성화
        fadeImage.gameObject.SetActive(false);
    }
}
