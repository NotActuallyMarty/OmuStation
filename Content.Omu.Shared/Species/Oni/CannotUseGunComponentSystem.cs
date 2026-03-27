using System.Linq;
using Content.Shared.Dataset;
using Content.Shared.Hands;
using Content.Shared.Humanoid;
using Content.Shared.Popups;
using Content.Shared.Tag;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Events;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Omu.Shared.Species.Oni;

public sealed class CannotUseGunComponentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly TagSystem _tag = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CannotUseGunComponentComponent, ShotAttemptedEvent>(OnAttemptGunshot);
        SubscribeLocalEvent<CannotUseGunComponentComponent, DidEquipHandEvent>(OnPickUpGunComponentItem);
    }

    private void OnPickUpGunComponentItem(Entity<CannotUseGunComponentComponent> ent, ref DidEquipHandEvent args)
    {
        if (!HasComp<GunComponent>(args.Equipped)) // Bad. Oh well.
            return;

        var messages = _proto.Index<LocalizedDatasetPrototype>(ent.Comp.PickUpGunMessage.Id);

        var time = _timing.CurTime;

        if (time > ent.Comp.LastPopup
            + TimeSpan.FromSeconds(0.5)
           )
        {
            ent.Comp.LastPopup = time;
            _popup.PopupClient(Loc.GetString(_random.Pick(messages.Values)), args.User);
        }
    }

    private void OnAttemptGunshot(Entity<CannotUseGunComponentComponent> ent, ref ShotAttemptedEvent args)
    {
        if (ent.Owner != args.User || !TryComp<HumanoidAppearanceComponent>(args.User, out var appearanceComp))
            return;

        if (_tag.HasTag(args.Used, ent.Comp.NotAGunTag))
            return;

        var currentSpecies = appearanceComp.Species;
        var originalSpecies = ent.Comp.Species;

        if (currentSpecies == originalSpecies || ent.Comp.RespectOriginalSpecies)
            CancelGunshot(ent, ref args);

    }

    private void CancelGunshot(Entity<CannotUseGunComponentComponent> ent, ref ShotAttemptedEvent args)
    {
        var messages = _proto.Index<LocalizedDatasetPrototype>(ent.Comp.UsingGunMessage.Id);
        var time = _timing.CurTime;
        var chosenMessage = Loc.GetString(_random.Pick(messages.Values));

        if (time > ent.Comp.LastPopup
            + TimeSpan.FromSeconds(0.25)
            )
        {
            ent.Comp.LastPopup = time;
            _popup.PopupClient(chosenMessage, args.User);
        }
        args.Cancel();
    }
}
