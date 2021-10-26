﻿using System.Threading.Tasks;
using WebLoginDemo.Data.Services;
using WebLoginDemo.Data.FormModels;
using Microsoft.AspNetCore.Components;
using WebLoginDemo.Data.Repositories;

namespace WebLoginDemo.Pages
{
    public partial class Index
    {
        private bool isProcessingSubmit = false;
        private string _infoMessage = string.Empty;
        private string _successMessage = string.Empty;
        private string _errorMessage = string.Empty;

        private LoginModel LoginModel;
        private LoginService LoginService;
        private readonly LoginRepository LoginRepository;
        private readonly NavigationManager _NavigationManager;

        protected async override Task OnInitializedAsync()
        {
            LoginModel = new();
            LoginService = new(LoginRepository);
            
            await base.OnInitializedAsync();
        }

        private async Task OnValidSubmit()
        {
            ClearMessages();
            
            isProcessingSubmit = true;
            try
            {
                var result = await LoginService.GetByUsernameAsync(LoginModel.Username);

                if(result != null) _NavigationManager.NavigateTo("/success");

                else
                {
                    _errorMessage = "Bruger findes ikke i systemet.";
                }
                

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
