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
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Gibbed.Prototype.Game
{
    internal static class Engine
    {
        private struct Native
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr LoadLibraryEx(string lpszLib, IntPtr hFile, UInt32 dwFlags);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr SetDllDirectory(string lpPathName);

            internal const UInt32 LoadWithAlteredSearchPath = 8;
        }

        private static IntPtr _Handle = IntPtr.Zero;

        private static Delegate GetExportDelegate<TDelegate>(IntPtr module, string name)
        {
            IntPtr address = Native.GetProcAddress(module, name);

            if (address == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.GetDelegateForFunctionPointer(address, typeof(TDelegate));
        }

        private static TDelegate GetExportFunction<TDelegate>(IntPtr module, string name)
            where TDelegate : class
        {
            return (TDelegate)((object)GetExportDelegate<TDelegate>(module, name));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool NativeEngineInitialize(ref InitializationSettings settings);

        private static NativeEngineInitialize _EngineInitialize;

        public static bool Initialize(InitializationSettings settings)
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            return _EngineInitialize(ref settings);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void NativeEngineTerminate();

        private static NativeEngineTerminate _EngineTerminate;

        public static void Terminate()
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _EngineTerminate();
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool NativeEngineService([MarshalAs(UnmanagedType.I1)] bool message,
                                                  [MarshalAs(UnmanagedType.I1)] bool focused);

        private static NativeEngineService _EngineService;

        public static bool Service(bool quitting, bool focused)
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            return _EngineService(quitting, focused);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void NativeEngineKeyboardService([MarshalAs(UnmanagedType.U4)] uint message,
                                                          [MarshalAs(UnmanagedType.U4)] uint virtualKeyCode,
                                                          [MarshalAs(UnmanagedType.U4)] uint flags,
                                                          [MarshalAs(UnmanagedType.I1)] bool capsLock,
                                                          [MarshalAs(UnmanagedType.I1)] bool numLock);

        private static NativeEngineKeyboardService _EngineKeyboardService;

        public static void KeyboardService(uint message,
                                           uint virtualKeyCode,
                                           uint flags,
                                           bool capsLock,
                                           bool numLock)
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _EngineKeyboardService(message, virtualKeyCode, flags, capsLock, numLock);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void NativeEngineMouseService([MarshalAs(UnmanagedType.U4)] uint message,
                                                       [MarshalAs(UnmanagedType.U4)] uint virtualKeyState,
                                                       [MarshalAs(UnmanagedType.U4)] uint coordinate);

        private static NativeEngineMouseService _EngineMouseService;

        public static void EngineMouseService(uint message,
                                              uint virtualKeyState,
                                              uint coordinate)
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _EngineMouseService(message, virtualKeyState, coordinate);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void NativeEngineWindowFocus([MarshalAs(UnmanagedType.I1)] bool focused);

        private static NativeEngineWindowFocus _EngineWindowFocus;

        public static void WindowFocus(bool focused)
        {
            if (_Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            _EngineWindowFocus(focused);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct InitializationSettings
        {
            public string CommandLine;
            public IntPtr DeviceWindow;
            public IntPtr FocusWindow;
        }

        public static string GetInstallPath()
        {
            return
                (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Prototype", "Path", null) ??
                (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Prototype", "Path", null);
        }

        public static bool Load()
        {
            if (_Handle != IntPtr.Zero)
            {
                return true;
            }

            string path = GetInstallPath();
            if (path == null)
            {
                return false;
            }

            Native.SetDllDirectory(path);

            path = Path.Combine(path, "prototypeenginef.dll");
            var module = Native.LoadLibraryEx(path, IntPtr.Zero, Native.LoadWithAlteredSearchPath);
            if (module == IntPtr.Zero)
            {
                return false;
            }

            var engineInitialize = GetExportFunction<NativeEngineInitialize>(module, "EngineInitialize");
            var engineTerminate = GetExportFunction<NativeEngineTerminate>(module, "EngineTerminate");
            var engineService = GetExportFunction<NativeEngineService>(module, "EngineService");
            var engineKeyboardService = GetExportFunction<NativeEngineKeyboardService>(module, "EngineKeyboardService");
            var engineMouseService = GetExportFunction<NativeEngineMouseService>(module, "EngineMouseService");
            var engineWindowFocus = GetExportFunction<NativeEngineWindowFocus>(module, "EngineWindowFocus");

            if (engineInitialize == null ||
                engineTerminate == null ||
                engineService == null ||
                engineKeyboardService == null ||
                engineMouseService == null ||
                engineWindowFocus == null)
            {
                // todo: free library?
                return false;
            }

            _Handle = module;
            _EngineInitialize = engineInitialize;
            _EngineTerminate = engineTerminate;
            _EngineService = engineService;
            _EngineKeyboardService = engineKeyboardService;
            _EngineMouseService = engineMouseService;
            _EngineWindowFocus = engineWindowFocus;
            return true;
        }
    }
}
