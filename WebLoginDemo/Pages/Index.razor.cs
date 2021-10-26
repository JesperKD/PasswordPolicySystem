using System.Threading.Tasks;
using WebLoginDemo.Data.Services;
using WebLoginDemo.Data.FormModels;
using Microsoft.AspNetCore.Components;
using WebLoginDemo.Data.Repositories;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Pages
{
    public partial class Index
    {
        [Inject] private LoginService LoginService { get; set; }
        [Inject] private LoginRepository LoginRepository { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private bool isProcessingSubmit = false;
        private string _infoMessage = string.Empty;
        private string _successMessage = string.Empty;
        private string _errorMessage = string.Empty;

        private LoginModel LoginModel;

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
                Login login = new(LoginModel.Username, LoginModel.Password, LoginModel.Attempts);

                bool result = await LoginService.CheckLogin(login);

                if (result == true) NavigationManager.NavigateTo("/success");

                else
                {
                    _errorMessage = "En bruger med denne kode findes ikke i systemet.";
                    isProcessingSubmit = false;
                }
                

            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
                System.Console.WriteLine(ex.Message);
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
