using System;
namespace projet2_CRA.Models
{
	public interface IDal : IDisposable
	{
        void DeleteCreateDatabase();
    }
}

