//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public CodeBase.Components.InputContext.RaycastInputComponent raycastInput { get { return (CodeBase.Components.InputContext.RaycastInputComponent)GetComponent(InputComponentsLookup.RaycastInput); } }
    public bool hasRaycastInput { get { return HasComponent(InputComponentsLookup.RaycastInput); } }

    public void AddRaycastInput(UnityEngine.Vector3 newTargetPosition, UnityEngine.Vector3 newStartPositionSelection, UnityEngine.Vector3 newEndPositionSelection, bool newIsSelection) {
        var index = InputComponentsLookup.RaycastInput;
        var component = (CodeBase.Components.InputContext.RaycastInputComponent)CreateComponent(index, typeof(CodeBase.Components.InputContext.RaycastInputComponent));
        component.TargetPosition = newTargetPosition;
        component.StartPositionSelection = newStartPositionSelection;
        component.EndPositionSelection = newEndPositionSelection;
        component.IsSelection = newIsSelection;
        AddComponent(index, component);
    }

    public void ReplaceRaycastInput(UnityEngine.Vector3 newTargetPosition, UnityEngine.Vector3 newStartPositionSelection, UnityEngine.Vector3 newEndPositionSelection, bool newIsSelection) {
        var index = InputComponentsLookup.RaycastInput;
        var component = (CodeBase.Components.InputContext.RaycastInputComponent)CreateComponent(index, typeof(CodeBase.Components.InputContext.RaycastInputComponent));
        component.TargetPosition = newTargetPosition;
        component.StartPositionSelection = newStartPositionSelection;
        component.EndPositionSelection = newEndPositionSelection;
        component.IsSelection = newIsSelection;
        ReplaceComponent(index, component);
    }

    public void RemoveRaycastInput() {
        RemoveComponent(InputComponentsLookup.RaycastInput);
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

    static Entitas.IMatcher<InputEntity> _matcherRaycastInput;

    public static Entitas.IMatcher<InputEntity> RaycastInput {
        get {
            if (_matcherRaycastInput == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.RaycastInput);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherRaycastInput = matcher;
            }

            return _matcherRaycastInput;
        }
    }
}
