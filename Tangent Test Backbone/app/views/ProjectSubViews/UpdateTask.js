var app = app || {};

app.UpdateTask = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },

  model: task,
  tagName: 'div',
  tamplate: '',

  events: {
    'click #btnUpdateTask': "UpdateTask"
  },

  initialize: function(){
    this.template = _.template($("#updatetask").html());
  },

  render: function(){
    var string = JSON.stringify(this.model);
    var object = JSON.parse(string);
    this.$el.append(this.$el.html(this.template({title: object[0].title, duedate: object[0].due_date, estimatedhours: object[0].estimated_hours})));
    return this;
  },

  UpdateTask: function(){
    //post new task to api.
    var task = JSON.parse(sessionStorage.getItem("task"));
    console.log(task);
    console.log("update: " + task.id);
    $.ajax({
      method: "PATCH",
      contentType: "application/json",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.id + "/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify({"title": $("#title").val(), "due_date": $("#duedate").val(),"estimated_hours": $("#estimatedhours").val()}),
      processData: false,
      success: function(data){
        $.notify("Task updated", "success");
        window.location.replace("");
      },
      error: function(data){
        //handle error.
        $.notify("error updating task", "error");
      }
    });
  }
});
