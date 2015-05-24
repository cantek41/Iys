var ytApp = angular.module('YouTubeApp', []);

ytApp.constant('YT_event', {
  STOP:            0, 
  PLAY:            1,
  PAUSE:           2
});

ytApp.controller('YouTubeCtrl', function($scope, YT_event) {
  //initial settings
  $scope.yt = {
    width: 600, 
    height: 480, 
    videoid: "M7lc1UVf-VE"
  };
});

ytApp.directive('youtube', function($window, YT_event) {
  return {
    restrict: "E",

    scope: {
      height: "@",
      width: "@",
      videoid: "@"
    },

    template: '<div></div>',

    link: function(scope, element, attrs, $rootScope) {
      var tag = document.createElement('script');
      tag.src = "https://www.youtube.com/iframe_api";
      var firstScriptTag = document.getElementsByTagName('script')[0];
      firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
      
      var player;

      $window.onYouTubeIframeAPIReady = function() {

        player = new YT.Player(element.children()[0], {
          playerVars: {
            autoplay: 1,
            html5: 1,
            theme: "light",
            modesbranding: 0,
            color: "white",
            iv_load_policy: 3,
            showinfo: 1,
            controls: 1
          },
          
          height: scope.height,
          width: scope.width,
          videoId: scope.videoid, 

        });
      }

      scope.$watch('height + width', function(newValue, oldValue) {
        if (newValue == oldValue) {
          return;
        }
        
        player.setSize(scope.width, scope.height);
      
      });

      scope.$watch('videoid', function(newValue, oldValue) {
        if (newValue == oldValue) {
          return;
        }
        
        player.cueVideoById(scope.videoid);
      
      });
    }  
  };
});