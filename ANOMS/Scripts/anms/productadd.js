$(document).ready(function () {

    $('#titleengcheck').hide();
    $('#titlebangcheck').hide();
    $('#pm_engcheck').hide();
    $('#pm_bangcheck').hide();
    $('#imagecheck').hide();


    var titleeng_err = true;
    var titlebang_err = true;
    var engname_err = true;
    var banglaname_err = true;
    var image_err = true;


    //eng title
    $('#titleenglish').keyup(function () {
        titleenglish_check();
    });

    function titleenglish_check() {
        var titleeng_val = $('#titleenglish').val();

        if (titleeng_val.length == "") {
            $('#titleengcheck').show();
            $('#titleengcheck').html("** please enter your details");
            $('#titleengcheck').focus();
            $('#titleengcheck').css("color", "red");
            titleeng_err = false;
            return false;
        } else {
            $('#titleengcheck').hide();
        }


        if ((titleeng_val.length <= 10) || (titleeng_val.length > 50000)) {
            $('#titleengcheck').show();
            $('#titleengcheck').html("** paragraph length must be 10 to 50000");
            $('#titleengcheck').focus();
            $('#titleengcheck').css("color", "red");
            titleeng_err = false;
            return false;
        } else {
            $('#titleengcheck').hide();
        }
    }
    // bangla title
    $('#titlebangla').keyup(function () {
        titlebangla_check();
    });

    function titlebangla_check() {
        var titlebang_val = $('#titlebangla').val();

        if (titlebang_val.length == "") {
            $('#titlebangcheck').show();
            $('#titlebangcheck').html("** please enter your details");
            $('#titlebangcheck').focus();
            $('#titlebangcheck').css("color", "red");
            titlebang_err = false;
            return false;
        } else {
            $('#titlebangcheck').hide();
        }


        if ((titlebang_val.length <= 10) || (titlebang_val.length > 50000)) {
            $('#titlebangcheck').show();
            $('#titlebangcheck').html("** paragraph length must be 10 to 50000");
            $('#titlebangcheck').focus();
            $('#titlebangcheck').css("color", "red");
            titlebang_err = false;
            return false;
        } else {
            $('#titlebangcheck').hide();
        }
    }


    //English name check
    $('#nameenglish').keyup(function () {
        pm_engnamecheck();
    });

    function pm_engnamecheck() {
        var engname_val = $('#nameenglish').val();
        if (engname_val.length == '') {
            $('#pm_engcheck').show();
            $('#pm_engcheck').html("** please fill  your Name");
            $('#pm_engcheck').focus();
            $('#pm_engcheck').css("color", "red");
            engname_err = false;
            return false;
        } else {
            $('#pm_engcheck').hide();
        }

        if (!isNaN(engname_val)) {
            $('#pm_engcheck').show();
            $('#pm_engcheck').html("** Numbers are not allowed");
            $('#pm_engcheck').focus();
            $('#pm_engcheck').css("color", "red");
            engname_err = false;
            return false;
        } else {
            $('#pm_engcheck').hide();
        }



        if ((engname_val.length <= 3) || (engname_val.length > 50000)) {
            $('#pm_engcheck').show();
            $('#pm_engcheck').html("** name must be 4 t0 50000");
            $('#pm_engcheck').focus();
            $('#pm_engcheck').css("color", "red");
            engname_err = false;
            return false;
        } else {
            $('#pm_engcheck').hide();
        }
    }

    // banglaname check pass

    $('#pm_namebang').keyup(function () {
        pm_banglanamecheck();
    });

    function pm_banglanamecheck() {
        var p_bangla_val = $('#pm_namebang').val();

        if ((p_bangla_val.length == '') && (!isNaN(p_bangla_val))) {
            $('#pm_bangcheck').show();
            $('#pm_bangcheck').html("** please write in Bangla");
            $('#pm_bangcheck').focus();
            $('#pm_bangcheck').css("color", "red");
            banglaname_err = false;
            return false;
        } else {
            $('#pm_bangcheck').hide();
        }

        if ((p_bangla_val.length <= 3) || (p_bangla_val.length > 50000)) {
            $('#pm_bangcheck').show();
            $('#pm_bangcheck').html("** must be 4 to 5000 character");
            $('#pm_bangcheck').focus();
            $('#pm_bangcheck').css("color", "red");
            banglaname_err = false;
            return false;
        } else {
            $('#pm_bangcheck').hide();
        }

    }

    //image chek
    $('#imageloder').mouseenter(function () {
        recentimagecheck();
    });

    function recentimagecheck() {

        var fileInput = $('#imageloder').val();
        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
        if (!allowedExtensions.exec(fileInput)) {
            $('#imagelodercheck').show();
            $('#imagelodercheck').html("Please upload file having extensions .jpeg/.jpg/.png/.gif only.");
            $('#imagelodercheck').focus();
            $('#imagelodercheck').css("color", "red");
            image_err = false;
            return false;
        } else {
            $('#imagelodercheck').hide();
        }

    }


    $('#submitbtn').click(function () {

        titleeng_err = true;
        titlebang_err = true;
        engname_err = true;
        banglaname_err = true;
        image_err = true;


        titleenglish_check();
        titlebangla_check();
        pm_engnamecheck();
        pm_banglanamecheck();
        recentimagecheck();

        if ((titleeng_err == true) && (titlebang_err == true) && (engname_err == true) && (banglaname_err == true) && (image_err == true)) {
            return true;
        } else {
            return false;
        }

    });
});