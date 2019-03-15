using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using TimesheetMobileApp;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

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
        public async void TakePhoto(object sender, EventArgs e)
        {
            string employee = employeeList.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(employee))
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable)
                {
                    await DisplayAlert("Ops", "Kamera ei löydy", "Ok");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo"
        });
                if (file == null)
                    return;
                FotoImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }
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
