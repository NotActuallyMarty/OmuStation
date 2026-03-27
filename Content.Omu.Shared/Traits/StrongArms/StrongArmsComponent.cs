using Content.Omu.Common.Traits.StrongArms;
using Content.Shared.Dataset;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Omu.Shared.Traits.StrongArms;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class StrongArmsComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public ProtoId<LocalizedDatasetPrototype> PryingMessage;

}
