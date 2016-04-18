using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NXOpen;
using NXOpen.Utilities;
using NXOpen.UF;
using test;

namespace test
{
    public class Program
    {
        // class members
        static Session theSession = null;
        static UFSession theUFSession = null;
        static ListingWindow lw = null;
        static UI theUI = UI.GetUI();
        public static bool isDisposeCalled;

        //------------------------------------------------------------------------------
        // Callback Name: ApplicationInit
        //    Executed when the application is entered.  
        //    It's used to initialize the application's data.
        //
        //    Prints a note to the listing window.
        //------------------------------------------------------------------------------
        public static int ApplicationInit()
        {
            // ---- Enter your callback code here -----
            lw.Open();
            lw.WriteLine(" ");
            lw.WriteLine("Inside My CSharp Init Callback");
            lw.WriteLine(" ");
            return 0;
        }

        //------------------------------------------------------------------------------
        // Callback Name: ApplicationEnter
        //    Executed when the application is selected 
        //    (activated) from the pulldown menu.  
        //    If this is the first time entering the application,
        //    the ApplicationInit callback will get called first to
        //    initialize any data required by this callback.
        //
        //    Prints a note to the listing window.
        //------------------------------------------------------------------------------
        public static int ApplicationEnter()
        {
            // ---- Enter your callback code here -----
            lw.Open();
            lw.WriteLine(" ");
            lw.WriteLine("Inside My CSharp Enter Callback");
            lw.WriteLine(" ");
            return 0;
        }

        //------------------------------------------------------------------------------
        // Callback Name: ApplicationExit
        //    Executed when the application is exited.  
        //    It's used to free (clean up) the application's data.
        //
        //    Prints a note to the listing window.
        //------------------------------------------------------------------------------
        public static int ApplicationExit()
        {
            // ---- Enter your callback code here -----
            lw.Open();
            lw.WriteLine(" ");
            lw.WriteLine("Inside My CSharp Exit Callback");
            lw.WriteLine(" ");
            return 0;
        }

        //------------------------------------------------------------------------------
        // Callback Name: PrintButtonIdCB
        //   This is a callback method associated with SAMPLE_CSHARP_APP__action1 action button.
        //   This will get executed whenever 'Print Button ID' is selected from the
        //   'Sample C Sharp' menu.
        //   Given a button name (such as SAMPLE_CSHARP_APP_BUTTON1) from the user, the
        //   input button's id gets printed to the listing window.
        //------------------------------------------------------------------------------
        public static NXOpen.MenuBar.MenuBarManager.CallbackStatus StarkTestFunction(NXOpen.MenuBar.MenuButtonEvent buttonEvent)
        {
            CBFunctions.theUFSession = theUFSession;
            CBFunctions.lw = lw;
            CBFunctions.customizedCBFunction();

            return NXOpen.MenuBar.MenuBarManager.CallbackStatus.Continue;
        }

        //------------------------------------------------------------------------------
        //  NX Startup
        //      This entry point registers the application at NX startup
        //------------------------------------------------------------------------------
        public static int Startup()
        {
            int retValue = 0;
            try
            {
                if (theSession == null)
                {
                    theSession = Session.GetSession();
                }
                if (theUFSession == null)
                {
                    theUFSession = UFSession.GetUFSession();
                }
                if (lw == null)
                {
                    lw = theSession.ListingWindow;
                }
                theUI.MenuBarManager.RegisterApplication("Stark_Test_App",
                new NXOpen.MenuBar.MenuBarManager.InitializeMenuApplication(ApplicationInit),
                new NXOpen.MenuBar.MenuBarManager.EnterMenuApplication(ApplicationEnter),
                new NXOpen.MenuBar.MenuBarManager.ExitMenuApplication(ApplicationExit), true, true, true);
                theUI.MenuBarManager.AddMenuAction("testAction", new NXOpen.MenuBar.MenuBarManager.ActionCallback(StarkTestFunction));
            }
            catch (NXException ex)
            {
                // ---- Enter your exception handling code here -----
            }
            return retValue;
        }

        //------------------------------------------------------------------------------
        // Following method disposes all the class members
        //------------------------------------------------------------------------------
        public void Dispose()
        {
            try
            {
                if (isDisposeCalled == false)
                {
                    //TODO: Add your application code here 
                }
                isDisposeCalled = true;
            }
            catch (NXException ex)
            {
                // ---- Enter your exception handling code here -----

            }
        }

        //------------------------------------------------------------
        //
        //  GetUnloadOption()
        //
        //     Used to tell NX when to unload this library
        //
        //     Available options include:
        //       Session.LibraryUnloadOption.Immediately
        //       Session.LibraryUnloadOption.Explicitly
        //       Session.LibraryUnloadOption.AtTermination
        //
        //     Any programs that register callbacks must use 
        //     AtTermination as the unload option.
        //------------------------------------------------------------
        public static int GetUnloadOption(string arg)
        {
            //Unloads the image explicitly, via an unload dialog
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

            //Unloads the image immediately after execution within NX
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

            //Unloads the image when the NX session terminates
            return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }
    }
}
