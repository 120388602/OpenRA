#region Copyright & License Information
/*
 * Copyright 2007-2020 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Linq;
using OpenRA.Graphics;
using OpenRA.Primitives;

namespace OpenRA.Traits
{
	[Desc("Add this to the Player actor definition.")]
	public class PlayerHighlightPaletteInfo : ITraitInfo
	{
		[PaletteDefinition(true)]
		[Desc("The prefix for the resulting player palettes")]
		public readonly string BaseName = "highlight";

		[Desc("Index set to be fully transparent/invisible.")]
		public readonly int TransparentIndex = 0;

		public object Create(ActorInitializer init) { return new PlayerHighlightPalette(this); }
	}

	public class PlayerHighlightPalette : ILoadsPlayerPalettes
	{
		readonly PlayerHighlightPaletteInfo info;

		public PlayerHighlightPalette(PlayerHighlightPaletteInfo info)
		{
			this.info = info;
		}

		public void LoadPlayerPalettes(WorldRenderer wr, string playerName, Color color, bool replaceExisting)
		{
			var argb = (uint)Color.FromArgb(128, color).ToArgb();
			var pal = new ImmutablePalette(Enumerable.Range(0, Palette.Size).Select(i => i == info.TransparentIndex ? 0 : argb));
			wr.AddPalette(info.BaseName + playerName, pal, false, replaceExisting);
		}
	}
}
