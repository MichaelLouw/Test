var AddTaskView = {};

AddTaskView.AddTask = Backbone.View.extend({

  event: {
    'click #btnSaveTask': "SaveNewTask"
  },

  render: function(){
    var AddTaskPage = "<div class='container'>"+
            "<h1 class='form-control' style='background-color: lightblue'>Add Task For Project</h1>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Title</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='text' class='form-control' id='title'/>"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Due Date</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='date' class='form-control' id='duedate'/>"+
                "</div>"+
            "</div>"+
            "<div class='row'>"+
                "<div class='col-lg-6'>"+
                    "<label>Estimated Hours</label>"+
                "</div>"+
                "<div class='col-lg-6'>"+
                    "<input type='number' class='form-control' id='estimatedhours'/>"+
                "</div>"+
            "</div>"+
            "<br />"+
            "<div class='row' style='padding-left: 70%;'>"+
                "<a href='#/'><button id='btnSaveTask' class='form-control' style='width: 50%;'>Save New Task</button></a>"+
            "</div>"+
        "</div>";

        this.$el.html(AddTaskPage);
        return this;
  },

  SaveNewTask: function(){
    //post new task to api.
    alert(this.options.projectid);
    $.ajax({
      method: "POST",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify({"title": $("#title").val(), "due_date": $("#duedate").val(), "estimated_hours": $("#estimatedhours").val(), "project": sessionStorage.getItem("projectid")}),
      processData: false,
      success: function(data){

      },
      error: function(data){
        //handle error.
      }
    });
  }
});
