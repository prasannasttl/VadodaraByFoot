using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace VadodaraByFoot.CustomControls
{
	public class CustomMap : Map
	{
		public static List<Position> RouteCoordinates = new List<Position>();

		public CustomMap()
		{
		}

		/// <summary>
		/// Event thrown when the user taps on the map
		/// </summary>
		public event EventHandler<MapTapEventArgs> Tapped;


		/// <summary>
		/// Constructor that takes a region
		/// </summary>
		/// <param name="region"></param>
		public CustomMap(MapSpan region) : base(region) { }

		public void OnTap(Position coordinate)
		{
			OnTap(new MapTapEventArgs { Position = coordinate });

		}

		protected virtual void OnTap(MapTapEventArgs e)
		{
			var handler = Tapped;

			if (handler != null)
				handler(this, e);

			if (e != null && e.Position != null)
			{
				var pin = new Pin();
				pin.Label = "Tapped Pin";
				pin.Position = e.Position;
				this.Pins.Add(pin);
			}
		}

	}


}

/// <summary>
/// Event args used with maps, when the user tap on it
/// </summary>
public class MapTapEventArgs : EventArgs
{
	public Position Position { get; set; }
}