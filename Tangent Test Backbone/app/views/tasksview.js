var app = app || {}

app.TaskView = Backbone.View.extend({
  render: function(){
    var string = "<table class='table table-hover table-responsive'><thead><tr><th>Title</th><th>Due Date</th><th>Estimated Hours</th><th>Actions</th></thead><tbody>";
    //var getTasks = JSON.parse(sessionStorage.getItem("Tasks"));
    var getTasks = this.model.attributes;
    console.log(getTasks + Object.keys(getTasks));
    for (var i = 0; i < Object.keys(getTasks).length; i++){
      //console.log(getTasks[i].id);
      string += "<tr><td>" + getTasks[i].title +"</td><td>" + getTasks[i].due_date + "</td><td>" + getTasks[i].estimated_hours +"</td>" +
      "<td><a href='#/taskUpdate/:" + getTasks[i].id +"'><button>Update Task</button></a><a href='#/taskDelete/:" + getTasks[i].id +"'><button>Delete Task</button></a></td></tr>";
    }
    string += "</tbody></table>";
    this.$el.html(string);
    return this;
  }
});
