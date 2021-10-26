using System.Threading.Tasks;
using WebLoginDemo.DataModels;
using WebLoginDemo.Data.Services;
using WebLoginDemo.Data.FormModels;
using Microsoft.AspNetCore.Components;

namespace WebLoginDemo.Pages
{
    public partial class Index
    {
        private bool isProcessingSubmit = false;
        private string _infoMessage = string.Empty;
        private string _successMessage = string.Empty;
        private string _errorMessage = string.Empty;

        private NavigationManager NavigationManager;
        private LoginModel LoginModel;
        private LoginService LoginService;

        protected async override Task OnInitializedAsync()
        {
            LoginModel = new();
            
            await base.OnInitializedAsync();
        }

        private async Task OnValidSubmit()
        {
            ClearMessages();
            
            isProcessingSubmit = true;
            try
            {
                Login login = new(
                    username: LoginModel.Username,
                    password: LoginModel.Password,
                    attempts: LoginModel.Attempts
                    ) ;

                await LoginService.CreateAsync(login);

                NavigationManager.NavigateTo("");

            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
                isProcessingSubmit = false;
                StateHasChanged();
            }
        }


        private void ResetModelInformation()
        {
            LoginModel = new();
        }

        private void ClearMessages()
        {
            _infoMessage = string.Empty;
            _errorMessage = string.Empty;
        }
    }
}
