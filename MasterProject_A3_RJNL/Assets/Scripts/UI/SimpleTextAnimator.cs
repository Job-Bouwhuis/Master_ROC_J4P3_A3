// Creator: Job
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShadowUprising.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SimpleTextAnimator : MonoBehaviour
    {
        [Multiline, SerializeField, Tooltip("The text that will be displayed on the screen")]
        string text;

        [SerializeField, Tooltip("The time to wait before appending the next character")]
        float timeInBetweenCharacters = 0.1f;

        [SerializeField, Tooltip("The time to wait before clearing the text (-1 to disable)")]
        float timeToWaitBeforeClearing = -1f;

        [SerializeField, Tooltip("The time to wait before starting the animation")]
        float timeToWaitBeforeStarting = 0f;

        [SerializeField, Tooltip("Whether to start writing when the game starts")]
        bool startWritingOnStart = true;

        [SerializeField, Tooltip("Whether to use unscaled time or scaled time for the animation")]
        bool useUnscaledTime = false;

        private TMP_Text tmpText;

        /// <summary>
        /// Starts the writing animation
        /// </summary>
        public void StartWriting() => StartCoroutine(WriteText());
        /// <summary>
        /// Clears the text character by character
        /// </summary>
        public void ClearText() => StartCoroutine(ClearTextField());

        // Start is called before the first frame update
        void Start()
        {
            tmpText = GetComponent<TMP_Text>();
            tmpText.text = "";
            if (startWritingOnStart)
                StartCoroutine(WriteText());
        }

        IEnumerator WriteText()
        {
            if(useUnscaledTime)
                yield return new WaitForSecondsRealtime(timeToWaitBeforeStarting);
            else
                yield return new WaitForSeconds(timeToWaitBeforeStarting);

            foreach (char c in text)
            {
                tmpText.text += c;
                if (useUnscaledTime)
                    yield return new WaitForSecondsRealtime(timeInBetweenCharacters);
                else
                    yield return new WaitForSeconds(timeInBetweenCharacters);
            }
        }

        IEnumerator ClearTextField()
        {
            if (useUnscaledTime)
                yield return new WaitForSecondsRealtime(timeToWaitBeforeClearing);
            else
                yield return new WaitForSeconds(timeToWaitBeforeClearing);
            for (int i = tmpText.text.Length; i >= 0; i--)
            {
                tmpText.text = tmpText.text.Remove(i);
                if (useUnscaledTime)
                    yield return new WaitForSecondsRealtime(timeInBetweenCharacters);
                else
                    yield return new WaitForSeconds(timeInBetweenCharacters);
            }
        }
    }
}