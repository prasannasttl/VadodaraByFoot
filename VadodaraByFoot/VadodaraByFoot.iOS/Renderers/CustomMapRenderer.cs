using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using MapKit;


using UIKit;
using VadodaraByFoot.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using VadodaraByFoot.iOS.Renderers;
using VadodaraByFoot.ServiceLayer.ServiceClass;

[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace VadodaraByFoot.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        MKPolylineRenderer polylineRenderer;
        UITapGestureRecognizer _tapRecogniser;
        MKMapView nativeMap;
        MKPolyline routeOverlay;
        Position Origin = CustomMap.RouteCoordinates[CustomMap.RouteCoordinates.Count - 1];

        CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[CustomMap.RouteCoordinates.Count];
        CLLocationCoordinate2D[] newRouteCoordinates;

        public List<Position> newLatlng = new List<Position>();
        // public static List<MKMapPoint> newRouteCoordinates = new MKMapPoint;
       
        #region Variables For Different image on all the pins
        UIView customPinView;
        List<Pin> customPins;
		public int PinImageBindCount = 1;
        #endregion

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap = Control as MKMapView;
                nativeMap.OverlayRenderer = null;
                #region For different pin images
                nativeMap.GetViewForAnnotation = null;
                nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                #endregion
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MKMapView;
                customPins = new List<Pin>(formsMap.Pins);

                nativeMap.OverlayRenderer = GetOverlayRenderer;
                int index = 0;
                foreach (var position in CustomMap.RouteCoordinates)
                {
                    coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                    index++;
                }

                routeOverlay = MKPolyline.FromCoordinates(coords);

                nativeMap.AddOverlay(routeOverlay);
                nativeMap.ScrollEnabled = true;
                #region For different pin images
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
                #endregion
            }

        }

        private void AddtoPolylineCoordinates(List<Position> TempNewLatLong)
        {
            newRouteCoordinates = new CLLocationCoordinate2D[TempNewLatLong.Count];

            int index = 0;
            foreach (var Item in TempNewLatLong)
            {
                newRouteCoordinates[index] = new CLLocationCoordinate2D(Item.Latitude, Item.Longitude);
                index++;
            }

            nativeMap.OverlayRenderer = GetOverlayRenderer;

            var NewOverlay = MKPolyline.FromCoordinates(newRouteCoordinates);

            nativeMap.AddOverlay(NewOverlay);
        }

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            #region for dotted routing between pins
            /*	var temp = new Foundation.NSNumber[2];
				temp[0] = 2;
				temp[1] = 10;

				polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline);
				polylineRenderer.FillColor = UIColor.Blue;
				polylineRenderer.StrokeColor = UIColor.Red;
				polylineRenderer.LineWidth = 3;
				polylineRenderer.Alpha = 0.4f;

				polylineRenderer.LineDashPhase = 2;
				polylineRenderer.LineDashPattern = temp;
				polylineRenderer.LineJoin = CGLineJoin.Round;

				return polylineRenderer;*/
            #endregion
            #region for line drawing routing between pins
            if (polylineRenderer == null)
            {
                polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline);
                polylineRenderer.FillColor = UIColor.Blue;
                polylineRenderer.StrokeColor = UIColor.FromRGB(40, 115, 221);
                polylineRenderer.LineWidth = 3;
                polylineRenderer.Alpha = 0.4f;
            }
            return polylineRenderer;
            #endregion
        }


		#region For different images on pins and pin content view methods

		MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

			if (annotation is MKUserLocation)
				return null;

			var anno = annotation as MKPointAnnotation;
			var customPin = GetCustomPin(anno);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}

			annotationView = mapView.DequeueReusableAnnotation("xamarin");
			if (annotationView == null)
			{
				annotationView = new CustomMKAnnotationView(annotation,"xamarin");
				annotationView.Image = UIImage.FromBundle("route_pin_1.png");


				//annotationView.Image = UIImage.FromBundle("route_pin_"+PinImageBindCount.ToString()+".png");
                PinImageBindCount++;
				annotationView.CalloutOffset = new CGPoint(0, 0);
				//annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("route_scroller_sel.png"));
				//annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
				((CustomMKAnnotationView)annotationView).Id ="xamarin";
				//((CustomMKAnnotationView)annotationView).Url = customPin.Url;
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}
		void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
		{
		    /*var customView = e.View as CustomMKAnnotationView;
			if (!string.IsNullOrWhiteSpace(customView.Url))
			{
				UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
			}*/
		}

		void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			var customView = e.View as CustomMKAnnotationView;
			customPinView = new UIView();
            if (customView.Id == "xamarin")
            {
                customPinView.Frame = new CGRect(0, 0, 200, 84);
                //var image = new UIImageView(new CGRect(0, 0, 200, 84));
                //image.Image = UIImage.FromFile("xamarin.png");
                //customPinView.AddSubview(image);
                customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
                e.View.AddSubview(customPinView);
            }
		}

		void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			if (!e.View.Selected)
			{
				customPinView.RemoveFromSuperview();
				customPinView.Dispose();
				customPinView = null;
			}
		}

		Pin GetCustomPin(MKPointAnnotation annotation)
		{
			var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
			foreach (var pin in customPins)
			{
				if (pin.Position == position)
				{
					return pin;
				}
			}
			return null;
		}
        #endregion
    }
}
            
