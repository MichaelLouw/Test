app.AddTask = Backbone.View.extend({

  events: {
    'submit .addtask-form': "onFormSubmit"
  },

  initialize: function(){
    this.template = _.template($("#addtask").html());
  },

  render: function(){

    this.$el.html(this.template(this.model));

    //this.$el.html(AddTaskPage);
    return this;
  },

  onFormSubmit: function(e){
    e.preventDefault();
    alert("submit task");
    this.trigger('form:submitted', {
      title: $(".task-title-input").val(),
      due_date: $(".task-duedate-input").val(),
      estimated_hours: $(".task-estimatedhours-input").val(),
      project: this.model.pk
    });
  }

  // SaveNewTask: function(){
  //   //post new task to api.
  // //   var task = new app.task();
  // //   task.fetch({data: {"title": $("#title").val(), "due_date": $("#duedate").val(), "estimated_hours": $("#estimatedhours").val(), "project": sessionStorage.getItem("projectid").substr(1)}, type: "POST",
  // //   headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
  // //   success: function(data){
  // //     console.log(data);
  // //     $.notify("New Project Saved", "success");
  // //     window.location.replace("");
  // //   },
  // //   error: function(data){
  // //     $.notify("error occured", "error");
  // //   }
  // // });
  // this.model.AddTask({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}});
  // }
});
