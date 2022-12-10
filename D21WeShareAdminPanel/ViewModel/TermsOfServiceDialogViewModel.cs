using NNTPClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using D21WeShareAdminPanel.Model;
using D21WeShareAdminPanel.Model.DTO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace D21WeShareAdminPanel.ViewModel
{
    public class TermsOfServiceDialogViewModel : Bindable
    {
        public string TermsOfService { 
            get { return _TermsOfService!; } 
            set { _TermsOfService = value; propertyIsChanged(); } }
        private string? _TermsOfService;


        public async void UpdateTOS() {
            await APIRequester.UpdateTOS(TermsOfService);
        }

        public async void GetTOS() {
            string res = await APIRequester.Get("https://api-wan-kenobi.ovh/api/TermsOfService/GetLatestToS", "");
            
            // Check if response empty
            if (String.IsNullOrEmpty(res)) {
                Debug.WriteLine("Response is empty");
                return;
            }

            List<InPaymentDTO>? expenses;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                TermsOfService = JsonSerializer.Deserialize<TermsOfServiceDTO>(res, options)!.content!;
                return;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
