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
        private static List<Projects> rows = new List<Projects>();
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
            populateTasks();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populateTable();
            populateTasks();
            TangentTest.DataSources.ProjectsDBSet projectsDBSet = ((TangentTest.DataSources.ProjectsDBSet)(this.FindResource("projectsDBSet")));
        }

        public void populateTable()
        {
            projectDataGrid.ItemsSource = Projects.SetProjects();
        }

        public void populateTasks()
        {
            tasksDataGrid.ItemsSource = Tasks.SetTasks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddProject modalWindow = new AddProject();
            modalWindow.ShowDialog();
        }


        //delete project
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult ConfirmDeletion = System.Windows.MessageBox.Show("Please Confirm", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

                if (ConfirmDeletion == MessageBoxResult.Yes)
                {
                    //execute.
                    Projects Deletion = projectDataGrid.SelectedItem as Projects;
                    var pk = Deletion.PK;
                    var apiURL = ConfigurationManager.AppSettings["Projects_Service"].ToString() + pk + "/";
                    var request = (HttpWebRequest)WebRequest.Create(apiURL);

                    request.Method = "DELETE";
                    request.ContentType = "application/json";
                    request.Headers[HttpRequestHeader.Authorization] = "Token " + Application.Current.Properties["Token"].ToString();

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    populateTable();
                }
                else
                {
                    //no changes
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects addtaskProject = projectDataGrid.SelectedItem as Projects;
                if (rows.Count == 1)
                {
                    Application.Current.Properties["Project_PK"] = rows[0].PK.ToString();
                    AddProjectTask NewAdd = new AddProjectTask();
                    NewAdd.ShowDialog();
                }
                else
                {
                    //no project selected.
                }
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 10);
            }            
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects prjRow = projectDataGrid.SelectedItem as Projects;
                rows.Add(prjRow);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 25);
            }            
        }

        private void Select_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Projects prjRow = projectDataGrid.SelectedItem as Projects;
                rows.Remove(prjRow);
            }
            catch(Exception ex)
            {
                Logger.error(ex.Message, 26);
            }
        }
    }
}
