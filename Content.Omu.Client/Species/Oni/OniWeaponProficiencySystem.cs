using Content.Goobstation.Common.MartialArts;
using Content.Goobstation.Shared.MartialArts.Components;
using Content.Omu.Shared.Species.Oni;
using Content.Shared.CombatMode;
using Content.Shared.Examine;
using Content.Shared.Popups;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Omu.Client.Species.Oni;

public abstract class OniWeaponProficiencySystem : SharedOniWeaponProficiencySystem // remember to make client dumbass
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly TagSystem _tag = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<OniWeaponProficiencyComponent, ExaminedEvent>(OnExamineOni);
        SubscribeLocalEvent<GrantMartialArtKnowledgeComponent, ExaminedEvent>(OnExamineMartial);
    }

    private void OnExamineOni(EntityUid ent, OniWeaponProficiencyComponent comp, ref ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        if (!TryComp<OniWeaponProficiencyComponent>(args.Examiner, out var examinerComp)
            || !examinerComp.IsFightingExpert)
            return;

        var (key, color) = comp.Proficiency switch
        {
            OniWeaponProficiencyComponent.ProficiencyType.Brawler =>
                ("oni-fighting-style-brawler", "orange"),

            OniWeaponProficiencyComponent.ProficiencyType.SwashBuckler =>
                ("oni-fighting-style-swashbuckler", "cyan"),

            OniWeaponProficiencyComponent.ProficiencyType.SpearMaster =>
                ("oni-fighting-style-spearmaster", "lightgreen"),

            OniWeaponProficiencyComponent.ProficiencyType.WallBreaker =>
                ("oni-fighting-style-wallsmasher", "khaki"),

            OniWeaponProficiencyComponent.ProficiencyType.Shunned =>
                ("oni-fighting-style-falseoni", "gray"),

            _ => (null, null),
        };

        if (key != null && color != null)
            args.PushMarkup(Loc.GetString(key, ("color", color)));
    }

    private void OnExamineMartial(EntityUid ent, GrantMartialArtKnowledgeComponent comp, ref ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        if (
            !TryComp<OniWeaponProficiencyComponent>(args.Examiner, out var examinerComp)
            || !TryComp<CombatModeComponent>(args.Examined, out var combatModeComp)
            || !combatModeComp.IsInCombatMode // stance up if you want them to see that shit
            || !examinerComp.IsFightingExpert
            || !examinerComp.CanSeeOtherMartialArts)
            return;

        var key = comp.MartialArtsForm switch
        {
            MartialArtsForms.CorporateJudo => "oni-martial-art-corporate-judo",
            MartialArtsForms.CloseQuartersCombat => "oni-martial-art-cqc",
            MartialArtsForms.SleepingCarp => "oni-martial-art-sleeping-carp",
            MartialArtsForms.Capoeira => "oni-martial-art-capoeira",
            MartialArtsForms.KungFuDragon => "oni-martial-art-kung-fu-dragon",
            MartialArtsForms.Ninjutsu => "oni-martial-art-ninjutsu",
            MartialArtsForms.HellRip => "oni-martial-art-hell-rip",
            _ => null,
        };

        if (key == null)
            return;

        args.PushMarkup($"[color=orange]{Loc.GetString(key)}[/color]");
    }
}
