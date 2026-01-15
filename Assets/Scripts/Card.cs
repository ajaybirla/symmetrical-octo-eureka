using UnityEngine;
using UnityEngine.UI;

namespace SymmetricalOctoEureka
{
    public class Card : MonoBehaviour
    {
        [Header ("Images")]
        [SerializeField] public Image frontImage;
        [SerializeField] private Image backImage;

        [Header ("Buttons")]
        [SerializeField] private Button button;

        public int Id { get; private set; }

        public void Initialize (int id, Sprite frontSprite)
        {
            name = $"Card_{id}";
            Id = id;
            frontImage.sprite = frontSprite;
        }
    }
}