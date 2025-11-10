using DAL;
using EL;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace UI.Authentication
{
    public interface IAutenticationsServices
    {
        event Action? OnChange;
        Task Logout();
        Task<bool> Authenticate(Usuarios User);
        Task<UserSession> GetAuthenticated();
        Task<bool> IsAuthenticated();
        Task<Usuarios> GetAuthenticatedUser();
        Task ExtendSession();
        void NotifyChanged();
    }
    public class AutenticationsServices:IAutenticationsServices
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly int _sessionTimeoutMinutes = 0;
        private bool _isAuthenticated;
        private readonly IGeneral _Gral;
        private readonly IAuthenticationController _authenticationController;
        private IParametrosController _ParametrosController;
        public AutenticationsServices(ProtectedSessionStorage sessionStorage, IGeneral Gral, IParametrosController parametrosController, IAuthenticationController authentication)
        {
            _sessionStorage = sessionStorage;
            _Gral = Gral;
            _ParametrosController = parametrosController;
            _authenticationController = authentication;
            _sessionTimeoutMinutes = _Gral.ValidateInt(_ParametrosController.Register((byte)(Enums.eParametros.TiempoEspera)).Valor); ;  // Tiempo de expiración en minutos
        }
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();  // Notificar a los suscriptores
        public bool IsAuthenticatedChange
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyStateChanged();  // Notificar solo si cambia
                }
            }
        }
        public async Task Logout()
        {
            await _sessionStorage.DeleteAsync("SESSIONID");
            IsAuthenticatedChange = false;  // Actualiza y notifica
        }
        public async Task<bool> Authenticate(Usuarios User)
        {
            var entidad = await _authenticationController.Authenticate(User);
            if (entidad != null)
            {
                UserSession uSession = new()
                {
                    IdUsuario = entidad.IdUsuario,
                    NombreCompleto = entidad.NombreCompleto,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(_sessionTimeoutMinutes)
                };

                await _sessionStorage.SetAsync("SESSIONID", uSession);
                IsAuthenticatedChange = true;  // Actualiza y notifica
                return true;
            }
            return false;
        }
        public async Task<UserSession> GetAuthenticated()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            return storage.Success && storage.Value != null ? storage.Value : new UserSession();
        }
        public async Task<bool> IsAuthenticated()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            if (storage.Value != null && storage.Success)
            {
                if (DateTime.UtcNow <= storage.Value.ExpirationTime)
                {
                    IsAuthenticatedChange = true;
                    return true;
                }
                else
                {
                    await Logout();  // Cierra sesión si ha expirado
                }
            }
            IsAuthenticatedChange = false;
            return false;
        }
        public async Task<Usuarios> GetAuthenticatedUser()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            var result = storage.Success && storage.Value != null ? storage.Value : new UserSession();
            if (result != null)
            {
                var Registro = await _authenticationController.Existe(result.IdUsuario);
                if (Registro != null)
                {
                    return Registro;
                }
            }
            return new();
        }
        public async Task ExtendSession()
        {
            var storage = await _sessionStorage.GetAsync<UserSession>("SESSIONID");
            if (storage.Success && storage.Value != null)
            {
                storage.Value.ExpirationTime = DateTime.UtcNow.AddMinutes(_sessionTimeoutMinutes);
                await _sessionStorage.SetAsync("SESSIONID", storage.Value);
                IsAuthenticatedChange = true;  // Notificar sobre la extensión
            }
        }
        public void NotifyChanged()
        {
            NotifyStateChanged();
        }
    }
    public class UserSession
    {
        public short IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }
}
