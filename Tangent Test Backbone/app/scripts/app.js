var AppRouter = Backbone.Router.extend({
  routes:{
    "": "LoadAll",
    "login": "login",
    "projectAddTask/:project": "AddTask",
    "projectDelete/:project": "DeleteProject",
    "projectUpdate/:project": "UpdateProject",
    "taskUpdate/:task": "UpdateTask",
    "taskDelete/:task": "DeleteTask"
  },

  UpdateTask: function(task){
    sessionStorage.setItem("task", task);
  }

  DeleteTask: function(task){
    $.ajax({
      method: "DELETE",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task,
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        //notify the user and reload the views.
        $.notify("Project Deleted");
        window.location.replace("main.html");
      },
      error: function(data){
        //handle error.
      }
    });
  }

  AddTask: function(project){
    sessionStorage.setItem("projectid", project);
    var addtasktoproject = new AddTaskView.AddTask({});
    $("#mainContainer").html(addtasktoproject.render().el);
  },

  DeleteProject: function(project){
    //delete the project
    $.ajax({
      method: "DELETE",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project,
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        //notify the user and reload the views.
        $.notify("Project Deleted");
        window.location.replace("main.html");
      },
      error: function(data){
        //handle error.
      }
    });
  },

  UpdateProject: function(project){
      sessionStorage.setItem("project", project);
      var updateproject = new UpdateProjectView.UpdateProject({});
      $("#mainContainer").html(updateproject.render().el);
  },

  LoadAll: function(){
    //get all data from projects and tasks.
    $.ajax({
      method: "GET",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        var projects = new models.project(data);
        sessionStorage.setItem("Projects", JSON.stringify(projects["attributes"]));
        // console.log(Object.keys(projects).length);
        // for (var i = 0; i < Object.keys(projects).length; i++){
        //   console.log(projects["attributes"][0]["pk"]);
        //   for (var name in projects){
        //     console.log(JSON.stringify(name));
        //   }
        // }
        var viewProjects = new project.ProjectView({
           collection: projects
        });
        $("#project").html(viewProjects.render().el);
        alert(data);
      },
      error: function(data){
        //handle error.
        alert(data);
      }
    });

    $.ajax({
      method: "GET",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        var tasks = new models.task(data);
        sessionStorage.setItem("Tasks", JSON.stringify(tasks["attributes"]));

        var viewTasks = new task.TaskView({
          collection: tasks
        });
        $("#task").html(viewTasks.render().el);
      },
      error: function(data){
        //handle error.
      }
    });
  },

  login: function(){
    window.LoginView = new LoginView({model: new login()});
  }
});

jQuery(document).ready(function(){
  var app = new AppRouter();

  $(function(){
    Backbone.history.start();
  })
});
