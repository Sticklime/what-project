//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly CodeBase.Components.InputContext.MouseInputComponent mouseInputComponent = new CodeBase.Components.InputContext.MouseInputComponent();

    public bool isMouseInput {
        get { return HasComponent(InputComponentsLookup.MouseInput); }
        set {
            if (value != isMouseInput) {
                var index = InputComponentsLookup.MouseInput;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : mouseInputComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherMouseInput;

    public static Entitas.IMatcher<InputEntity> MouseInput {
        get {
            if (_matcherMouseInput == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.MouseInput);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherMouseInput = matcher;
            }

            return _matcherMouseInput;
        }
    }
}
