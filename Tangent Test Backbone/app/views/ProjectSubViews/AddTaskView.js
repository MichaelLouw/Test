var app = app || {};

app.AddTask = Backbone.View.extend({

  template: $("#addtask").html(),

  events: {
    'click #btnSaveTask': "SaveNewTask"
  },

  initialize: function(){
    this.render();
  },

  render: function(){
    // var AddTaskPage = "<div class='container'>"+
    //         "<h1 class='form-control' style='background-color: lightblue'>Add Task For Project</h1>"+
    //         "<div class='row'>"+
    //             "<div class='col-lg-6'>"+
    //                 "<label>Title</label>"+
    //             "</div>"+
    //             "<div class='col-lg-6'>"+
    //                 "<input type='text' class='form-control' id='title'/>"+
    //             "</div>"+
    //         "</div>"+
    //         "<div class='row'>"+
    //             "<div class='col-lg-6'>"+
    //                 "<label>Due Date</label>"+
    //             "</div>"+
    //             "<div class='col-lg-6'>"+
    //                 "<input type='date' class='form-control' id='duedate'/>"+
    //             "</div>"+
    //         "</div>"+
    //         "<div class='row'>"+
    //             "<div class='col-lg-6'>"+
    //                 "<label>Estimated Hours</label>"+
    //             "</div>"+
    //             "<div class='col-lg-6'>"+
    //                 "<input type='number' class='form-control' id='estimatedhours'/>"+
    //             "</div>"+
    //         "</div>"+
    //         "<br />"+
    //         "<div class='row' style='padding-left: 70%;'>"+
    //             "<a href='#/projectAddTask/save'><button id='btnSaveTask' class='form-control' class='savenewtask' style='width: 50%;'>Save New Task</button></a>"+
    //         "</div>"+
    //     "</div>";

    this.$el.html($("#addtask").html());

    //this.$el.html(AddTaskPage);
    return this;
  },

  SaveNewTask: function(){
    //post new task to api.
    var task = new app.task();
    task.fetch({data: {"title": $("#title").val(), "due_date": $("#duedate").val(), "estimated_hours": $("#estimatedhours").val(), "project": sessionStorage.getItem("projectid").substr(1)}, type: "POST",
    headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
    success: function(data){
      console.log(data);
      $.notify("New Project Saved", "success");
      window.location.replace("");
    },
    error: function(data){
      $.notify("error occured", "error");
    }
  });
    // $.ajax({
    //   method: "POST",
    //   contentType: "application/json",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
    //   headers:{
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   data: JSON.stringify({"title": $("#title").val(), "due_date": $("#duedate").val(), "estimated_hours": $("#estimatedhours").val(), "project": sessionStorage.getItem("projectid").substr(1)}),
    //   processData: false,
    //   success: function(data){
    //     console.log(data);
    //     $.notify("New Task Added", "success");
    //     window.location.replace("");
    //   },
    //   error: function(data){
    //     //handle error.
    //     console.log(data);
    //     $.notify("Error Adding Task", "error");
    //   }
    // });
  }
});
