﻿#if UNITY_WEBPLAYER
namespace UnityEngine.Cloud.Analytics
{
	internal class WebPlayerWrapper : BasePlatformWrapper
	{
		public override string NewGuid()
		{
			string guidKey = "UNITY_ANALYTICS_GUID_KEY";
			string savedKey = PlayerPrefs.GetString(guidKey, null);
			if (string.IsNullOrEmpty(savedKey))
			{
				savedKey = System.Guid.NewGuid().ToString();
				PlayerPrefs.SetString(guidKey, savedKey);
			}

			return savedKey;
		}
	}
}
#endif