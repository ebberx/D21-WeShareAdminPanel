using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D21WeShareAdminPanel.Model;
using System.Text.Json;
using System.Windows.Controls;
using D21WeShareAdminPanel.Model.DTO;
using System.Collections.ObjectModel;
using NNTPClient.Model;

namespace D21WeShareAdminPanel.ViewModel
{
    public class MainWindowViewModel : Bindable {

        public string sessionToken;
    
        // Users in group OC
        public ObservableCollection<UserDTO> ocUsersInGroup {
            get { return _ocUsersInGroup; }
            set { _ocUsersInGroup = value; propertyIsChanged(); }
        }
        private ObservableCollection<UserDTO> _ocUsersInGroup;
        

        public MainWindowViewModel(string _sessionToken) {
            // Save session token for later use
            this.sessionToken = _sessionToken;
        }

        public void SearchGroup(string groupName) {
        
        }

        public async Task<GroupDTO> GetGroupByID(int id) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/UserGroup/GetGroupDetailsByGroupID/" + id, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            GroupDTO? group;
            try {
                group = JsonSerializer.Deserialize<GroupDTO>(response);
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }

            return group != null ? group : null!;
        }

        public async void GetGroupDetailsByID(int id) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/UserGroup/GetGroupByGroupID/" + id, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
            }

            List<GroupDetailsDTO>? groupDetails;
            try {
                groupDetails = JsonSerializer.Deserialize<List<GroupDetailsDTO>>(response);

                // FIXME: This does not take null values into account
                if (groupDetails != null) {
                    List<UserDTO> users = groupDetails.Select(x => x.user!).ToList();
                    ocUsersInGroup = new ObservableCollection<UserDTO>(users);
                    Debug.WriteLine(users.Count);
                }
                    
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            
        }

    }
}
