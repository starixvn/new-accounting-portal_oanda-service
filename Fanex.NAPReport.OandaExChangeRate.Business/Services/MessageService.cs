using Fanex.LetstalkService.Client;
using Fanex.LetstalkService.Core.Models;
using Fanex.NAPReport.OandaExChangeRate.Business;
using System.Collections.Generic;
using LetstalkConfig = Fanex.LetstalkService.Core.Services.AppConfigService;

namespace Fanex.OandaExChangeRate.Business.Services
{
    public interface MessageService
    {
        bool SendMessage(string message);
    }

    public class MessageServiceImp : MessageService
    {
        public bool SendMessage(string message)
        {
            var model = CreateModel(message);
            var service = new LetstalkClientServiceImp();
            return service.SendMessage(model);
        }

        private static LetstalkModel CreateModel(string message)
        {
            LetstalkConfig.Config.LetstalkServiceApi = AppSettings.LetstalkServiceApi;
            var letstalkAttachments = new List<LetstalkAttachment>();

            return new LetstalkModel
            {
                TargetName = AppSettings.TargetName,
                Group = AppSettings.LetstalkGroup,
                LetstalkAttachments = letstalkAttachments,
                Subject = "OANDA System Service - ERROR",
                Content = message
            };
        }
    }
}