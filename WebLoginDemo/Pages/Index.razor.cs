using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebLoginDemo.Pages
{
    public partial class Index
    {
        private bool isProcessingPasswordChangeRequest = false;
        private string _infoMessage = string.Empty;
        private string _successMessage = string.Empty;
        private string _errorMessage = string.Empty;
        private string _sessionUserEmail = string.Empty;
        private bool _isLoadingData = false;
        private bool _hasValidSession = true;
        private bool _isProcessingRequest = false;

        private ChangePasswordModel changePasswordModel;

        protected async override Task OnInitializedAsync()
        {
            changePasswordModel = new();
            
            _isLoadingData = true;

            _isLoadingData = false;

            await base.OnInitializedAsync();
        }


        private async Task OnValidSubmit_ChangeUserPassword()
        {
            ClearMessages();

            try
            {
                isProcessingPasswordChangeRequest = true;

                _infoMessage = "Arbejder på det, vent venligst..";

                IUser user = await UserService.GetUserByEmailAsync(_sessionUserEmail, _tokenSource.Token);

                //Role role = null;
                //Location location = null;
                //List<Area> areaList = new List<Area>();

                //User user = new(0, role, location, areaList, string.Empty, string.Empty, changePasswordModel.Email, changePasswordModel.Password);

                user.SetPassword(changePasswordModel.Password);

                user.GenerateSalt();
                user.HashPassword();

                await UserService.UpdateUserPasswordAsync(user, _tokenSource.Token);

                _infoMessage = "Adgangskode blev ændret.";
            }
            catch (Exception)
            {
                ClearMessages();
                _errorMessage = "Kunne ikke ændre adgangskode, prøv igen senere.";
            }
            finally
            {
                ResetModelInformation();

                isProcessingPasswordChangeRequest = false;
            }
        }

        private void OnInvalidSubmit_ChangeUserPassword()
        {
            _errorMessage = "Venligst udfyld de manglende felter, markeret med rød.";
        }

        private void ResetModelInformation()
        {
            changePasswordModel = new();

            changePasswordModel.Email = _sessionUserEmail;
        }

        private void ClearMessages()
        {
            _infoMessage = string.Empty;
            _errorMessage = string.Empty;
        }
    }
}
