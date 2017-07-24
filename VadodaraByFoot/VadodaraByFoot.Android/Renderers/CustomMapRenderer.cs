using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using Android.Widget;

using VadodaraByFoot.CustomControls;
using VadodaraByFoot.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace VadodaraByFoot.Droid.Renderers
{
	public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback 
	{
		List<Pin> customPins;
		bool isDrawn;

		GoogleMap map;
		List<Position> routeCoordinates;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            ((MapView)Control).NestedScrollingEnabled = true;
            ((MapView)Control).VerticalScrollBarEnabled = true;
            if (e.OldElement != null)
            {
                //	NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                routeCoordinates = CustomMap.RouteCoordinates;
                customPins = new List<Pin>(formsMap.Pins);
                ((MapView)Control).GetMapAsync(this);
                ((MapView)Control).NestedScrollingEnabled = true;
                ((MapView)Control).VerticalScrollBarEnabled = true;
            }
            ((MapView)Control).NestedScrollingEnabled = true;
           
		}
     /*   public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            return false;
        }*/
       
		public void OnMapReady(GoogleMap googleMap)
		{
			NativeMap = googleMap;

			DrawLineBetweenPins();

		//	NativeMap.InfoWindowClick += OnInfoWindowClick;
		//	NativeMap.SetInfoWindowAdapter(this);
			NativeMap.UiSettings.ZoomControlsEnabled = Map.HasZoomEnabled;
			NativeMap.UiSettings.ZoomGesturesEnabled = Map.HasZoomEnabled;
			NativeMap.UiSettings.ScrollGesturesEnabled = Map.HasScrollEnabled;

		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			/*   if (e.PropertyName.Equals("Height"))
			   {
				   System.Threading.Thread.Sleep(5000);
			   }*/
        //    DrawLineBetweenPins();
			if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
			{
			//	NativeMap.Clear();
			//	DrawLineBetweenPins();
				isDrawn = true;
			}
		}
		public void DrawLineBetweenPins()
		{
			#region Bind Pin Numbers
			for (int i = 0; i < customPins.Count; i++)
			{
				var marker = new MarkerOptions();
				marker.SetPosition(new LatLng(customPins[i].Position.Latitude, customPins[i].Position.Longitude));

				if (i == 0)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_1));
				else if (i == 1)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_2));
				else if (i == 2)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_3));
				else if (i == 3)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_4));
				else if (i == 4)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_5));
				else if (i == 5)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_6));
				else if (i == 6)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_7));
				else if (i == 7)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_8));
				else if (i == 8)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_9));
				else if (i == 9)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_10));
				else if (i == 10)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_11));
				else if (i == 11)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_12));
				else if (i == 12)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_11));
				else if (i == 13)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_12));
				else if (i == 14)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_13));
				else if (i == 15)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_14));
				else if (i == 16)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_15));
				else if (i == 17)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_16));
				else if (i == 18)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_17));
				else if (i == 19)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_18));
				else if (i == 20)
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_19));
				else
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_20));

				marker.SetTitle(customPins[i].Label);
				marker.SetSnippet(customPins[i].Address);
				//marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.route_pin_1));

				NativeMap.AddMarker(marker);
			}
			#endregion 
			var polylineOptions = new PolylineOptions();
			polylineOptions.InvokeColor(0x662873DD);
			polylineOptions.InvokeZIndex(5);
			polylineOptions.InvokeWidth(7);

			foreach (var position in routeCoordinates)
			{
				polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
			}

			NativeMap.AddPolyline(polylineOptions);
		}
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			if (changed)
			{
				isDrawn = false;
			}
		}
	
	    #region On Pin tap Events View methods
		void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
		/*	var customPin = GetCustomPin(e.Marker);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}*/

			/*if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var url = Android.Net.Uri.Parse(customPin.Url);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }*/
		}

		/*public Android.Views.View GetInfoContents(Marker marker)
		{
			var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
			if (inflater != null)
			{
				Android.Views.View view;

				var customPin = GetCustomPin(marker);
				if (customPin == null)
				{
					throw new Exception("Custom pin not found");
				}
                view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
				if (infoTitle != null)
				{
					infoTitle.Text = marker.Title;
				}
				return view;
			}
			return null;
		}

		public Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}

		Pin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
			foreach (var pin in customPins)
			{
				if (pin.Position == position)
				{
					return pin;
				}
			}
			return null;
		}*/
		#endregion
    
	}
}