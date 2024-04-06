using Newtonsoft.Json;

namespace ASM_C__PH39133.Controllers
{
	public static class SessionHelper
	{
		public static T? GetObjectFromJson<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default : JsonConvert.DeserializeObject<T>(value);
		}

		public static void SetObjectAsJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}
	}
}
