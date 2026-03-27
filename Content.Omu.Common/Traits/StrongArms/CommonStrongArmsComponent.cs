using Robust.Shared.GameStates;

namespace Content.Omu.Common.Traits.StrongArms;

/// <summary>
/// Marker Component for StrongArmsComponent... and now Carrybonus. Holy shit we need to kill carryingsystem or at least mod it.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CommonStrongArmsComponent : Component
{
    /// <summary>
    /// How much to offset hands required by CarriableComponent in the negative.
    /// If something requires 2 hands, FreeHandsRequired(2) - Carrybonus(1) = 1.
    /// Bonus will not bring FreeHandsRequired below 1.
    /// </summary>
    [DataField, AutoNetworkedField]
    public int CarryBonus = 1;

}
