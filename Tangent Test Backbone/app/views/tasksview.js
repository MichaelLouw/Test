var app = app || {}

app.TaskView = Backbone.View.extend({
  // tagName: 'tr',
  //
  // initialize: function(options){
  //   _.bindAll(this.$("#task"), 'render');
  //
  //   this.model.bind('change', this.render);
  // },
  //
  // render: function(){
  //   jQuery(this.el).empty();
  //
  //   //add table columns
  //   jQuery(this.el).append(jQuery('<tr>' + this.model.get('Title') + '</tr>'));
  //   jQuery(this.el).append(jQuery('<tr>' + this.model.get('Due_Date') + '</tr>'));
  //   jQuery(this.el).append(jQuery('<tr>' + this.model.get('Estimated Hours') + '</tr>'));
  //
  //   return this;
  // }
  render: function(){
    var string = "<table class='table table-hover table-responsive'><thead><tr><th>Title</th><th>Due Date</th><th>Estimated Hours</th><th>Actions</th></thead><tbody>";
    //var getTasks = JSON.parse(sessionStorage.getItem("Tasks"));
    var getTasks = this.collection;
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

// task.TaskViewBody = Backbone.View.extend({
//   collection: null,
//
//   el: 'tbody',
//
//   initialize: function(options){
//     this.collection = options.collection;
//
//     _.bindAll(this, 'render');
//
//     this.collection.bind('reset', this.render);
//     this.collection.bind('add', this.render);
//     this.collection.bind('remove', this.render);
//     this.collection.bind('update', this.render);
//   },
//
//   render: function(){
//     var element = jQuery(this.el);
//
//     element.empty();
//
//     this.collection.forEach(function(item){
//       var itemView = new views.TaskViewItem({
//         taskmodel: item
//       });
//
//       element.append(itemView.render().el);
//     });
//
//     return this;
//   }
// });
