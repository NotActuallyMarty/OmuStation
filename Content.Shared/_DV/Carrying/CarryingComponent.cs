// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Omu.Common.Traits.StrongArms;
using Robust.Shared.GameStates;

namespace Content.Shared._DV.Carrying;

/// <summary>
/// Added to an entity when they are carrying somebody.
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(CarryingSystem),
     typeof(CommonStrongArmsSystem) // Omu for onehand Carry
 )]
[AutoGenerateComponentState]
public sealed partial class CarryingComponent : Component
{
    [DataField, AutoNetworkedField]
    public EntityUid Carried;

    [DataField, AutoNetworkedField] // Omu for single-hand carry
    public bool StrongCarry;
}
