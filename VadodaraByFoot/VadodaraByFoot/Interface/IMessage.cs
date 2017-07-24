using System;
namespace VadodaraByFoot.Interface
{
	public interface IMessage
	{
		void LongAlert(string message);
		void ShortAlert(string message);
	}
}
