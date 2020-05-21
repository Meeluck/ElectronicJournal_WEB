using ElectronicJournal_WEB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal_WEB.Models
{
	public class UserSession
	{
		static UserSession _userSession;
		static int _userId;
		static string _userFullName;
		static int _accessLevelId;
		static string _accessLevelName;

		protected UserSession() { }

		public static UserSession Instance(int id)
		{
			if (_userSession == null)
			{
				_userSession = new UserSession();
				_userId = id;
				using (ElectronicalJournalContext db = new ElectronicalJournalContext())
				{
					var user = from us in db.Users
							   join al in db.AccessLevels on us.AccessLevelId equals al.AccessLevelId
							   where us.UserId == id
							   select new
							   {
								   FullName = us.LastName + " " + us.FirstName + " " +
								   (!string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty),
								   AccessLevel = us.AccessLevelId == null ? 0 : Convert.ToInt32(us.AccessLevelId),
								   AccessLevelName = al.AccessLevelName
							   };
					foreach (var item in user)
					{
						_userFullName = item.FullName;
						_accessLevelId = item.AccessLevel;
						_accessLevelName = item.AccessLevelName;
					}
				}
			}
			return _userSession;
		}

		public static UserSession Session
		{
			get
			{
				return _userSession;
			}
		}
		public static string GetName
		{
			get { return _userFullName; }
		}
		public static string AccessLevelName
		{
			get { return _accessLevelName; }
		}
		public static int AccessLevelId
		{
			get { return _accessLevelId; }
		}

	}
}
