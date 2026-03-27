using System.Linq;
using Content.Omu.Shared.Species.Oni;
using Content.Server.Mind;
using Content.Server.Popups;
using Content.Shared.CombatMode;
using Content.Shared.Humanoid;
using Content.Shared.Mind;
using Robust.Server.GameObjects;
using Robust.Shared.Player;

namespace Content.Omu.Server.Species.Oni;

public sealed class OniWeaponProficiencySystem : SharedOniWeaponProficiencySystem
{
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly TransformSystem _xform = default!;

    public override void Initialize()
    {
        //SubscribeLocalEvent<OniWeaponProficiencyComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<HumanoidAppearanceComponent, ToggleCombatActionEvent>(OnCombatToggle);
    }

    // todo unshitcode this and move to mind proper.
    /*private void OnStartup(EntityUid uid, OniWeaponProficiencyComponent component, ComponentStartup args)
    {
        // lwk this is shit req changes for me to do something better if this makes it into the final PR.
        if (!TryComp<MindComponent>(uid, out var mind))
        {
            _mind.TryGetMind(uid, out var mindId, out _);
            CopyComp(uid, mindId, component);
            RemCompDeferred<OniWeaponProficiencyComponent>(uid);
            return;
        }
    }*/

    private void OnCombatToggle(EntityUid uid, HumanoidAppearanceComponent component, ToggleCombatActionEvent args)
    {
        var mapCoords = _xform.GetMapCoordinates(uid);

        var entities = new HashSet<Entity<OniWeaponProficiencyComponent>>();
        _lookup.GetEntitiesInRange(mapCoords, 7f, entities, LookupFlags.Uncontained);

        var filter = Filter.Empty()
            .AddWhereAttachedEntity(e =>
            {
                return entities.Any(ent =>
                    ent.Owner == e
                    && ent.Comp.FightingInstinct);
            });

        if (filter.Count == 0)
            return;

        _popup.PopupEntity("Your instincts warn you of danger.", uid, filter, true);
    }
}
