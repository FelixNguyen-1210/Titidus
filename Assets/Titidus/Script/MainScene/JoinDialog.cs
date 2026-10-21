using System;
using DG.Tweening;
using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Titidus
{
    public class JoinDialog : MonoBehaviour
    {

        [SerializeField] TMP_InputField m_joinRoomInput;
        [SerializeField] Button m_cancelBtn;
        [SerializeField] Button m_pasteBtn;
        [SerializeField] Button m_joinBtn;
        [SerializeField] float WrongInputShakeStrength = 12;
        [SerializeField] int WrongInputShakeVibrato = 2;
        [SerializeField] float WrongInputShakeDuration = 0.5f;

        [SerializeField] GameObject loadingPopup;

        void OnEnable()
        {
            m_cancelBtn.onClick.AddListener(() => Cancel());
            m_pasteBtn.onClick.AddListener(() => Paste());
            m_joinBtn.onClick.AddListener(() => Join());
        }
        public void DisplayLoading()
        {
            if (loadingPopup) loadingPopup.gameObject.SetActive(true);
            else Debug.LogWarning("Loading Icon Not Found");
        }
        public async void Join()
        {
            RelayManager relayManager = RelayManager.Instance;
            if (!relayManager)
            {
                Debug.LogWarning("RelayManager not found");
                return;
            }

            try
            {
                string code = m_joinRoomInput.text.Trim().ToUpperInvariant();

                DisplayLoading();
                gameObject.SetActive(false);

                await relayManager.JoinRelay(code);
            }
            catch
            {

                m_joinRoomInput.GetComponent<Animator>().SetTrigger("Shake");
                RectTransform rect = m_joinRoomInput.GetComponent<RectTransform>();
                rect.DOKill();

                rect.DOShakeAnchorPos(
        duration: WrongInputShakeDuration,
        strength: new Vector2(WrongInputShakeStrength, 0.1f),
        vibrato: WrongInputShakeVibrato,
        randomness: 0f,
        snapping: false,
        fadeOut: true
                );
            }
        }
        public void Cancel()
        {
            gameObject.SetActive(false);
        }
        public void Paste()
        {
            m_joinRoomInput.text = GUIUtility.systemCopyBuffer;
        }
    }
}