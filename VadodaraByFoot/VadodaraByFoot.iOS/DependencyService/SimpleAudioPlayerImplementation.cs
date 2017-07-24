using AVFoundation;
using Foundation;
using System;
using System.IO;
using CoreMedia;
using VadodaraByFoot.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(VadodaraByFoot.iOS.DependencyService.SimpleAudioPlayerImplementation))]
namespace VadodaraByFoot.iOS.DependencyService
{
	/// <summary>
	/// Implementation for SimpleAudioPlayer
	/// </summary>
	public class SimpleAudioPlayerImplementation : ISimpleAudioPlayer
	{
		public event EventHandler PlaybackEnded;

		AVPlayer player;

		///<Summary>
		/// Length of audio in seconds
		///</Summary>
		// public double Duration
		// { get { return player == null ? 0 : player.CurrentItem.Duration.Seconds; } }
		public double Duration
		{
			get
			{
				if (player == null)
					return 0;

				var currentItem = player.CurrentItem;
				return currentItem.Duration.Seconds;
			}
		}

		///<Summary>
		/// Current position of audio in seconds
		///</Summary>
		public double CurrentPosition
		{ get { return player == null ? 0 : player.CurrentTime.Seconds; } }

		///<Summary>
		/// Playback volume (0 to 1)
		///</Summary>
		public double Volume
		{
			get { return player == null ? 0 : player.Volume; }
			set { SetVolume(value, Balance); }
		}

		///<Summary>
		/// Balance left/right: -1 is 100% left : 0% right, 1 is 100% right : 0% left, 0 is equal volume left/right
		///</Summary>
		public double Balance
		{
			get { return _balance; }
			set { SetVolume(Volume, _balance = value); }
		}
		double _balance = 0;

		///<Summary>
		/// Indicates if the currently loaded audio file is playing
		///</Summary>
		public bool IsPlaying
		//  { get { return player == null ? false : (Math.Abs(player.Rate - 1) > float.Epsilon); } }
		{ get { return player == null ? false : (player.Status == AVPlayerStatus.ReadyToPlay); } }

		public bool Loop
		{
			get { return _loop; }
			set
			{
				_loop = value;
				// if (player != null)
				// player.NumberOfLoops = _loop ? -1 : 1;
			}
		}
		bool _loop;

		///<Summary>
		/// Indicates if the position of the loaded audio file can be updated - always returns true on iOS
		///</Summary>
		public bool CanSeek
		{ get { return player == null ? false : true; } }

		///<Summary>
		/// Load wave or mp3 audio file as a stream
		///</Summary>
		public bool Load(Stream audioStream)
		{
			/*    var data = NSData.FromStream(audioStream);

				Stop();
				player?.Dispose();

				player = AVAudioPlayer.FromData(data);

				if (player != null)
					player.FinishedPlaying += OnPlaybackEnded;*/

			return (player == null) ? false : true;
		}

		///<Summary>
		/// Load wave or mp3 audio file from the Android assets folder
		///</Summary>
		public bool Load(string fileName)
		{
			player?.Dispose();
			NSString aSongURL = new NSString(fileName);
			AVPlayerItem aPlayerItem = new AVPlayerItem(new NSUrl(aSongURL));
			player = new AVPlayer(aPlayerItem);
			//player.Play();
			return (player == null) ? false : true;


			/*player?.Dispose();
            player = AVAudioPlayer.FromUrl(NSUrl.FromFilename(fileName));

            if(player != null)
                player.FinishedPlaying += OnPlaybackEnded;

            return (player == null) ? false : true;*/
		}

		private void OnPlaybackEnded(object sender, AVStatusEventArgs e)
		{
			PlaybackEnded?.Invoke(sender, e);
		}

		///<Summary>
		/// Begin playback or resume if paused
		///</Summary>
		public void Play()
		{
			if (player == null)
				return;

			if (!(Math.Abs(player.Rate - 1) > float.Epsilon))
			{
				var newTime = CMTime.FromSeconds(0, 1);
				player.Seek(player.CurrentTime, CMTime.Zero, CMTime.Zero);
				//player.CurrentTime = 0;
			}

			if ((int)player.CurrentTime.Seconds >= (int)Duration)
			{
				var newTime = CMTime.FromSeconds(0, (int)Duration);
				player.Seek(CMTime.Zero);
				player.Play();
				//player.CurrentTime = 0;
			}

			else
			{
				if (player.Status == AVPlayerStatus.ReadyToPlay)
				{
					player?.Play();
				}
			}
		}

		///<Summary>
		/// Pause playback if playing (does not resume)
		///</Summary>
		public void Pause()
		{
			player?.Pause();
		}

		///<Summary>
		/// Stop playack and set the current position to the beginning
		///</Summary>
		public void Stop()
		{
			if (player != null)
				player.Rate = 0;
			//player?.Pause();
			//Seek(0);
		}

		///<Summary>
		/// Seek a position in seconds in the currently loaded sound file 
		///</Summary>
		public void Seek(double position)
		{
			if (player == null)
				return;
			CMTime newTime = new CMTime((long)position, 1);
			player.Seek(newTime);

			// player.CurrentTime = position;
		}

		public void SetVolume(double volume, double balance)
		{
			if (player == null)
				return;

			volume = Math.Max(0, volume);
			volume = Math.Min(1, volume);

			balance = Math.Max(0, balance);
			balance = Math.Min(1, balance);

			var right = (balance < 0) ? volume * -1 * balance : volume;
			var left = (balance > 0) ? volume * 1 * balance : volume;
			player.Volume = (float)volume;
			// player.SetVolume((float)left, (float)right);
		}
		void OnPlaybackEnded()
		{
			PlaybackEnded?.Invoke(this, EventArgs.Empty);
		}

		bool isDisposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed)
				return;

			if (disposing)
			{
				player.Pause();
				//  player = new AVPlayer();
				//  player.FinishedPlaying -= OnPlaybackEnded;   
				player.Dispose();
				player = null;
			}

			isDisposed = true;
		}

		~SimpleAudioPlayerImplementation()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}
	}
}