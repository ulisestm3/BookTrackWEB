using Radzen;
using UI.Components.Layout;
using static EL.Enums;

namespace UI
{
    public interface IRadzenMessages
    {
        Task ShowAlert(DialogService dialogService, string message, eIcon type, eAlign eAlign, string title);
        Task ShowAlert(DialogService dialogService, string message, eIcon type, eAlign eAlign = eAlign.center);
        Task ShowAlert(DialogService dialogService, string message);
        Task<bool> ConfirmAsync(DialogService dialogService, string message, string title = "¿Está seguro?", eAlign eAlign = eAlign.center);


    }

    public class RadzenMessages : IRadzenMessages
    {
        public async Task ShowAlert(DialogService dialogService, string message, eIcon type, eAlign eAlign = eAlign.center, string title = "") => await Alert(dialogService, message, type, eAlign, title);
        public async Task ShowAlert(DialogService dialogService, string message, eIcon type, eAlign eAlign = eAlign.center) => await Alert(dialogService, message, type, eAlign, "");
        public async Task ShowAlert(DialogService dialogService, string message) => await Alert(dialogService, message, eIcon.info, eAlign.center, "");



        private async Task Alert(DialogService dialogService, string message, eIcon type, eAlign eAlign, string title)
        {
            var parameter = new Dictionary<string, object>();
            parameter["type"] = type;
            parameter["Title"] = title;
            parameter["Message"] = message;
            parameter["eAlign"] = eAlign;

            var opciones = new DialogOptions
            {
                ShowClose = false,
                CloseDialogOnEsc = true,
                CloseDialogOnOverlayClick = false,
                Resizable = false,
                Width = "400px"
            };

            await dialogService.OpenAsync<RadzenAlert>("", parameter, opciones);
        }
        public async Task<bool> ConfirmAsync(DialogService dialogService, string message, string title = "Confirmación", eAlign eAlign = eAlign.center)
        {
            var parameter = new Dictionary<string, object>
            {
                ["Title"] = title,
                ["Message"] = message,
                ["eAlign"] = eAlign
            };

            var opciones = new DialogOptions
            {
                ShowClose = false,
                CloseDialogOnEsc = true,
                CloseDialogOnOverlayClick = false,
                Resizable = false,
                Width = "400px"
            };

            var result = await dialogService.OpenAsync<RadzenConfirm>("", parameter, opciones);
            return Convert.ToBoolean(result);
        }

    }
}
