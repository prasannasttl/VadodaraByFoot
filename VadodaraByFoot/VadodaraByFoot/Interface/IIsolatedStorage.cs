using System;
namespace VadodaraByFoot.Interface
{
    public interface IIsolatedStorage
    {
		void SaveData(string filename, string text);
		string LoadData(string filename);
		bool ClearData(string filename);
    }
}
