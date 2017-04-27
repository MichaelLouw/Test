var LoginView = Backbone.View.extend({
  el: $("#login-form"),

  events: {
    "click #login": "login"
  },

  initialize: function(){
    var self = this;

    this.username = $("#username");
    this.password = $("#password");

    this.username.change(function(e){
      self.model.set({username: $(e.currentTarget).val()});
    });

    this.password.change(function(e){
      self.model.set({password: $(e.currentTarget).val()});
    });
  },

  login: function(){
    var user= this.model.get('username');
    var password = this.model.get('password');

    //connect to api and set sessionToken.
    $.ajax({
      method: "POST",
      contentType: "application/json",
      url: "http://userservice.staging.tangentmicroservices.com:80/api-token-auth/",
      data: JSON.stringify({ "username": user, "password": password }),
      success: function(data){
          sessionStorage.setItem("Token", data.token);
          window.location.replace("main.html#/main");
          alert(sessionStorage.getItem("Token"));
      },
      error: function(data){
          alert(JSON.stringify(data));
          //handle error give feedback.
      }
    })
    return false;
  }
});

window.LoginView = new LoginView({model: new models.login() });