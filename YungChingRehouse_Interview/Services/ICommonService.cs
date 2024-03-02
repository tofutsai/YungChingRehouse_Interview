using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.Services
{
    public interface ICommonService
    {
        string GetJsonString(object payload);
        UserInfo DecodeJsonToUserInfo(string json);
    }
}
