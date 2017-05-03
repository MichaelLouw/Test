var app = app || {};

var AppRouter = Backbone.Router.extend({
  routes:{
    "": "LoadAll",
    "login": "login",
    "projectaddproject": "AddProject",
    "projectAddTask/:project": "AddTask",
    "projectDelete/:project": "DeleteProject",
    "projectUpdate/:project": "UpdateProject",
    "taskUpdate/:task": "UpdateTask",
    "taskDelete/:task": "DeleteTask"
  },

  AddProject: function(){
    var addnewproject = new app.newProject({

    });
    $("#mainContainer").html(addnewproject.render().el);
  },

  // UpdateTask: function(task){
  //   var taskupdate = new app.task({id:  task.substr(1) + "/" });
  //
  //   taskupdate.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
  //     console.log(data);
  //     tskData = data;
  //     var updatetask = new app.UpdateTask({
  //       collection: tskData
  //     });
  //     $("#mainContainer").html(updatetask.render().el);
  //   }, function(error){
  //     $.notify("erro occured", "error");
  //   });
  // },

  // DeleteTask: function(task){
  //   var deletetask = new app.task({id: task.substr(1) + "/"});
  //   deletetask.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, type: "DELETE", success: function(data){
  //     console.log(data);
  //     window.location.replace("main.html");
  //   }, error: function(data){
  //     $.notify("error occured", "error");
  //   }});
  // },

  // AddTask: function(project){
  //   sessionStorage.setItem("projectid", project);
  //   var addtasktoproject = new app.AddTask({});
  //   $("#mainContainer").html(addtasktoproject.render().el);
  // },

  // DeleteProject: function(project){
  //   //delete the project
  //   var deleteproj = new app.project({pk: project.substr(1) + "/"});
  //   deleteproj.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, type: "DELETE", success: function(data){
  //     console.log(data);
  //     window.location.replace("main.html");
  //   }, error: function(data){
  //     $.notify("error occured", "error");
  //   }});
  // },

  // UpdateProject: function(project){
  //     var proj = new app.project({pk:  project.substr(1) + "/" });
  //
  //     proj.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
  //       console.log(data);
  //       prjData = data;
  //       var updateproject = new app.UpdateProject({
  //         collection: prjData
  //       });
  //       $("#mainContainer").html(updateproject.render().el);
  //     }, function(error){
  //       $.notify("erro occured", "error");
  //     });
  // },

  LoadAll: function(){

    var projectTest = new app.project();
    projectTest.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
      projectData = data;
      //create view.
      var viewProjects = new app.ProjectView({
         model: projectTest
      });
      $("#project").html(viewProjects.render().el);
    }, function(error){
      $.notify("Error downloading projects", "error");

    });


    var taskTest = new app.task();
    taskTest.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
      Taskdata = data;
      //create view
      var viewTasks = new app.TaskView({
        model: taskTest
      });
      $("#task").html(viewTasks.render().el);

    }, function(error){
      $.notify("Error downloading tasks", "error");
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
