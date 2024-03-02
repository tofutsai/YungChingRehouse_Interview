using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.Services
{
    public class CommonService : ICommonService
    {
        /// <summary>
        /// 取得登入者資訊
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public UserInfo DecodeJsonToUserInfo(string json)
        {
            UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
            return userInfo;
        }
        /// <summary>
        /// 登入者資訊轉為Json
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string GetJsonString(object payload)
        {
            string jsonString = JsonConvert.SerializeObject(payload);
            return jsonString;
        }
    }
}