using Microsoft.Phone.Controls;
using System.Windows;

namespace Weather.WP
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }        
    }
}