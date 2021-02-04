using System;
// using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

using ContentPatcher;

namespace SDVTest
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var api = this.Helper.ModRegistry.GetApi<IContentPatcherAPI>("Pathoschild.ContentPatcher");

            api.RegisterToken(this.ModManifest, "Luck", () => {
                if (Context.IsWorldReady)
                {
                    return new[] { EvaluateLuck(Game1.player.DailyLuck) };
                }
                else
                {
                    return null;
                }
            });
        }

        private string EvaluateLuck(double luck)
        {
            if (luck > 0.07)
            {
                return "best";
            }
            else if (luck > 0.02)
            {
                return "good";
            }
            else if (luck >= -0.02)
            {
                return "neutral";
            }
            else if (luck >= -0.07)
            {
                return "bad";
            }
            else
            {
                return "worst";
            }
        }
    }
}
