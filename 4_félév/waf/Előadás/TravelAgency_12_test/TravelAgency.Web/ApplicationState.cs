using System;
using System.Threading;

namespace ELTE.TravelAgency.Web
{
    public class ApplicationState
    {
	    private long _userCount;

		// Szálbiztos kezelés
		public long UserCount
	    {
			get => Interlocked.Read(ref _userCount);
			set => Interlocked.Exchange(ref _userCount, value);
		}
    }
}
