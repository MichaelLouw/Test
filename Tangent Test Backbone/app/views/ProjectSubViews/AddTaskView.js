var app = app || {};

app.AddTask = Backbone.View.extend({

  model: AddNewTask,
  template: $("#addtask").html(),

  events: {
    'click #btnSaveTask': "SaveNewTask"
  },

  initialize: function(){
    this.render();
  },

  render: function(){

    this.$el.html(this.template(this.model));

    //this.$el.html(AddTaskPage);
    return this;
  },

  SaveNewTask: function(){
    //post new task to api.
  //   var task = new app.task();
  //   task.fetch({data: {"title": $("#title").val(), "due_date": $("#duedate").val(), "estimated_hours": $("#estimatedhours").val(), "project": sessionStorage.getItem("projectid").substr(1)}, type: "POST",
  //   headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
  //   success: function(data){
  //     console.log(data);
  //     $.notify("New Project Saved", "success");
  //     window.location.replace("");
  //   },
  //   error: function(data){
  //     $.notify("error occured", "error");
  //   }
  // });
  this.model.AddTask({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}});
  }
});
