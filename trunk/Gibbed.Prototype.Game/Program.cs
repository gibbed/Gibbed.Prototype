/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SlimDX.Windows;

namespace Gibbed.Prototype.Game
{
    internal class Program
    {
        private static uint ToCoordinates(MouseEventArgs me)
        {
            return (uint)((me.X & 0xFFFF) | ((me.Y & 0xFFFF) << 16));
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var image = new Bitmap(1, 1);

            var form = new RenderForm("[prototype]")
            {
                Cursor = new Cursor(image.GetHicon()),
            };

            Engine.InitializationSettings settings;
            settings.CommandLine = " Windowed";
            settings.DeviceWindow = IntPtr.Zero;
            settings.FocusWindow = form.Handle;

            Directory.SetCurrentDirectory(Engine.GetInstallPath());

            if (Engine.Load() == false)
            {
                MessageBox.Show("Failed to load engine.");
                return;
            }

            if (Engine.Initialize(settings) == false)
            {
                MessageBox.Show("Failed to initialize engine.");
            }

            bool formIsClosed = false;
            form.FormClosed += (a, b) => { formIsClosed = true; };

            bool mouseInForm = false;
            form.MouseEnter += (a, b) => { mouseInForm = true; };
            form.MouseLeave += (a, b) => { mouseInForm = false; };

            form.MouseMove += (a, b) =>
            {
                if (mouseInForm == false)
                {
                    return;
                }

                Engine.EngineMouseService(0x200, 0, ToCoordinates(b));
            };

            form.MouseDown += (a, b) =>
            {
                if ((b.Button & MouseButtons.Left) != 0)
                {
                    Engine.EngineMouseService(0x201, 0, ToCoordinates(b));
                }

                if ((b.Button & MouseButtons.Right) != 0)
                {
                    Engine.EngineMouseService(0x204, 0, ToCoordinates(b));
                }

                if ((b.Button & MouseButtons.Middle) != 0)
                {
                    Engine.EngineMouseService(0x207, 0, ToCoordinates(b));
                }
            };

            form.MouseUp += (a, b) =>
            {
                if ((b.Button & MouseButtons.Left) != 0)
                {
                    Engine.EngineMouseService(0x202, 0, ToCoordinates(b));
                }

                if ((b.Button & MouseButtons.Right) != 0)
                {
                    Engine.EngineMouseService(0x205, 0, ToCoordinates(b));
                }

                if ((b.Button & MouseButtons.Middle) != 0)
                {
                    Engine.EngineMouseService(0x208, 0, ToCoordinates(b));
                }
            };

            form.MouseWheel += (a, b) =>
            {
                if (mouseInForm == false)
                {
                    return;
                }

                Engine.EngineMouseService(0x20A, (uint)(((b.Delta & 0xFFFF) << 16) | 0), ToCoordinates(b));
            };

            form.GotFocus += (a, b) => Engine.WindowFocus(true);

            form.LostFocus += (a, b) => Engine.WindowFocus(false);

            MessagePump.Run(form,
                            () =>
                            {
                                if (formIsClosed == true)
                                {
                                    return;
                                }

                                var running = Engine.Service(false, form.Focused == false);
                                if (running == false)
                                {
                                    form.Close();
                                }
                            });
        }
    }
}
