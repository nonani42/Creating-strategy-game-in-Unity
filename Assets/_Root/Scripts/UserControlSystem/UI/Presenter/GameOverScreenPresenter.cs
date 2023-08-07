using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using System.Text;
using Abstractions;

namespace UI.Presenter
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _view.SetActive(false);

            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                var sb = new StringBuilder($"Game Over! ");
                if (result == 0)
                {
                    sb.AppendLine("Tie!");
                }
                else
                {
                    sb.AppendLine($"Team #{result} won!");
                }
                _view.SetActive(true);
                _text.text = sb.ToString();
                Time.timeScale = 0;
            });
        }
    }
}
