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
using Microsoft.Win32;

namespace D21WeShareAdminPanel.ViewModel
{
    public class MainWindowViewModel : Bindable {

        public string sessionToken;

        // Users in group OC
        public ObservableCollection<UserDTO> ocUsersInGroup {
            get { return _ocUsersInGroup!; }
            set { _ocUsersInGroup = value; propertyIsChanged(); }
        }
        private ObservableCollection<UserDTO>? _ocUsersInGroup;

        // Groups that the user is in OC
        public ObservableCollection<GroupDTO> ocGroupsOfUser {
            get { return _ocGroupsOfUser!; }
            set { _ocGroupsOfUser = value; propertyIsChanged(); }
        }
        private ObservableCollection<GroupDTO>? _ocGroupsOfUser;

        // Group tab object and update display method delegate
        public delegate void DisplayGroupDelegate();
        public DisplayGroupDelegate DisplayGroup = null!;
        public GroupDTO groupInTab { get { return _groupInTab!; } set { _groupInTab = value; DisplayGroup(); } }
        private GroupDTO? _groupInTab;

        // User tab object and update display method delegate
        public delegate void DisplayUserDelegate();
        public DisplayGroupDelegate DisplayUser = null!;
        public UserDTO userInTab { get { return _userInTab!; } set { _userInTab = value; DisplayUser(); } }
        private UserDTO? _userInTab;

        public MainWindowViewModel(string _sessionToken) {
            // Save session token for later use
            this.sessionToken = _sessionToken;
        }

        public async Task<GroupDTO> GetGroupByID(int id) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/ShareGroup/GetGroupDetailsByGroupID/" + id, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            GroupDTO? group;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                group = JsonSerializer.Deserialize<GroupDTO>(response, options);
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }

            return group != null ? group : null!;
        }

        public async Task<List<GroupDTO>> GetGroupsByName(string search) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/ShareGroup/SearchShareGroups/" + search, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            List<GroupDTO>? groups;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                groups = JsonSerializer.Deserialize<List<GroupDTO>>(response, options);
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }

            return groups != null ? groups : null!;
        }

        public async void GetGroupDetailsByID(int id) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/UserGroup/GetGroupByGroupID/" + id, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return;
            }

            List<GroupDetailsDTO>? groupDetails;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                groupDetails = JsonSerializer.Deserialize<List<GroupDetailsDTO>>(response, options);

                // FIXME: This does not take null values into account
                if (groupDetails != null) {
                    List<UserDTO> users = groupDetails.Select(x => x.user!).ToList();
                    ocUsersInGroup = new ObservableCollection<UserDTO>(users);
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            
        }

        public async Task<UserDTO> GetUsersByID(string id) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/ShareUser/GetUserByID/" + id, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            UserDTO? user;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                user = JsonSerializer.Deserialize<UserDTO>(response, options);
                return user != null ? user : null!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            
            return null!;
        }

        public async Task<List<UserDTO>> GetUsersByName(string search) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/ShareUser/SearchShareUsers/" + search, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            List<UserDTO>? users;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                users = JsonSerializer.Deserialize<List<UserDTO>>(response, options);
                
                return users != null ? users : null!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return null!;
        }

        public async void GetAllGroupsForUser(int userid) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/ShareUser/GetAllUsersGroups/" + userid, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return;
            }

            List<GroupDTO>? groupDetails;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                groupDetails = JsonSerializer.Deserialize<List<GroupDTO>>(response, options);
                if(groupDetails != null)
                    ocGroupsOfUser = new ObservableCollection<GroupDTO>(groupDetails);

                return;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task<List<ExpenseDTO>> GetExpensesByName(string search) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/Expense/SearchExpenses/" + search, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            List<ExpenseDTO>? expenses;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                expenses = JsonSerializer.Deserialize<List<ExpenseDTO>>(response, options);

                return expenses != null ? expenses : null!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return null!;
        }

        public async Task<ExpenseDTO> GetExpenseByID(string search) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/Expense/GetExpense/" + search, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            ExpenseDTO? expense;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                expense = JsonSerializer.Deserialize<ExpenseDTO>(response, options);

                return expense != null ? expense : null!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return null!;
        }

        public async Task<ExpenseDTO> GetInPaymentByID(string search) {
            string response = await APIRequester.Get("https://api-wan-kenobi.ovh/api/InPayment/GetInPayment/" + search, sessionToken);

            // Check if response empty
            if (String.IsNullOrEmpty(response)) {
                Debug.WriteLine("Response is empty");
                return null!;
            }

            ExpenseDTO? expense;
            try {
                JsonSerializerOptions options = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
                expense = JsonSerializer.Deserialize<ExpenseDTO>(response, options);

                return expense != null ? expense : null!;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return null!;
        }

        public async void ResetPasswordOfUser() {
            await APIRequester.ResetPassword(userInTab.userId);
        }

        public async void UpdateUser(UserDTO user) {
            userInTab = user;
            string response = await APIRequester.UpdateUser(new UpdateUserDTO().SetUpdateUserDTO(userInTab), sessionToken);
            Debug.WriteLine(response);
        }

        public async void UpdateGroup(GroupDTO group) {
            groupInTab = group;
            string response = await APIRequester.UpdateGroup(groupInTab, sessionToken);
            Debug.WriteLine(response);
        }

        public async void DeleteUser() {
            await APIRequester.DeleteUser(userInTab.userId);
            userInTab = null!;
        }

        public void Logout() {
            APIRequester.Logout(sessionToken);
        }
    }
}
