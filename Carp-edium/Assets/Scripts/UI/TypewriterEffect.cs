using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Carp {
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text textBox;

        [Header("Test String")]
        [SerializeField]
        private string testText;

        private int _currentVisibleCharacterIndex;
        private Coroutine typewriterCoroutine;

        [SerializeField]
        private WaitForSeconds _simpleDelay;
        [SerializeField]
        private WaitForSeconds _interpunctuationDelay;

        [Header("Typewriter Settings")]
        [SerializeField]
        private float charactersPerSecond = 20;
        [SerializeField]
        private float interpunctuationDelay = 0.5f;

        private string interpunctuations = "?.!,-:;";

        private void Awake() {
            EvtSystem.EventDispatcher.AddListener<SendDialogueText>(HandleSentDialogue);
            EvtSystem.EventDispatcher.AddListener<DialogueEnd>(HandleDialogueEnd);
            EvtSystem.EventDispatcher.AddListener<ShowFullDialogue>(HandleShowFullDialogue);
            textBox = GetComponent<TMP_Text>();
            _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
            _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        }

        private void Start () {
            //SetText(testText);
            textBox.text = "";
        }

        void HandleSentDialogue(SendDialogueText evt) {
            SetText(evt.dialogueText);
        }

        public void SetText(string text) {
            textBox.text = text;
            textBox.maxVisibleCharacters = 0;
            _currentVisibleCharacterIndex = 0;

            typewriterCoroutine = StartCoroutine(Typewriter());
        }

        private IEnumerator Typewriter() {
            TMP_TextInfo textInfo = textBox.textInfo;

            while(_currentVisibleCharacterIndex < textInfo.characterCount + 1) {
                char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

                textBox.maxVisibleCharacters++;

                if (interpunctuations.Contains(character)) {
                    yield return _interpunctuationDelay;
                } else {
                    yield return _simpleDelay;
                }

                _currentVisibleCharacterIndex++;
            }

            EvtSystem.EventDispatcher.Raise<DialogueFullyShown>( new 
                    DialogueFullyShown {});
        }

        void HandleDialogueEnd(DialogueEnd evt) {
            textBox.text = "";
        }

        void HandleShowFullDialogue(ShowFullDialogue _) {
            // If eveything is already shown, let ConvoManager known
            if (textBox.maxVisibleCharacters == textBox.textInfo.characterCount) {
                EvtSystem.EventDispatcher.Raise<NoMoreToShow>( new 
                        NoMoreToShow {});
                return;
            }
            // Otherwise, show everything now
            StopCoroutine(typewriterCoroutine);
            textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
            EvtSystem.EventDispatcher.Raise<DialogueFullyShown>( new 
                    DialogueFullyShown {});
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<SendDialogueText>(HandleSentDialogue);
            EvtSystem.EventDispatcher.RemoveListener<DialogueEnd>(HandleDialogueEnd);
            EvtSystem.EventDispatcher.RemoveListener<ShowFullDialogue>(HandleShowFullDialogue);
        }
    }
}
