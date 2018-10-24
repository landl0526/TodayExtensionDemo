using System;

using NotificationCenter;
using Foundation;
using Social;
using UIKit;
using CoreGraphics;
using CoreLocation;
using System.Linq;

namespace TodayExtensionDemo.Today
{
    public partial class TodayViewController : SLComposeServiceViewController, INCWidgetProviding
    {
        public TodayViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        CLLocationManager locationManager;

        string labelString;
        public string LabelString
        {
            set
            {
                labelString = value;
                MyLabel.Text = value;
            }
            get
            {
                return labelString;
            }
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            locationManager = new CLLocationManager();
            locationManager.RequestAlwaysAuthorization();
            locationManager.Delegate = new LocationDelegate(this);
            locationManager.StartUpdatingLocation();
        }

        public void WidgetPerformUpdate(Action<NCUpdateResult> completionHandler)
        {
            // Perform any setup necessary in order to update the view.

            // If an error is encoutered, use NCUpdateResultFailed
            // If there's no update required, use NCUpdateResultNoData
            // If there's an update, use NCUpdateResultNewData

            completionHandler(NCUpdateResult.NewData);
        }
    }

    public class LocationDelegate: CLLocationManagerDelegate
    {
        TodayViewController viewController;

        public LocationDelegate(TodayViewController viewController)
        {
            this.viewController = viewController;
        }
        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            CLLocation location = locations.FirstOrDefault();

            var latitude = location.Coordinate.Latitude;
            var longitude = location.Coordinate.Longitude;

            viewController.LabelString = "latitude:" + latitude + "\n" + "longitude:" + longitude;
        }
    }
}
