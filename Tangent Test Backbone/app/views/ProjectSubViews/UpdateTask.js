var app = app || {};

app.UpdateTask = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },

  tagName: 'div',
  tamplate: '',

  events: {
    'click #btnUpdateTask': "UpdateTask"
  },

  initialize: function(){
    this.template = _.template($("#updatetask").html());
  },

  render: function(){
    var string = this.collection;
    //var object = JSON.parse(string);
    this.$el.append(this.$el.html(this.template({title: string.title, duedate: string.due_date, estimatedhours: string.estimated_hours})));
    return this;
  },

  UpdateTask: function(){
    //post new task to api.
    console.log(this.collection.id);
    var task = new app.task({id: this.collection.id + "/"});
    task.fetch({data: {"title": $("#title").val(), "due_date": $("#duedate").val(),"estimated_hours": $("#estimatedhours").val()}, type: "PATCH", headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
    success: function(data){
      console.log(data);
       window.location.replace("");
    },
    error: function(data){
      $.notify("error occured", "error");
    }
  });
  }
});
