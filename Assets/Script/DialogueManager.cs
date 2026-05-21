using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필수 추가

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소")]
    public Text dialogueText;       // 대사가 출력될 레거시 Text 컴포넌트

    [Header("대사 데이터")]
    [TextArea(3, 5)]
    public string[] sentences;          // 출력할 대사들을 저장하는 배열

    [Header("설정")]
    public float typingSpeed = 0.05f;   // 글자가 타이핑되는 속도

    [Header("씬 전환 설정")]
    public Image fadeImage;             // 페이드 효과에 사용할 UI Image
    public float fadeDuration = 1.0f;   // 페이드아웃에 걸리는 시간 (초)
    public string nextSceneName;        // 이동할 다음 씬의 정확한 이름

    private int currentIndex = 0;       // 현재 대사 번호
    private bool isTyping = false;      // 현재 글자가 타이핑 중인지 여부
    private bool isEnding = false;      // 대사가 끝나고 씬 전환 중인지 체크
    private Coroutine typingCoroutine;  // 타이핑 코루틴 제어용

    void Start()
    {
        // 페이드 이미지 초기화 (시작할 때는 투명하게)
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
            fadeImage.gameObject.SetActive(false);
        }

        // 게임 시작 시 첫 대사 출력
        StartDialogue();
    }

    void Update()
    {
        // 이미 대사가 끝나고 씬 전환 중이라면 입력을 무시
        if (isEnding) return;

        // 마우스 좌클릭 또는 스페이스바를 눌렀을 때
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // 1. 글자가 나오는 중이었다면 -> 즉시 전체 대사 출력
                FinishSentenceEarly();
            }
            else
            {
                // 2. 글자 출력이 끝난 상태였다면 -> 다음 대사로 진행
                DisplayNextSentence();
            }
        }
    }

    // 대사 시스템 시작
    public void StartDialogue()
    {
        currentIndex = 0;
        DisplayNextSentence();
    }

    // 다음 대사 넘기기
    public void DisplayNextSentence()
    {
        // 모든 대사가 끝났다면
        if (currentIndex >= sentences.Length)
        {
            EndDialogue();
            return;
        }

        // 기존에 돌고 있던 타이핑 코루틴이 있다면 멈춤
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 새 대사 타이핑 시작
        typingCoroutine = StartCoroutine(TypeSentence(sentences[currentIndex]));
        currentIndex++;
    }

    // 한 글자씩 출력하는 코루틴
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 설정한 속도만큼 대기
        }

        isTyping = false;
    }

    // 대사 즉시 완성하기
    void FinishSentenceEarly()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        // 현재 인덱스가 이미 증가했으므로 -1 해줌
        dialogueText.text = sentences[currentIndex - 1]; 
        isTyping = false;
    }

    // 모든 대사가 종료되었을 때 호출
    void EndDialogue()
    {
        isEnding = true;
        dialogueText.text = ""; // 대사창 글자 비우기
        
        // 페이드아웃 코루틴 시작
        StartCoroutine(FadeOutAndLoadScene());
    }

    // 화면을 어둡게 만든 뒤 씬을 전환하는 코루틴
    IEnumerator FadeOutAndLoadScene()
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image가 연결되지 않았습니다! 바로 씬을 전환합니다.");
            SceneManager.LoadScene(nextSceneName);
            yield break;
        }

        fadeImage.gameObject.SetActive(true);
        Color startColor = fadeImage.color;
        startColor.a = 0f;
        fadeImage.color = startColor;

        float elapsedTime = 0f;

        // 투명도(Alpha)를 0에서 1로 서서히 증가시킴
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        // 페이드 완료 후 완전히 어두워졌을 때 다음 씬 로드
        SceneManager.LoadScene(nextSceneName);
    }
}
