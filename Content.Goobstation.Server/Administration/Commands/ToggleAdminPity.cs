using Content.Server.Administration;
using Content.Server.GameTicking;
using Content.Shared.Administration;
using Content.Shared.GameTicking;
using Robust.Server.Player;
using Robust.Shared.Console;


namespace Content.Goobstation.Server.Administration.Commands
{
    [AdminCommand(AdminFlags.Spawn)]
    public sealed class ToggleAdminPityCommand : IConsoleCommand
    {
        public string Command => "toggleadminpity";
        public string Description => Loc.GetString("adminpity-command-description");
        public string Help =>  Loc.GetString("adminpity-command-help");

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var _entityManager = IoCManager.Resolve<IEntityManager>();
            var _playerManager = IoCManager.Resolve<IPlayerManager>();
            var ticker = _entityManager.System<GameTicker>();
            if (shell.Player == null)
                return;
            switch (args.Length)
            {
                case 0: // Toggle to opposite state
                {
                    if (!ticker.AdminPityStatuses.TryGetValue(shell.Player.UserId, out var pityStatus))
                        pityStatus = AdminPityStatus.Disabled; // default for first-time toggle

                    if (pityStatus == AdminPityStatus.Unavailable)
                    {
                        shell.WriteError(Loc.GetString("adminpity-command-used-while-unavailable"));
                        return;
                    }

                    ticker.ToggleAdminPity(shell.Player, pityStatus != AdminPityStatus.Enabled);
                    return;
                }
                case > 1: // Set state
                    shell.WriteError(Loc.GetString("shell-wrong-arguments-number"));
                    return;
            }

            ticker.ToggleAdminPity(shell.Player, bool.Parse(args[0]));
        }
    }
}
