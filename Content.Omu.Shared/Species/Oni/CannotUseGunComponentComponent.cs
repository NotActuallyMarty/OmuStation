using Content.Shared.Dataset;
using Content.Shared.Humanoid.Prototypes;
using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Content.Shared.Humanoid;
using Content.Shared.Weapons.Ranged.Components;

namespace Content.Omu.Shared.Species.Oni;

// todo this should probably be a generic system not just for one component but there's too many edge cases that i don't want to think about rn

/// <summary>
/// The name refers to the fact that this prevents usage of anything with <see cref="GunComponent"/>, not preventing usage of 'guns' specifically
/// May cancel usage of things that are not implicitly guns.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CannotUseGunComponentComponent : Component
{
    /// <summary>
    /// This refers to the original species of the User, not their current species.
    /// Usually the same but will differ in cases User is transplanted, transformed, etc.
    ///
    /// Here cause its annoying getting the player profile in shared.
    /// Use the same speciesId as set in <see cref="HumanoidAppearanceComponent"/>
    /// </summary>
    [DataField(required: true), AutoNetworkedField]
    public ProtoId<SpeciesPrototype> Species;

    /// <summary>
    ///  Whether the user should base its usage based on the rules of the original species.
    ///
    ///  i.e. User original species does not allow use gun, but new one does.
    ///  Setting this true will make them unable to use it irrespective of their current species
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool RespectOriginalSpecies;

    [DataField, AutoNetworkedField]
    public ProtoId<LocalizedDatasetPrototype> UsingGunMessage;

    [DataField, AutoNetworkedField]
    public ProtoId<LocalizedDatasetPrototype> PickUpGunMessage;

    [DataField, AutoNetworkedField]
    public TimeSpan LastPopup;

    [DataField, AutoNetworkedField]
    public ProtoId<TagPrototype> NotAGunTag = "NotAGun";
}
