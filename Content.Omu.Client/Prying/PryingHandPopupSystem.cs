using Content.Shared.Hands.Components;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Popups;
using Content.Shared.Prying.Components;

namespace Content.Omu.Client.Prying;

public sealed class PryingHandPopupSystem : EntitySystem
{

    [Dependency] private readonly SharedHandsSystem _hands = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HandsComponent, GetPryTimeModifierEvent>(OnPry);
    }

    private void OnPry(EntityUid user, HandsComponent hands, ref GetPryTimeModifierEvent args)
    {
        if (!_hands.ActiveHandIsEmpty(user))
            return;

        _popup.PopupClient(
            Loc.GetString(hands.HandPryMessage, ("freeHandCount", _hands.CountFreeHands(user))),
            user,
            user);
    }
}
