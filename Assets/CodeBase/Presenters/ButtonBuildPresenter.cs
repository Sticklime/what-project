using UnityEngine.UIElements;

namespace CodeBase.Presenters
{
    public class ButtonBuildPresenter : MonoPresenter
    {
        private VisualElement _buttonBuild;
        private Button _button;

        public ButtonBuildPresenter(VisualElement buttonBuild)
        {
            _buttonBuild = buttonBuild;
        }

        public override void OnEnable()
        {
            _button = _buttonBuild.Q<Button>("Button");

            _button.clicked += CreateBuild;
        }

        public override void OnDisable()
        {
            _button.clicked -= CreateBuild;
        }

        private void CreateBuild()
        {
        }
    }
}