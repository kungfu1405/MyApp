using Mic.Core.DataTypes;
using Mic.Core.Entities;
using Mic.Core.MemCache;
using System.Collections.Generic;
using System.Linq;

namespace Mic.Core.Website
{
    public class BaseModel
    {
        public BaseModel()
        {
            AlertMessages = new List<AlertMessage>();
        }

        public FormActionMode ActionMode { get; set; }
        public IList<AlertMessage> AlertMessages { get; set; }
        public string ReturnUrl { get; set; }
        public string CommandName { get; set; }

        public string SaveMessageCache()
        {
            var randString = dStr.RandomText(8);
            while (MemCacheManager.Temp.ExistFormat(MemCacheConfigs.MessageDataKey, randString))
            {
                randString = dStr.RandomText(8);
            }
            MemCacheManager.Temp.SetFormat(AlertMessages, MemCacheConfigs.MessageDataKey, randString, MemCacheConfigs.TimeoutTemp);
            return randString;
        }

        public bool LoadMessageCache(string code)
        {
            if (MemCacheManager.Temp.ExistFormat(MemCacheConfigs.MessageDataKey, code))
            {
                var messages = MemCacheManager.Temp.GetFormat<List<AlertMessage>>(MemCacheConfigs.MessageDataKey, code);
                foreach (var msg in messages)
                    AlertMessages.Add(msg);
                return true;
            }
            return false;
        }

        public bool IsSuccessStatus()
        {
            return AlertMessages.All(e => e.IsSuccess());
        }

        public void AlertInfo(string message, string code = "")
        {
            AlertMessages.Add(new AlertMessage
            {
                StatusValue = EnumMessageStatus.Info,
                Message = message,
                StatusCode = code
            });
        }

        public void AlertSuccess(string message, string code = "")
        {
            AlertMessages.Add(new AlertMessage
            {
                StatusValue = EnumMessageStatus.Success,
                Message = message,
                StatusCode = code
            });
        }

        public void AlertWarning(string message, string code = "")
        {
            AlertMessages.Add(new AlertMessage
            {
                StatusValue = EnumMessageStatus.Warning,
                Message = message,
                StatusCode = code
            });
        }

        public void AlertError(string message, string code = "")
        {
            AlertMessages.Add(new AlertMessage
            {
                StatusValue = EnumMessageStatus.Danger,
                Message = message,
                StatusCode = code
            });
        }
    }
}
