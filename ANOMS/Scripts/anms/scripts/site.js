'use strict';

// This JS is only needed for the demo to show features

var progressTrackerDemo = (function () {

    var animPathLinks = document.querySelectorAll('.anim--path .progress-step');
    var animPathLinksLength = animPathLinks.length;

    function init() {
        var oId = getQuerystring("o_ID");
        $.ajax({
            url: 'OrderStatuModel',
            data: { "id": oId },
            method: 'get',
            success: function (data) {
                (data);
                if (data == "In_production") {
                    _activateOtherLinks(0);
                    _toggleClass(this, 'is-complete');

                    if (this.nextElementSibling !== null) {
                        _toggleClass(this.nextElementSibling, 'is-active');
                    }
                    animPathLinksLength = 0;
                    index = 0;
                }
                else if (data == "Inshipment") {
                    debugger;
                    _activateOtherLinks(1);
                    _toggleClass(this, 'is-complete');

                    if (this.nextElementSibling !== null) {
                        _toggleClass(this.nextElementSibling, 'is-active');
                    }
                    //index = 1;
                    //animPathLinksLength = 1;
                }
                else if (data == "Packaging") {
                    _activateOtherLinks(2);
                    _toggleClass(this, 'is-complete');

                    if (this.nextElementSibling !== null) {
                        _toggleClass(this.nextElementSibling, 'is-active');
                    }
                    //animPathLinksLength = 2;
                    //index = 2;
                }
                else if (data == "Deliver") {
                    _activateOtherLinks(3);
                    _toggleClass(this, 'is-complete');

                    if (this.nextElementSibling !== null) {
                        _toggleClass(this.nextElementSibling, 'is-active');
                    }
                    animPathLinksLength = 3;
                    index = 3;
                }
            },
            error: function (data) { (data); }
        });
        //debugger;
        if (animPathLinksLength > 0) {
            for (var i = 0; i < animPathLinksLength; i++) {
                _handleClick(animPathLinks[i], i);
            }


        }
      


    }

    function _handleClick(link, index) {
      
        link.addEventListener('click', function (e) {
            e.preventDefault();
            alert("are you sure to Update?")
            //debugger;
            _deactivateOtherLinks(index);
            _toggleClass(this, 'is-complete');

            if (this.nextElementSibling !== null) {
                _toggleClass(this.nextElementSibling, 'is-active');
            }

        });
    };

    function getQuerystring(key) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == key) {
                return pair[1];
            }
        }
    }
    function _activateOtherLinks(activeIndex) {
        debugger;
        for (var i = 0; i < animPathLinksLength; i++) {
            if (i <= activeIndex) {
                
                _addClass(animPathLinks[i], 'is-complete');
                _addClass(animPathLinks[i], 'is-active');
            }
        }
        var oId = getQuerystring("o_ID");

        $.ajax({
            url: 'OrderStatusnew',
            data: { "currentStatus": activeIndex, "orderId": oId },
            method: 'post',
            success: function (data) { (data); },
            error: function (data) { (data); }
        });


    };

    function _deactivateOtherLinks(activeIndex) {
        for (var i = 0; i < animPathLinksLength; i++) {
            if (i >= activeIndex) {
                debugger;
                _removeClass(animPathLinks[i], 'is-complete');
                _removeClass(animPathLinks[i], 'is-active');
            }
        }
      var oId=  getQuerystring("o_ID");

        $.ajax({
            url: 'OrderStatusnew',
            data: { "currentStatus": activeIndex, "orderId": oId },
            method: 'post',
            success: function (data) {(data); },
            error: function (data) {  (data); }
        });


    };

    function _toggleClass(el, className) {
        debugger;
        if (el.classList) {
            el.classList.toggle(className);
        } else {
            var classes = el.className.split(' ');
            var existingIndex = classes.indexOf(className);

            if (existingIndex >= 0)
                classes.splice(existingIndex, 1);
            else
                classes.push(className);

            el.className = classes.join(' ');
        }

    }

    function _removeClass(el, className) {

        if (el.classList)
            el.classList.remove(className);
        else
            el.className = el.className.replace(new RegExp('(^|\\b)' + className.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');

    }

    return {
        init: init
    };

    function _addClass(el, className) {

        if (el.classList)
            el.classList.add(className);
        else
            el.className = el.className.replace(new RegExp('(^|\\b)' + className.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');

    }



})();

progressTrackerDemo.init();
