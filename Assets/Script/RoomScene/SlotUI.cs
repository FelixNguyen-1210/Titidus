using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Titidus
{
    public class SlotUI : MonoBehaviour
    {

    [SerializeField] private Image avatarImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject inviteBtn = null;
        [SerializeField] private GameObject playerUI;
        public void SetPlayer(string playerName, Sprite avatar)
        {
            playerUI.SetActive(true);
            nameText.text = playerName;
            avatarImage.sprite = avatar;
            avatarImage.enabled = avatar != null;

            if (inviteBtn != null)
                inviteBtn.SetActive(false);
        }

        public void Clear()
        {
            playerUI.SetActive(false);

            nameText.text = "";
            avatarImage.sprite = null;
            avatarImage.enabled = false;

            if (inviteBtn != null)
                inviteBtn.SetActive(true);
        }

    }   
}