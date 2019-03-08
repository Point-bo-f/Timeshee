using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using TimesheetMobileApp;

namespace TimesheetMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeePage : ContentPage
    {


        public object Json { get; private set; }

        public EmployeePage()
        {
            InitializeComponent();

            employeeList.ItemsSource = new String[] { "" };
            //employeeList.ItemSelected += EmployeeList_ItemSelected;
        }

        //private void EmployeeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        public async void LoadEmployees(object sender, EventArgs e)
        {

            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://timesheetmobilebackendapi.azurewebsites.net/");
                    string json = await client.GetStringAsync("/api/employee");
                    string[] employees = JsonConvert.DeserializeObject<string[]>(json);

                    employeeList.ItemsSource = employees;


                }
                catch (Exception ex)
                {
                    string errorMessage = ex.GetType().Name + ": " + ex.Message;
                    employeeList.ItemsSource = new string[] { errorMessage };
                }
            }
        }

        private async void ListWorkAssignments(object sender, EventArgs e)
        {
            string employee = employeeList.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(employee))
            {
                await DisplayAlert("List work", "You must select employee first.", "Ok");

            }
            else
            {
                Navigation.PushAsync(new WorkAssignmentsPage());
            }
        }
    }
}