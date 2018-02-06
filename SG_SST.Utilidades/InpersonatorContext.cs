using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Utilidades
{
    class InpersonatorContext
    {
        public class ImpersonatorContext
        {
            // constants from winbase.h
            public const int LOGON32_LOGON_INTERACTIVE = 2;
            public const int LOGON32_LOGON_NETWORK = 3;
            public const int LOGON32_LOGON_BATCH = 4;
            public const int LOGON32_LOGON_SERVICE = 5;
            public const int LOGON32_LOGON_UNLOCK = 7;
            public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
            public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

            public const int LOGON32_PROVIDER_DEFAULT = 0;
            public const int LOGON32_PROVIDER_WINNT35 = 1;
            public const int LOGON32_PROVIDER_WINNT40 = 2;
            public const int LOGON32_PROVIDER_WINNT50 = 3;

            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern int LogonUserA(String lpszUserName,
                String lpszDomain,
                String lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);
            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int DuplicateToken(IntPtr hToken,
                int impersonationLevel,
                ref IntPtr hNewToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool RevertToSelf();

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern bool CloseHandle(IntPtr handle);

            public static WindowsImpersonationContext LogOn(string userName, string password)
            {
                return LogOn(userName, password, "");
            }

            public static WindowsImpersonationContext LogOn(string userName, string password, string domain)
            {
                WindowsIdentity tempWindowsIdentity;
                WindowsImpersonationContext impersonationContext;
                IntPtr token = IntPtr.Zero;
                IntPtr tokenDuplicate = IntPtr.Zero;

                if (RevertToSelf())
                {
                    if (LogonUserA(userName, domain, password, LOGON32_LOGON_NEW_CREDENTIALS,
                        LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                            if (impersonationContext != null)
                            {
                                CloseHandle(token);
                                CloseHandle(tokenDuplicate);
                                return impersonationContext;
                            }
                        }
                    }
                    else
                    {
                        var win32 = new Win32Exception(Marshal.GetLastWin32Error());
                        throw new Exception(win32.Message);
                    }
                }
                if (token != IntPtr.Zero)
                    CloseHandle(token);
                if (tokenDuplicate != IntPtr.Zero)
                    CloseHandle(tokenDuplicate);
                return null; // Failed to impersonate
            }

            public static bool LogOff(WindowsImpersonationContext context)
            {
                bool result = false;
                try
                {
                    if (context != null)
                    {
                        context.Undo();
                        context = null;
                        result = true;
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

        public class Impersonator :
            IDisposable
        {
            #region Public methods.
            // ------------------------------------------------------------------

            /// <summary>
            /// Constructor. Starts the impersonation with the given credentials.
            /// Please note that the account that instantiates the Impersonator class
            /// needs to have the 'Act as part of operating system' privilege set.
            /// </summary>
            /// <param name="userName">The name of the user to act as.</param>
            /// <param name="domainName">The domain name of the user to act as.</param>
            /// <param name="password">The password of the user to act as.</param>
            public Impersonator(
                string userName,
                string domainName,
                string password)
            {
                ImpersonateValidUser(userName, domainName, password);
            }

            // ------------------------------------------------------------------
            #endregion

            #region IDisposable member.
            // ------------------------------------------------------------------

            public void Dispose()
            {
                UndoImpersonation();
            }

            // ------------------------------------------------------------------
            #endregion

            #region P/Invoke.
            // ------------------------------------------------------------------

            [DllImport("advapi32.dll", SetLastError = true)]
            private static extern int LogonUser(
                string lpszUserName,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int DuplicateToken(
                IntPtr hToken,
                int impersonationLevel,
                ref IntPtr hNewToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool RevertToSelf();

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern bool CloseHandle(
                IntPtr handle);

            private const int LOGON32_LOGON_INTERACTIVE = 2;
            private const int LOGON32_PROVIDER_DEFAULT = 0;

            // ------------------------------------------------------------------
            #endregion

            #region Private member.
            // ------------------------------------------------------------------

            /// <summary>
            /// Does the actual impersonation.
            /// </summary>
            /// <param name="userName">The name of the user to act as.</param>
            /// <param name="domainName">The domain name of the user to act as.</param>
            /// <param name="password">The password of the user to act as.</param>
            private void ImpersonateValidUser(
                string userName,
                string domain,
                string password)
            {
                WindowsIdentity tempWindowsIdentity = null;
                IntPtr token = IntPtr.Zero;
                IntPtr tokenDuplicate = IntPtr.Zero;

                try
                {
                    if (RevertToSelf())
                    {
                        if (LogonUser(
                            userName,
                            domain,
                            password,
                            LOGON32_LOGON_INTERACTIVE,
                            LOGON32_PROVIDER_DEFAULT,
                            ref token) != 0)
                        {
                            if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                            {
                                tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                                impersonationContext = tempWindowsIdentity.Impersonate();
                            }
                            else
                            {
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                finally
                {
                    if (token != IntPtr.Zero)
                    {
                        CloseHandle(token);
                    }
                    if (tokenDuplicate != IntPtr.Zero)
                    {
                        CloseHandle(tokenDuplicate);
                    }
                }
            }

            /// <summary>
            /// Reverts the impersonation.
            /// </summary>
            private void UndoImpersonation()
            {
                if (impersonationContext != null)
                {
                    impersonationContext.Undo();
                }
            }

            private WindowsImpersonationContext impersonationContext = null;

            // ------------------------------------------------------------------
            #endregion
        }
    }
}
