/*using Content.Shared.Dataset;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Omu.Client.Species.Oni;

// todo ive not written this at the time of writing but its probably ass rework later.

/// <summary>
/// Component to display markup for other oni about their fighting style and to display experts fighting styles of others.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class OniWeaponProficiencyComponent : Component
{
    /// <summary>
    /// Fighting style of oni depending on trait. Default to Brawler if no trait.
    /// </summary>
    [Serializable, NetSerializable]
    public enum ProficiencyType : byte
    {
        None,
        Brawler,
        SpearMaster,
        SwashBuckler,
        WallBreaker,
        Shunned,
    }

    [DataField, AutoNetworkedField]
    public ProficiencyType Proficiency = ProficiencyType.Brawler;

    /// <summary>
    /// Can user see the fighting style of other enties with it. Oni only atm.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool IsFightingExpert;

    /// <summary>
    /// Should the user be able to see other martial arts i.e. sleeping carp, judo, capoeira, cqc, etc. when their combatMode unarmed in enabled.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool CanSeeOtherMartialArts;

    /// <summary>
    /// Should  the user get popups if dangerous fighters (martial arts) enable combat mode unarmed near them.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool FightingInstinct;

    /// <summary>
    /// Set of messages for instinctual fighting popups. Does not matter if FightingInstinct is false.
    /// </summary>
    [DataField, AutoNetworkedField]
    public ProtoId<LocalizedDatasetPrototype> InstinctMessages;
}

*/
