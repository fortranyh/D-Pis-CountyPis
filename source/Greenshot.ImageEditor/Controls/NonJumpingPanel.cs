/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2013  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/// <summary>
/// See: http://nickstips.wordpress.com/2010/03/03/c-panel-resets-scroll-position-after-focus-is-lost-and-regained/
/// </summary>

using System.Drawing;
using System.Windows.Forms;

namespace GreenshotPlugin.Controls
{
    internal class NonJumpingPanel : Panel
    {
        protected override Point ScrollToControl(Control activeControl)
        {
            // Returning the current location prevents the panel from
            // scrolling to the active control when the panel loses and regains focus
            return DisplayRectangle.Location;
        }
    }
}