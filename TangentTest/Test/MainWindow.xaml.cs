using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using TangentTest.DataSources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Runtime.Remoting.Messaging;

namespace TangentTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public static DataSources.ProjectsDBSet NewProjectsDBSet = new DataSources.ProjectsDBSet();
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            Login FirstLogin = new Login();
            FirstLogin.Show();
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Projects_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            populateTable();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populateTable();
            TangentTest.DataSources.ProjectsDBSet projectsDBSet = ((TangentTest.DataSources.ProjectsDBSet)(this.FindResource("projectsDBSet")));
        }

        public void populateTable()
        {
            //get data from web request
            try
            {
                //create webrequest to api.
                var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString();
                var request = (HttpWebRequest)WebRequest.Create(apiURL);

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var jsonString = JsonConvert.DeserializeObject<JArray>(responseString);

                //init dataset
                NewProjectsDBSet.Clear();
                NewProjectsDBSet.BeginInit();


                //populate the dataset.
                int length = jsonString.Count;
                for (int x = 0; x < length; x++)
                {
                    //try parse the data.
                    int pk;
                    pk = 0;
                    int.TryParse(jsonString[x]["pk"].ToString(), out pk);

                    DateTime start_date;
                    start_date = DateTime.MinValue;
                    DateTime.TryParse(jsonString[x]["start_date"].ToString(), out start_date);

                    string end_dateNUll;
                    DateTime end_date;
                    end_date = DateTime.MinValue;
                    DateTime.TryParse(jsonString[x]["end_date"].ToString(), out end_date);
                    if (end_date == DateTime.MinValue)
                    {
                        end_dateNUll = "(None)";
                    }
                    else
                    {
                        end_dateNUll = end_date.ToString();
                    }

                    bool is_billable, is_active;
                    is_active = false;
                    is_billable = false;
                    bool.TryParse(jsonString[x]["is_billable"].ToString(), out is_billable);
                    bool.TryParse(jsonString[x]["is_active"].ToString(), out is_active);

                    //add row to project table.
                    DataSources.ProjectsDBSet.ProjectDataTable Projects = new DataSources.ProjectsDBSet.ProjectDataTable();
                    Projects.AddProjectRow(pk, jsonString[x]["title"].ToString(), jsonString[x]["description"].ToString(), start_date, end_dateNUll, is_billable, is_active);
                    NewProjectsDBSet.Project.Merge(Projects);

                    //add row for each project task in project table.
                    int TaskLenght = jsonString[x]["task_set"].Count();
                    for (int y = 0; y < TaskLenght; y++)
                    {
                        //try parse the data.
                        int id, project;
                        id = 0;
                        project = 0;
                        int.TryParse(jsonString[x]["task_set"][y]["id"].ToString(), out id);
                        int.TryParse(jsonString[x]["task_set"][y]["project"].ToString(), out project);

                        DateTime due_date;
                        due_date = DateTime.MinValue;
                        DateTime.TryParse(jsonString[x]["task_set"][y]["due_date"].ToString(), out due_date);

                        //add row to task table.
                        DataSources.ProjectsDBSet.TasksDataTable ProjectTask = new DataSources.ProjectsDBSet.TasksDataTable();
                        ProjectTask.AddTasksRow(id, jsonString[x]["task_set"][y]["title"].ToString(), due_date, jsonString[x]["task_set"][y]["estimated_hours"].ToString(), project);

                        NewProjectsDBSet.Tasks.Merge(ProjectTask);
                    }
                }

                NewProjectsDBSet.EndInit();
                var items = new List<DataSources.ProjectsDBSet.ProjectRow>();

                foreach (ProjectsDBSet.ProjectRow dr in NewProjectsDBSet.Project.Rows)
                {
                    items.Add(dr);
                }

                //bind data to datagrid.
                projectDataGrid.ItemsSource = items;
            }
            catch (Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddProject modalWindow = new AddProject();
            modalWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ConfirmDeletion = System.Windows.MessageBox.Show("Please Confirm", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (ConfirmDeletion == MessageBoxResult.Yes)
            {
                //execute.
                ProjectsDBSet.ProjectRow Deletion = (ProjectsDBSet.ProjectRow)projectDataGrid.SelectedItem;

                foreach (DataRow tr in Deletion.Table.Rows)
                {
                    var pk = tr["pk"].ToString();
                    var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString() + pk + "/";
                    var request = (HttpWebRequest)WebRequest.Create(apiURL);

                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    populateTable();

                    //validate the response (logfile purposes)
                }
            }
            else
            {
                //no changes
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ProjectsDBSet.ProjectRow> Updated = projectDataGrid.Items.Cast<ProjectsDBSet.ProjectRow>().ToList();

                foreach (ProjectsDBSet.ProjectRow dr in Updated)
                {
                    if (dr["Title"].ToString() != NewProjectsDBSet.Project.Rows[0]["Title"].ToString() || dr["Description"].ToString() != NewProjectsDBSet.Project.Rows[0]["Description"].ToString() || dr["Start_Date"].ToString() != NewProjectsDBSet.Project.Rows[0]["Start_Date"].ToString() || dr["End_Date"].ToString() != NewProjectsDBSet.Project.Rows[0]["End_Date"].ToString() || dr["Is_Billable"].ToString() != NewProjectsDBSet.Project.Rows[0]["Is_Billable"].ToString() || dr["Is_Active"].ToString() != NewProjectsDBSet.Project.Rows[0]["Is_Active"].ToString())
                    {
                        //update at api.
                        var pk = dr["pk"].ToString();
                        var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString() + pk + "/";
                        var request = (HttpWebRequest)WebRequest.Create(apiURL);

                        var postData = "title=" + dr["Title"].ToString();
                        postData += "&description=" + dr["Description"].ToString();
                        postData += "&start_date=" + dr["Start_Date"].ToString().Replace(@"/", "-");
                        postData += "&end_date=" + dr["End_Date"].ToString().Replace(@"/", "-");
                        postData += "&is_billable=" + dr["Is_Billable"].ToString();
                        postData += "&is_active=" + dr["Is_Active"].ToString();

                        var data = Encoding.ASCII.GetBytes(postData);

                        request.Method = "PATCH";
                        request.ContentType = "application/json";
                        request.ContentLength = data.Length;
                        request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        populateTable();
                        //handle response from api.

                    }
                    else
                    {
                        //do nothing 
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }            
        }

        private void projectDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)

                {

                    this.Dispatcher.BeginInvoke(new DispatcherOperationCallback((param) =>

                    {

                        // get the new cell, set focus, then open for edit

                        //var cell = Helper.GetCell(projectDataGrid, rowIndex, colIndex);

                        //cell.Focus();



                        projectDataGrid.BeginEdit();

                        return null;

                    }), DispatcherPriority.Background, new object[] { null });

                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 0);
            }
        }
    }
}
