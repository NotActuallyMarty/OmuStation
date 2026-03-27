using Content.Omu.Common.Traits.StrongArms;
using Content.Omu.Shared.Species.Oni;
using Content.Shared._DV.Carrying;
using Content.Shared.Dataset;
using Content.Shared.Hands.Components;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.IdentityManagement;
using Content.Shared.Popups;
using Content.Shared.Prying.Components;
using Content.Shared.Zombies;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Omu.Shared.Traits.StrongArms;

public sealed class StrongArmsSystem : CommonStrongArmsSystem
{
    [Dependency] private readonly SharedHandsSystem _hands = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StrongArmsComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<CarryingComponent, ComponentStartup>(OnCarryingStartup);
        SubscribeLocalEvent<StrongArmsComponent, GetPryTimeModifierEvent>(OnAttemptPry);
    }

    private void OnCarryingStartup(Entity<CarryingComponent> ent, ref ComponentStartup args)
    {
        if (HasComp<StrongArmsComponent>(ent))
            ent.Comp.StrongCarry = true;
    }

    private void OnStartup(Entity<StrongArmsComponent> ent, ref ComponentStartup args)
    {
        EnsureComp<CommonStrongArmsComponent>(ent);
    }


    private void OnAttemptPry(EntityUid uid, StrongArmsComponent component, ref GetPryTimeModifierEvent args)
    {
        var message = _random.Pick(_proto.Index<LocalizedDatasetPrototype>(component.PryingMessage.Id).Values);

        var freeHandCount = _hands.CountFreeHands(uid);

        var text = Loc.GetString(
            message,
            ("entityIdentityName", Identity.Entity(uid, EntityManager)),
            ("handCount", freeHandCount));

        _popup.PopupEntity(text, uid, Filter.PvsExcept(uid), true);

    }
}
