<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Linkskart.aspx.cs" Inherits="LINKSKARTNEW.Linkskart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>LINKSKART</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <%--<script src="Scripts/jquery-1.8.2.min.js"></script>--%>
     
    <style>
        .btn-sunny {
            color: #fff;
            background-color: #18a689;
            border-bottom: 3px solid #B6491F;
            outline: none;
            margin-top: 9px;
            margin-bottom: 9px;
            margin-left: -5px;
        }

        .btn-success {
            background-color: #18a689;
            border-color: #18a689;
        }

        .img-circle {
            width: 50px;
        }

        .navbar-default navbar-static-side {
            min-height: 605px;
        }

        .page-heading {
            padding: 0px 10px 2px 10px !important;
        }

        .navbar-default {
            background-color: #0A2A34 !important;
        }

        .gray-bg {
            background: url("patterns/header-profile.png") no-repeat !important;
        }

        .img-responsive {
            width: 215px !important;
            height: 100px !important;
        }

        .dropdown-menu > li > a {
            border-radius: 3px;
            color: inherit;
            line-height: 17px;
            margin: 2px;
            text-align: left;
            font-weight: normal;
        }

        .dropdownfilemanager {
            position: absolute;
            padding-left: 200px;
        }

        .leftwidth {
            width: 160px;
            overflow: hidden;
        }

        .rightpadding {
            padding-top: 8px !important;
        }

        .dropdown-submenu {
            position: relative;
        }

            .dropdown-submenu > .dropdown-menu {
                top: 0;
                left: 100%;
                margin-top: -6px;
                margin-left: -1px;
                -webkit-border-radius: 0 6px 6px 6px;
                -moz-border-radius: 0 6px 6px;
                border-radius: 0 6px 6px 6px;
            }

            .dropdown-submenu:hover > .dropdown-menu {
                display: block;
            }

            .dropdown-submenu > a:after {
                display: block;
                content: " ";
                float: right;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
                border-width: 5px 0 5px 5px;
                border-left-color: #ccc;
                margin-top: 5px;
                margin-right: -10px;
            }

            .dropdown-submenu:hover > a:after {
                border-left-color: #fff;
            }

            .dropdown-submenu.pull-left {
                float: none;
            }

        .file-box {
            float: left;
            width: 235px !important;
        }

        .dropdown-submenu.pull-left > .dropdown-menu {
            left: -100%;
            margin-left: 10px;
            -webkit-border-radius: 6px 0 6px 6px;
            -moz-border-radius: 6px 0 6px 6px;
            border-radius: 6px 0 6px 6px;
        }

        .linkdrop {
            /* float: right; */
            z-index: 1000 !important;
            float: right !important;
            position: absolute;
            padding-left: 180px;
        }
    </style>
     <script>
         $(document).ready(function () {
             $('#loading').hide();
         });
         $(document).keyup(function (e) {
             if (e.keyCode == 13) { // enter

                 e.stopPropagation(); //you can also say e.preventDefault();
             }
         });
         var sharetab = "";
     
         function searchlinks(e) {

             var key = e.keyCode || e.which;

             if (key == 13) {
                 debugger;
                 if (document.getElementById('txt_searchkey').value != "") {
                     searchfile();
                 }
                 else {
                     alert('Please enter text in search box');
                     document.getElementById('txt_searchkey').focus();
                 }

             }


         }

         function logout() {
             PageMethods.logo(OnSucceslogssearch, onfailurelogsearch);
         }
         function OnSucceslogssearch() {
             location.href = "frm_login.aspx";
         }
         function onfailurelogsearch() {

         }

         function searchfile() {
             debugger;
             if (document.getElementById('txt_searchkey').value != "") {
                 PageMethods.searchfiles(document.getElementById('txt_searchkey').value.replace('@', '') + '@' + document.getElementById('hid_userid').value, OnSuccesssearch, onfailuresearch);
             }
             else {
                 alert('Please enter text in search box');
                 document.getElementById('txt_searchkey').focus();
             }

         }
         function OnSuccesssearch(result) {

             if (result != '') {
                 document.getElementById('div_tabcontent').innerHTML = result;

             }
             else {
                 document.getElementById('div_tabcontent').innerHTML = "<h3>No Results Found</h3>";
             }
         }

         function goback() {
             debugger;
             document.getElementById('txt_searchkey').value = "";

             document.getElementById('tabs').style.display = "block";

             document.getElementById('div_rightbuttons').style.display = "block";
             if (document.getElementById("a_goback").text != 'Clear') {
                 selecttab(document.getElementById('hid_tab').value);
             }
             document.getElementById("a_goback").text = "Clear";
         }

         function onfailuresearch() {

         }
         function loadworddocument(filename) {
             debugger;
             var mylink = document.getElementById("MyLink");
             mylink.setAttribute("href", filename);
             mylink.click();
             return false;
         }
         function OnSuccessloadword() {
             return false;
         }
         function Onfailureloadword() {
             alert('error loading document');
             return false;
         }

         var linksid = "";
         function clearmovelinks() {
             $('#mymodalmovelink').modal('hide');
         }
         function OnSuccessmovelink(selectvalues) {
             $('#mymodalmovelink').modal('show');

             document.getElementById('select_alltabs').innerHTML = selectvalues;
             document.getElementById('select_moveto').innerHTML = "<option value=\"1\" selected=\"selected\">Move To</option><option value=\"2\">Copy To</option>";
         }
         function OnSuccessmovelinkadmin(selectvalues) {
             $('#mymodalmovelink').modal('show');

             document.getElementById('select_alltabs').innerHTML = selectvalues;
             document.getElementById('select_moveto').innerHTML = "<option value=\"2\" selected=\"selected\">Copy To</option>";
         }
         function OnSuccesslinksmove(status) {
             $('#loading').hide();
             if (status == true) {
                 debugger;
                 document.getElementById('divbox_' + linksid).style.display = "none";
                 alert('Link moved successfully');
                 $('#mymodalmovelink').modal('hide');
             }
             else {
                 alert('Contact admin');
             }
         }
         function onfailurelinkmoves() {
             $('#loading').hide();
             alert('Contact admin');
         }
         function OnSuccesslinkscopy(success) {
             $('#loading').hide();
             if (success == true) {
                 alert('Link Copied successfully');
                 $('#mymodalmovelink').modal('hide');
             }
             else {
                 alert('contact admin');
             }
         }

         function onfailurelinkcopy() {
             $('#loading').hide();
             alert('contact admin');
         }

         function submitmovelink() {
             debugger;
             if (linksid != "") {
                 var arraynew = new Array();
                 if (document.getElementById('select_alltabs').value != "0") {
                     arraynew[0] = linksid;
                     arraynew[1] = document.getElementById('select_moveto').value;
                     arraynew[2] = document.getElementById('select_alltabs').value;
                     arraynew[3] = document.getElementById('hid_userid').value;
                     if (document.getElementById('select_moveto').value == "1") {
                         $('#loading').show();
                         PageMethods.movelink(arraynew, OnSuccesslinksmove, onfailurelinkmoves);
                     }
                     else if (document.getElementById('select_moveto').value == "2") {
                         $('#loading').show();
                         PageMethods.copylink(arraynew, OnSuccesslinkscopy, onfailurelinkcopy);
                     }
                 }
                 else {
                     alert('Please select tab');
                     document.getElementById('select_alltabs').focus();
                 }
             }
         }


         function onfailuremovelink() {
             $('#loading').hide();
             alert('contact admin');
         }
         function onfailuremovelinkadmin() {
             $('#loading').hide();
             alert('contact admin');
         }
         function clearpass() {
             $('#loading').hide();
             $('#mymodalchangepass').modal('hide');
         }

         function openchangepassword() {
             debugger;
             //$('#mymodalsharetab').modal('show');
             //  $('#mymodalpassword').modal('show');
             document.getElementById('txt_oldpass').value == "";
             document.getElementById('txt_newpass').value == "";
             document.getElementById('txt_confirmpass').value == "";
         }
         function changepassword() {
             debugger;
             if (document.getElementById('txt_oldpass').value != "") {
                 if (document.getElementById('txt_newpass').value != "") {
                     if (document.getElementById('txt_confirmpass').value != "") {

                         if (document.getElementById('txt_oldpass').value != document.getElementById('txt_newpass').value) {




                             if (document.getElementById('txt_newpass').value == document.getElementById('txt_confirmpass').value) {
                                 var arraynew = new Array();
                                 arraynew[0] = document.getElementById('txt_oldpass').value;
                                 arraynew[1] = document.getElementById('txt_newpass').value;
                                 arraynew[2] = document.getElementById('hid_userid').value;
                                 $('#loading').show();
                                 PageMethods.passwordchange(arraynew, OnSuccesspasschange, onfailurepasschange);

                             }
                             else {
                                 alert('New Password and Confirm password are not same');
                             }
                         }
                         else {
                             alert('old password and new password are same');
                         }
                     }
                     else {
                         alert('Please enter confirm password');
                         document.getElementById('txt_confirmpass').focus();
                     }
                 }
                 else {
                     alert('Please enter new password');
                     document.getElementById('txt_newpass').focus();
                 }

             }
             else {
                 alert('Please enter old password');
                 document.getElementById('txt_oldpass').focus();
             }



         }

         function OnSuccesspasschange(status) {
             $('#loading').hide();
             if (status == "wrongpass") {
                 alert('Old password entered is wrong');
                 document.getElementById('txt_oldpass').focus();
             }
             else {
                 if (status == "true") {
                     alert('Password Changed Successfully');
                     $('#mymodalchangepass').modal('hide');
                     document.getElementById('txt_oldpass').value == "";
                     document.getElementById('txt_newpass').value == "";
                     document.getElementById('txt_confirmpass').value == "";
                 }
                 else {
                     alert('contact admin');
                 }
             }
         }
         function onfailurepasschange() {
             $('#loading').hide();
             alert('contact admin');
         }
         function clearsharetabcontrols() {
             $('#loading').hide();
             ('#mymodalsharetableft').modal('hide');
             document.getElementById('txt_sharetableft').value = "";
             sharevalue = "";
         }
         function clearsharetabcontrolsleft() {
             $('#loading').hide();
             ('#mymodalsharetab').modal('hide');
             document.getElementById('txt_sharetab').value = "";
         }
         function submitsharetab() {
             debugger;
             if (document.getElementById('txt_sharetab').value != '') {
                 var array = document.getElementById('hid_tab').value.split('_');
                 var id = array[1];
                 var arraynew = new Array();
                 arraynew[0] = id.toString();
                 arraynew[1] = document.getElementById('txt_sharetab').value;
                 arraynew[2] = array[0];
                 arraynew[3] = document.getElementById('hid_userid').value;
                 $('#loading').show();
                 PageMethods.tabshare(arraynew, OnSuccesstabshare, onfailuretabshare);
             }
             else {
                 alert('Please enter EMAILID');
                 document.getElementById('txt_sharetab').focus();
             }
         }
         function submitsharetableft() {
             debugger;
             if (document.getElementById('txt_sharetableft').value != '') {
                 var array = sharevalue.split('_');
                 var id = array[1];
                 var arraynew = new Array();
                 arraynew[0] = id.toString();
                 arraynew[1] = document.getElementById('txt_sharetableft').value;
                 arraynew[2] = array[0];
                 arraynew[3] = document.getElementById('hid_userid').value;
                 $('#loading').show();
                 PageMethods.tabshare(arraynew, OnSuccesstabshareleft, onfailuretabshare);
             }
             else {
                 alert('Please enter EMAILID');
                 document.getElementById('txt_sharetableft').focus();
             }
         }
         function OnSuccesstabshare(status) {
             $('#loading').hide();
             debugger;
             //    if (status != 'true') {
             //    alert(""+ status +" EmailIDS is not our registered clients")
             //  }
             //  else {
             if (status == 'true') {
                 alert('Tab shared successfully');
                 $('#mymodalsharetab').modal('hide');
                 sharetabemail();
             }
             else {
                 alert('contact admin');
             }
             // }
         }
         function OnSuccesstabshareleft(status) {
             $('#loading').hide();
             debugger;
             //    if (status != 'true') {
             //    alert(""+ status +" EmailIDS is not our registered clients")
             //  }
             //  else {
             if (status == 'true') {
                 alert('Tab shared successfully');
                 $('#mymodalsharetableft').modal('hide');
                 sharetabemailleft();
             }
             else {
                 alert('contact admin');
             }
             // }
         }
         function onfailuretabshare() {
             $('#loading').hide();
             alert('Plese contact administrator');
         }

         function sharetabemail() {
             var array = document.getElementById('hid_tab').value.split('_');
             var id = array[1];
             var arraynew = new Array();
             arraynew[0] = id.toString();
             arraynew[1] = document.getElementById('txt_sharetab').value;
             arraynew[2] = array[0];
             arraynew[3] = document.getElementById('hid_userid').value;

             PageMethods.tabshareemail(arraynew, OnSuccesstabshareemail, onfailuretabshareemail);
         }
         function sharetabemailleft() {
             var array = sharevalue.split('_');
             var id = array[1];
             var arraynew = new Array();
             arraynew[0] = id.toString();
             arraynew[1] = document.getElementById('txt_sharetableft').value;
             arraynew[2] = array[0];
             arraynew[3] = document.getElementById('hid_userid').value;

             PageMethods.tabshareemail(arraynew, OnSuccesstabshareemail, onfailuretabshareemail);
         }
         function OnSuccesstabshareemail(status) {

         }
         function onfailuretabshareemail() {

         }

         function Sharetotaltab() {

             debugger;
             document.getElementById('txt_sharetab').value = "";
             var array = document.getElementById('hid_tab').value.split('_');
             var id = array[1];
             document.getElementById('h_sharetab').innerHTML = "Share " + document.getElementById("a_" + id + "").title + " Tab With Friends";
             $('#mymodalsharetab').modal('show');
         }
         function Sharetotaltableft(value) {

             debugger;
             document.getElementById('txt_sharetableft').value = "";
             sharevalue = value;
             var array = value.split('_');
             var id = array[1];
             document.getElementById('h_sharetableft').innerHTML = "Share " + document.getElementById("a_" + id + "").title + " Tab With Friends";
             $('#mymodalsharetableft').modal('show');
         }
         //}

         var linksid = "";

         function movelinks(lid) {
             countdelete = 0;
             fbdelete = 0;
             twidelete = 0;
             sharefriend = 0;
             movelink = 0;
             linksid = lid.toString();
             var array = document.getElementById('hid_tab').value.split('_');
             var id = array[1];

             $('#loading').hide();
             PageMethods.Gettabs(id + '@' + document.getElementById('hid_userid').value, OnSuccessmovelink, onfailuremovelink);
         }
         function movelinksadmin(lid) {
             countdelete = 0;
             fbdelete = 0;
             twidelete = 0;
             sharefriend = 0;
             movelink = 0;
             linksid = lid.toString();
             var array = document.getElementById('hid_tabadmin').value.split('_');
             var id = array[1];

             $('#loading').hide();
             PageMethods.Gettabs(id, OnSuccessmovelinkadmin, onfailuremovelinkadmin);
         }
         function openkartshare(id) {
             debugger;
             countdelete = 0;
             fbdelete = 0;
             twidelete = 0;
             sharefriend = 0;
             movelink = 0;

             document.getElementById('main').value = "";

             $('#mymodalshare').modal('show');
             linksid = id.toString();
         }
         function clearsharecontrolscontrols() {
             document.getElementById('main').value = "";
             $('#mymodalshare').modal('hide');
         }
         function submitshare() {
             if (document.getElementById('main').value != '') {
                 var arraynew = new Array();
                 arraynew[0] = linksid;
                 arraynew[1] = document.getElementById('main').value;
                 var array = document.getElementById('hid_tab').value.split('_');
                 arraynew[2] = array[0];
                 arraynew[3] = document.getElementById('hid_userid').value;
                 $('#loading').show();
                 PageMethods.linkshare(arraynew, OnSuccesslinkshare, onfailureshare);
             }
             else {
                 alert('Please enter EMAILID');
                 document.getElementById('main').focus();
             }
         }
         function OnSuccesslinkshare(status) {
             $('#loading').hide();
             //if (status == 'invaliduser') {
             //    alert('Given EmailID is not our registered client')
             //}
             //else {
             if (status == 'true') {
                 alert('Link shared successfully');
                 $('#mymodalshare').modal('hide');
                 linkshareemail();
             }
             else {
                 alert('contact admin');
             }
             //}
         }
         function onfailureshare() {
             $('#loading').hide();
             alert('contact admin');
         }
         function linkshareemail() {
             var arraynew = new Array();
             arraynew[0] = linksid;
             arraynew[1] = document.getElementById('main').value;
             var array = document.getElementById('hid_tab').value.split('_');
             arraynew[2] = array[0];
             arraynew[3] = document.getElementById('hid_userid').value;

             PageMethods.linkshareemail(arraynew, OnSuccesslinkshareemail, onfailureshareemail);
         }
         function OnSuccesslinkshareemail() {

         }
         function onfailureshareemail() {

         }
         function deletevisible() {
             $('#loading').hide();
             if ((document.getElementById('hid_userid')).value != "22") {
                 $('.deleteadmin').hide();

             }

         }
         var countdelete = 1;
         var fbdelete = 1;
         var twidelete = 1;
         var sharefriend = 1;
         var movelink = 1;
         function opentwitter(id) {
             debugger;

             countdelete = 0;
             fbdelete = 0;
             twidelete = 0;
             sharefriend = 0;
             movelink = 0;
             window.open("http://linkskart.com/frm_twitter.aspx?twitterid=" + id + "", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes,top=100,  width=700, height=250");

         }
         function OpenInNewTab(url, id) {
             debugger;
             debugger;
             if (id != document.getElementById('hid_deleteid').value && countdelete != 0 && fbdelete != 0 && twidelete != 0 && sharefriend != 0 && movelink != 0) {
                 //if (countdelete != 1)
                 //    {
                 var win = window.open(url, '_blank');
                 win.focus();
                 countdelete = 1;
                 fbdelete = 1;
                 twidelete = 1;
                 sharefriend = 1;
                 movelink = 1;
             }
             else {
                 countdelete = 1;
                 fbdelete = 1;
                 twidelete = 1;
                 sharefriend = 1;
                 movelink = 1;
             }
             // }

         }

         function deletebox(id) {

             debugger;
             var confirmnew = confirm("Do you want to delete this link?")

             if (confirmnew) {
                 document.getElementById(id).style.display = "none";
                 document.getElementById('hid_deleteid').value = id;
                 countdelete = 0;
                 fbdelete = 0;
                 twidelete = 0;
                 sharefriend = 0;
                 movelink = 0;
                 $('#loading').show();
                 PageMethods.delete(document.getElementById('hid_deleteid').value, OnSuccessdelete, onfailuredelete);

             }
             else {
                 countdelete = 0;
                 fbdelete = 0;
                 twidelete = 0;
                 sharefriend = 0;
                 movelink = 0;
             }

         }
         function OnSuccessdelete(status) {
             $('#loading').hide();
             return false;
         }
         function onfailuredelete() {
             $('#loading').hide();
             return false;
         }
         function selecttabadmin(value) {
             if (value != '') {
                 debugger;
                 document.getElementById('hid_tabadmin').value = value;
                 var newvalue = "";

                 if (document.getElementById('hid_remainingtabsadmin').value != "") {
                     newvalue = value + "_" + document.getElementById('hid_remainingtabsadmin').value;
                 }
                 else {
                     newvalue = value + "_" + "0";
                 }
                 $('#loading').show();
                 PageMethods.gettabcontentadmin(newvalue, OnlinksSuccessadmin, onlinkfailureadmin);
             }
         }
         function OnlinksSuccessadmin(tab) {
             $('#loading').hide();
             if (tab != '') {

                 //var array = document.getElementById('hid_tab').value.split('_');

                 //if (array.length == 2) {
                 debugger;
                 var arraymain = tab.split('@!~&*');
                 if (arraymain.length == 4) {
                     if (arraymain[2] != "invalid") {
                         document.getElementById('hid_tabadmin').value = arraymain[0];
                         document.getElementById('u_admintabs').innerHTML = arraymain[1];
                         document.getElementById('div_admintabs').innerHTML = arraymain[2];
                         document.getElementById('hid_remainingtabsadmin').value = arraymain[3];
                     }
                     else {
                         document.getElementById(arraymain[1]).innerHTML = arraymain[0];
                     }

                 }
                 return false;
             }
         }
         function onlinkfailureadmin() {
             $('#loading').hide();
             return false;
         }

         function selecttab(value) {

             if (value != '') {

                 debugger;
                 document.getElementById('hid_tab').value = value;
                 // var newvalue = "";

                 //if (document.getElementById('hid_remainingtabs').value != "") {
                 //    newvalue = value + "_" + document.getElementById('hid_remainingtabs').value;
                 //}
                 //else {
                 //    newvalue = value + "_" + "0";
                 //}
                 $('#loading').show();
                 PageMethods.gettabcontent(document.getElementById('hid_tab').value, OnlinksSuccess, onlinkfailure);
             }
         }
         function OnlinksSuccess(result) {
             debugger;

             $('#loading').hide();
             if (result != '') {

                 //var array = document.getElementById('hid_tab').value.split('_');

                 //if (array.length == 2) {
                 debugger;
                 var arraymain = result.split('@!~&*');
                 if (arraymain.length == 2) {
                     //if (arraymain[2] != "invalid") {
                     document.getElementById('hid_tab').value = arraymain[1];
                     // document.getElementById('tabs').innerHTML = arraymain[1];
                     document.getElementById('div_tabcontent').innerHTML = arraymain[0];
                     // document.getElementById('hid_remainingtabs').value = arraymain[3];
                     //}
                     //else {
                     //    document.getElementById(arraymain[1]).innerHTML = arraymain[0];
                     //    //document.getElementById('hid_saving').style.display = "none";
                     //}

                 }

             }
             document.getElementById('txt_url').focus();
             debugger;
             var array = document.getElementById('hid_tab').value.split('_');
             var id = array[1];
             var element = document.getElementById('lbn_' + id);
             if (typeof (element) != 'undefined' && element != null) {
                 document.getElementById('lbn_' + id).style.display = "none";

                 // exists.
             }

         }
         function onlinkfailure() {
             $('#loading').hide();
             return false;
         }
         function buttonclick() {
             debugger;

             if (document.getElementById('txt_url').value != '') {

                 html = "";
                 //document.getElementById('txt_title').value = "";
                 //document.getElementById('txt_desc').value = "";
                 document.getElementById('hid_saving').style.display = "block";
                 var arraynew = new Array();
                 var split = document.getElementById('hid_tab').value.split('_');
                 if (split.length == 2) {
                     arraynew[0] = document.getElementById('txt_url').value;
                     arraynew[1] = split[0];
                     arraynew[2] = split[1];
                     $('#loading').show();
                     PageMethods.buttonsaveclick(arraynew, OnSuccess, onfailure);
                 }
             }
             else {
                 alert('Please enter URL');
                 document.getElementById('txt_url').focus();
             }
         }

         var html = "";
         var html = "";
         var description = "";
         var title = "";
         var image = "";
         var url = "";
         var hostname = "";
         var type = "";
         function OnSuccess(content) {
             $('#loading').hide();
             debugger;
             if (content != 'invalid') {
                 debugger;
                 // var content = document.getElementById('hid_html').value;
                 if (content != '') {
                     html = "";
                     description = "";
                     title = "";
                     image = "";
                     url = "";
                     hostname = "";
                     type = "";
                     //var array = document.getElementById('hid_tab').value.split('_');

                     var array = content.split('!@#');
                     description = array[3];
                     title = array[2];
                     image = array[1];
                     url = array[4];
                     hostname = array[5];
                     type = array[0];
                     //if (array.length == 6) {

                     if (array[0] == "3") {


                         if (array[1] != "0") {
                             document.getElementById('div_img').innerHTML = "<img src=\"" + array[1] + "\" alt=\"\"  style=\"width: 218px;height: 135px; margin-top:25px;\">";
                             document.getElementById('div_img').style.display = "block";
                             document.getElementById('txt_title').style.width = "320px";
                             document.getElementById('txt_desc').style.width = "320px";
                             document.getElementById('txt_title').disabled = false;
                             document.getElementById('txt_desc').disabled = false;
                             if (array[2] != "0") {
                                 document.getElementById('txt_title').value = array[2];
                                 if (array[3] != "0") {
                                     document.getElementById('txt_desc').value = array[3];
                                 }
                                 else {
                                     document.getElementById('txt_desc').value = array[4];
                                 }
                             }
                             else {
                                 document.getElementById('txt_title').value = array[5];
                             }
                         }
                         else {

                             document.getElementById('div_img').style.display = "none";
                             document.getElementById('txt_title').style.width = "533px";
                             document.getElementById('txt_desc').style.width = "533px";
                             document.getElementById('txt_title').disabled = false;
                             document.getElementById('txt_desc').disabled = false;
                             if (array[2] != "0") {
                                 document.getElementById('txt_title').value = array[2];
                                 if (array[3] != "0") {
                                     document.getElementById('txt_desc').value = array[3];
                                 }
                                 else {
                                     document.getElementById('txt_desc').value = array[4];
                                 }
                             }
                             else {
                                 document.getElementById('txt_title').value = array[5];
                             }
                         }

                     }
                     else {

                         document.getElementById('div_img').style.display = "block";
                         document.getElementById('txt_title').style.width = "320px";
                         document.getElementById('txt_desc').style.width = "320px";
                         document.getElementById('div_img').innerHTML = "<iframe width=\"192\" height=\"198\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + array[1] + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                         document.getElementById('txt_title').value = array[2];
                         document.getElementById('txt_desc').value = array[3];


                     }

                     $('#mymodalsave').modal('show');
                     document.getElementById("btn_sub").focus();
                     debugger;

                 }

                 // document.getElementById('txt_url').value = '';
             }
             else {
                 alert('Enter a valid URL');
                 document.getElementById('txt_url').focus();
             }

             document.getElementById('hid_saving').style.display = "none";
             return false;

         }
         function onfailure() {
             $('#loading').hide();
         }
         function clearsavecontrols() {
             description = "";
             title = "";
             image = "";
             url = "";
             hostname = "";
             html = "";
             document.getElementById('hid_saving').style.display = "none";
             document.getElementById('div_img').style.display = "none";
             document.getElementById('txt_title').style.width = "533px";
             document.getElementById('txt_desc').style.width = "533px";
             document.getElementById('txt_title').disabled = false;
             document.getElementById('txt_desc').disabled = false;
             $('#mymodalsave').modal('hide');
             document.getElementById('txt_url').value = '';
             document.getElementById('hid_saving').style.display = "none";
         }
         function submitsave() {
             debugger;
             var arraynew = new Array();
             html = type + "!@#" + image + "!@#" + document.getElementById('txt_title').value + "!@#" + document.getElementById('txt_desc').value + "!@#" + url + "!@#" + hostname;

             var split = document.getElementById('hid_tab').value.split('_');
             if (split.length == 2) {
                 arraynew[0] = document.getElementById('txt_url').value;
                 arraynew[1] = split[0];
                 arraynew[2] = split[1];
                 arraynew[3] = html;
                 $('#loading').show();
                 PageMethods.buttonsaveNewclick(arraynew, OnSuccesssaveclicknew, onfailuresavenew);
             }
         }
         function OnSuccesssaveclicknew(content) {
             debugger;
             $('#loading').hide();
             if (content != '') {

                 var array = document.getElementById('hid_tab').value.split('_');

                 if (array.length == 2) {
                     var id = array[1];

                     document.getElementById('div_tabcontent').innerHTML = content + document.getElementById('div_tabcontent').innerHTML;
                     // document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                 }
                 $('#mymodalsave').modal('hide');
                 document.getElementById('txt_url').value = '';

             }


         }
         function onfailuresavenew() {
             $('#loading').hide();

         }
         function buttonenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 buttonclick();

             }
         }
         function buttonshareenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 submitshare();

             }
         }
         function buttonsharetabenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 submitsharetab();

             }
         }
         function buttonsharetabenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 submitsharetableft();

             }
         }
         function buttontabenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 savetabname();

             }
         }
         function buttontabenterleft(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 savetabnameleft();

             }
         }
         function buttonsavetabenter(e) {
             var key = e.keyCode || e.which;
             if (key == 13) {
                 debugger;
                 savetab();

             }
         }
         function savetab() {
             debugger;
             if (document.getElementById('txt_newtab').value != '') {
                 $('#loading').show();
                 var array = document.getElementById('hid_tab').value.split('_');
                 PageMethods.buttonsavetab(array[0] + '@' + document.getElementById('txt_newtab').value.replace('@', ''), OnSuccesssavetab, onfailuresavetab);
             }
             else {
                 alert('Please enter tab name');
             }
         }
         function OnSuccesssavetab(tab) {
             debugger;
             $('#loading').hide();
             if (tab != '') {
                 if (tab != 'alreadyexists') {
                     var array = tab.split('!@#');


                     document.getElementById('hid_tab').value = array[1];
                     document.getElementById('div_menus').innerHTML = "";
                     document.getElementById('div_menus').innerHTML = array[0];
                     document.getElementById('div_tabcontent').innerHTML = "";

                     document.getElementById('txt_newtab').value == "";
                     $('#mynewtab').modal('hide');
                     $('#div_menus').metisMenu();
                     //alert('Tab Saved sucessfully please refer to click here for more tabs to view this tab');
                 }
                 else {
                     alert('Tab with this name already exists please type another');
                 }
             }
         }
         function onfailuresavetab() {
             $('#loading').hide();
         }

    </script>
    <script>
        function saveurl() {

            debugger;
            //var content = document.getElementById('hid_html').value;
            if (content != '') {

                var array = document.getElementById('hid_tab').value.split('_');

                if (array.length == 2) {
                    var id = array[1];
                    document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                }
            }
            return false;
        }
        function opentabname() {

            var array = document.getElementById('hid_tab').value.split('_');
            var id = array[1];
            if (document.getElementById("a_" + id + "").title != 'Anonymous') {
                document.getElementById('txt_newtabchange').value = document.getElementById("a_" + id + "").title;
                $('#mymodalchangename').modal('show');
            }
            else {
                alert('Dear user you cannot change anonymous tab name');
            }
        }
        function opentabnameleft(value) {
            sharevalue = value;
            var array = value.split('_');

            var id = array[1];
            if (document.getElementById("a_" + id + "").title != 'Anonymous') {
                document.getElementById('txt_newtabchangeleft').value = document.getElementById("a_" + id + "").title;
                $('#mymodalchangenameleft').modal('show');
            }
            else {
                alert('Dear user you cannot change anonymous tab name');
            }
        }
        function deletetab() {
            var array = document.getElementById('hid_tab').value.split('_');
            var id = array[1];
            var result = confirm("Are you sure do you want to delete " + document.getElementById("a_" + id + "").title + " tab");
            if (result == true) {
                //Logic to delete the item


                if (document.getElementById("a_" + id + "").title != 'Anonymous') {

                    if (array.length == 2) {

                        document.getElementById("a_" + id + "").title = document.getElementById('txt_newtabchange').value;
                        // document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                        $('#loading').show();
                        PageMethods.buttondeletetab(id + '@' + document.getElementById('txt_newtabchange').value.replace('@', '') + '@' + array[0], OnSuccessdeletetab, onfailuredeletetab);
                    }
                }
                else {
                    alert('Dear user you cannot delete anonymous tab ');
                }
            }
        }
        function deletetableft(value) {
            sharevalue = value;
            var array = value.split('_');
            var id = array[1];
            var result = confirm("Are you sure do you want to delete " + document.getElementById("a_" + id + "").title + " tab");
            if (result == true) {
                //Logic to delete the item


                if (document.getElementById("a_" + id + "").title != 'Anonymous') {

                    if (array.length == 2) {

                        document.getElementById("a_" + id + "").title = document.getElementById('txt_newtabchange').value;
                        // document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                        $('#loading').show();
                        PageMethods.buttondeletetab(id + '@' + document.getElementById('txt_newtabchange').value.replace('@', '') + '@' + array[0], OnSuccessdeletetab, onfailuredeletetab);
                    }
                }
                else {
                    alert('Dear user you cannot delete anonymous tab ');
                }
            }
        }
        function OnSuccessdeletetab(content) {
            $('#loading').hide();
            var array = content.split('!@#');
            if (array[0] != 'invalid') {


                alert('Tab deleted successfully');
                location.href = "Linkskart.aspx";

            }
            else {
                alert('Dear user atleast one tab should be present');
            }
        }
        function onfailuredeletetab() {
            $('#loading').hide();

        }
        function savetabname() {
            debugger;
            if (document.getElementById('txt_newtabchange').value != '') {
                var array = document.getElementById('hid_tab').value.split('_');

                if (array.length == 2) {
                    var id = array[1];
                    document.getElementById("a_" + id + "").title = document.getElementById('txt_newtabchange').value;
                    // document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                    $('#loading').show();
                    PageMethods.buttonchangename(id + '@' + document.getElementById('txt_newtabchange').value.replace('@', ''), OnSuccesschangename, onfailurechangename);
                }
            }
            else {
                alert('Please enter tab name');
                document.getElementById('txt_newtabchange').focus();
            }
        }
        function savetabnameleft() {
            debugger;
            if (document.getElementById('txt_newtabchangeleft').value != '') {
                var array = sharevalue.split('_');

                if (array.length == 2) {
                    var id = array[1];
                    document.getElementById("a_" + id + "").title = document.getElementById('txt_newtabchangeleft').value;
                    // document.getElementById('tab' + id + 'default').innerHTML = content + document.getElementById('tab' + id + 'default').innerHTML;
                    $('#loading').show();
                    PageMethods.buttonchangename(id + '@' + document.getElementById('txt_newtabchangeleft').value.replace('@', ''), OnSuccesschangenameleft, onfailurechangenameleft);
                }
            }
            else {
                alert('Please enter tab name');
                document.getElementById('txt_newtabchangeleft').focus();
            }
        }
        function OnSuccesschangename(content) {
            $('#loading').hide();
            $('#mymodalchangename').modal('hide');
        }
        function OnSuccesschangenameleft(content) {
            $('#loading').hide();
            $('#mymodalchangenameleft').modal('hide');
        }
        function onfailurechangename() {
            $('#loading').hide();
        }
        function onfailurechangenameleft() {
            $('#loading').hide();
        }
    </script>
    <script src="https://connect.facebook.net/en_US/all.js">
</script>
    <script type="text/javascript">
        var i = 0;
        function share(name, link, caption, description, picture) {
            debugger;
            countdelete = 0;
            fbdelete = 0;
            twidelete = 0;
            sharefriend = 0;
            movelink = 0;
            FB.ui(
                              {
                                  method: 'feed',
                                  name: name,
                                  link: link,
                                  caption: caption,
                                  description: description,
                                  picture: picture,
                                  message: ''
                              },
            function (response) {
                if (response && response.post_id) {
                    // Do some custom action after the user successfully
                    // posts this to their wall
                    alert('Thanks for sharing!');
                }
            }

                              );


        }

        function sharevideo(name, link, caption, description, picture, source) {
            debugger;
            countdelete = 0;
            fbdelete = 0;
            twidelete = 0;
            sharefriend = 0;
            movelink = 0;
            FB.ui(
                              {
                                  method: 'feed',
                                  name: name,
                                  link: link,
                                  caption: caption,
                                  description: description,
                                  picture: picture,
                                  message: ''
                              },
        function (response) {
            if (response && response.post_id) {
                // Do some custom action after the user successfully
                // posts this to their wall
                alert('Thanks for sharing!');
            }
        }

        );


        }


</script>
    <script>
        FB.init({
            appId: '794873443927888', status: true, cookie: true,
            xfbml: true
        });
</script>
</head>

<body>
    <form runat="server" onsubmit="return false">
          <div id="loading">
<%--  <img id="loading-image" src="images/ajax-loader.gif" alt="Loading..." />--%>
</div>
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" EnableCdn="true" runat="server" />
     <asp:HiddenField runat="server" ID="hid_remainingtabs" />
                    <asp:HiddenField runat="server" ID="hid_deleteid" />
                    <asp:HiddenField runat="server" ID="hid_tab" />
                    <asp:HiddenField runat="server" ID="hid_userid" />
                    <asp:HiddenField runat="server" ID="hid_html" />
                     <asp:HiddenField runat="server" ID="hid_remainingtabsadmin" />
                    <asp:HiddenField runat="server" ID="hid_deleteidadmin" />
                    <asp:HiddenField runat="server" ID="hid_tabadmin" />

                    <asp:HiddenField runat="server" ID="hid_htmladmin" />
    <div id="wrapper">

        <nav class="navbar-default navbar-static-side" role="navigation">
            <div class="sidebar-collapse">
                <ul class="nav" runat="server" id="div_menus" >
                 




                </ul>

            </div>
        </nav>

         <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <nav class="navbar navbar-static-top  " role="navigation" style="margin-bottom: 0">
                    <div class="navbar-header">
                        <a onclick="hidemenu();" class="navbar-minimalize minimalize-styl-2 btn btn-primary " ><i class="fa fa-bars"></i></a>
                        <div  class="navbar-form-custom" >
                            <div class="form-group">
                                <input type="text" placeholder="Search for something..." onkeyup="searchlinks(event);" class="form-control" name="top-search" id="txt_searchkey">
                            </div>
                        </div>
                    </div>

                    <ul class="nav navbar-top-links navbar-right">
                        <%-- <li>
                    <span class="m-r-sm text-muted welcome-message">Welcome to INSPINIA+ Admin Theme.</span>
                </li>--%>



                        <li>
                           
                                
                                   <a onclick="logout();"><i class="fa fa-sign-out"></i> Log out</a>
                              <%--<asp:LinkButton  runat="server" ID="LinkButtonchange"  OnClick="LinkButtonchange_Click"><i class="fa fa-key"></i>Change Password</asp:LinkButton>--%>
                              
                           
                        </li>
                    </ul>

                </nav>
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-md-7" style="padding:10px 0px 10px 0px">
                     <div class="search">
                                   <%-- <div class="form-group">--%>
                                        <asp:TextBox runat="server" style="border:2px solid #18a689 !important" onkeypress="buttonenter(event);" ID="txt_url" class="form-control" placeholder="Enter URL Address" rel="tooltip" title="Enter URL Address and Save"></asp:TextBox>
                                        <%--<input type="email" id="txt_url" runat="server" class="form-control" placeholder="Enter URL Address" rel="tooltip" title="Enter URL Address and Save">--%>
                                       
                                    <%-- <button type="button" id="up" data-toggle="modal" data-target="#temp"  class="btn btn-sunny  btn-orangemain">Upload</button>--%>
                                           <%--<asp:Button runat="server" ID="btn_save"   OnClientClick="buttonclick();" class="btn btn-sunny  btn-orangemain" Text="Save" />--%>
                                        <div class="clearfix"></div>
                                      
                                   <%-- </div>--%>
                                    <h3 id="hid_saving" style="display: none" align="center">Saving...</h3>
                                </div>
                   
                </div>
                <div class="col-md-3">
                     <button type="button" id="btn_search" onclick="buttonclick();" style="margin-right:5px" class="btn btn-sunny  btn-orangemain">Save&Share</button>
                                    
                                        <input type="button" id="open_btn" class="btn btn-sunny  btn-orangemain" class="btn btn-primary" value="upload files">
                </div>
            </div>
            
           <%-- <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
             <script src="js/bootstrap.fd.js"></script>
             <link href="js/bootstrap.fd.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {

            $("#open_btn").click(function () {
                debugger;
                $.FileDialog({ multiple: true }).on('files.bs.filedialog', function (ev) {
                    var files = ev.files;
                    debugger;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    // data.append("user", (document.getElementById('hid_userid')).value);
                    var array = document.getElementById('hid_tab').value.split('_');
                    var id = array[1];
                    document.getElementById('open_btn').value = "Uploading...";
                    document.getElementById('open_btn').disabled = true;
                    $.ajax({
                        url: "FileUploadHandler.ashx?user=" + document.getElementById('hid_userid').value + "&tabid=" + id + "",
                        type: "POST",
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            debugger;

                            selecttab(document.getElementById('hid_tab').value);
                            document.getElementById('open_btn').value = "Upload Files";
                            document.getElementById('open_btn').disabled = false;
                            $('#loading').hide()
                        },
                        error: function (err) {
                            alert('unexpected error');
                            $('#loading').hide()
                        }
                    });
                }).on('cancel.bs.filedialog', function (ev) {
                    alert("Cancelled!");
                });
            });
        });
    </script>
            <div class="wrapper wrapper-content">
                <div class="row">

                    <div class="col-lg-12 animated fadeInRight">
                        <div class="row">
                            <div class="col-lg-12" runat="server" id="div_tabcontent">
                                <div class="file-box">
                                    <div class="file">
                                      <div class="container">
	<div class="row">
       
        
        
	</div>
</div>
                    
                                        <a href="#">

                                            <span class="corner"></span>
                                                                                <div class="dropdown pull-right " style="z-index:1000">
            <a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-primary" data-target="#" href="/page.html">
                <span class="caret"></span>
            </a>
    		<ul class="dropdown-menu multi-level" role="menu" aria-labelledby="dropdownMenu">
              <li><a href="#">Some action</a></li>
              <li><a href="#">Some other action</a></li>
                <li><a href="#">Some other action</a></li>
              
              <li >
                
               
              </li>
            </ul>
        </div>
                                            <div class="icon">
            
                                                <i class="fa fa-file"></i>
                                            </div>
                                            <div class="file-name">
                                               sujith is dream boy sujith is dream boy sujith is dream boy sujith is dream boy 
                                            
                                            </div>
                                        </a>
                                    </div>

                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>
                                            <div class="dropdown pull-right dropdownfilemanager" >
            <a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-primary" data-target="#" href="/page.html">
                <span class="caret"></span>
            </a>
    		<ul class="dropdown-menu multi-level" role="menu" aria-labelledby="dropdownMenu">
              <li><a href="#">Some action</a></li>
              <li><a href="#">Some other action</a></li>
                <li><a href="#">Some other action</a></li>
              
              <li >
                
               
              </li>
            </ul>
        </div>
                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p1.jpg">
                                            </div>
                                            <div class="file-name">
                                                Italy street.jpg
                                            <br />
                                                <small>Added: Jan 6, 2014</small>
                                            </div>
                                        </a>

                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p2.jpg">
                                            </div>
                                            <div class="file-name">
                                                My feel.png
                                            <br />
                                                <small>Added: Jan 7, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-music"></i>
                                            </div>
                                            <div class="file-name">
                                                Michal Jackson.mp3
                                            <br />
                                                <small>Added: Jan 22, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p3.jpg">
                                            </div>
                                            <div class="file-name">
                                                Document_2014.doc
                                            <br />
                                                <small>Added: Fab 11, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="img-responsive fa fa-film"></i>
                                            </div>
                                            <div class="file-name">
                                                Monica's birthday.mpg4
                                            <br />
                                                <small>Added: Fab 18, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <a href="#">
                                        <div class="file">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-bar-chart-o"></i>
                                            </div>
                                            <div class="file-name">
                                                Annual report 2014.xls
                                            <br />
                                                <small>Added: Fab 22, 2014</small>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-file"></i>
                                            </div>
                                            <div class="file-name">
                                                Document_2014.doc
                                            <br />
                                                <small>Added: Jan 11, 2014</small>
                                            </div>
                                        </a>
                                    </div>

                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p1.jpg">
                                            </div>
                                            <div class="file-name">
                                                Italy street.jpg
                                            <br />
                                                <small>Added: Jan 6, 2014</small>
                                            </div>
                                        </a>

                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p2.jpg">
                                            </div>
                                            <div class="file-name">
                                                My feel.png
                                            <br />
                                                <small>Added: Jan 7, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-music"></i>
                                            </div>
                                            <div class="file-name">
                                                Michal Jackson.mp3
                                            <br />
                                                <small>Added: Jan 22, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p3.jpg">
                                            </div>
                                            <div class="file-name">
                                                Document_2014.doc
                                            <br />
                                                <small>Added: Fab 11, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="img-responsive fa fa-film"></i>
                                            </div>
                                            <div class="file-name">
                                                Monica's birthday.mpg4
                                            <br />
                                                <small>Added: Fab 18, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <a href="#">
                                        <div class="file">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-bar-chart-o"></i>
                                            </div>
                                            <div class="file-name">
                                                Annual report 2014.xls
                                            <br />
                                                <small>Added: Fab 22, 2014</small>
                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-file"></i>
                                            </div>
                                            <div class="file-name">
                                                Document_2014.doc
                                            <br />
                                                <small>Added: Jan 11, 2014</small>
                                            </div>
                                        </a>
                                    </div>

                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p1.jpg">
                                            </div>
                                            <div class="file-name">
                                                Italy street.jpg
                                            <br />
                                                <small>Added: Jan 6, 2014</small>
                                            </div>
                                        </a>

                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p2.jpg">
                                            </div>
                                            <div class="file-name">
                                                My feel.png
                                            <br />
                                                <small>Added: Jan 7, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-music"></i>
                                            </div>
                                            <div class="file-name">
                                                Michal Jackson.mp3
                                            <br />
                                                <small>Added: Jan 22, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="image">
                                                <img alt="image" class="img-responsive" src="img/p3.jpg">
                                            </div>
                                            <div class="file-name">
                                                Document_2014.doc
                                            <br />
                                                <small>Added: Fab 11, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <div class="file">
                                        <a href="#">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="img-responsive fa fa-film"></i>
                                            </div>
                                            <div class="file-name">
                                                Monica's birthday.mpg4
                                            <br />
                                                <small>Added: Fab 18, 2014</small>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                                <div class="file-box">
                                    <a href="#">
                                        <div class="file">
                                            <span class="corner"></span>

                                            <div class="icon">
                                                <i class="fa fa-bar-chart-o"></i>
                                            </div>
                                            <div class="file-name">
                                                Annual report 2014.xls
                                            <br />
                                                <small>Added: Fab 22, 2014</small>
                                            </div>
                                        </div>
                                    </a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer">
                <div class="pull-right">
                  
                </div>
                <div>
                    <strong>Copyright</strong> Linkskart &copy; 2014-2015
                </div>
            </div>

        </div>
    </div>
          <!--My Personal dairy Modal Popup starts -->
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content" style="background: #d7d0ca">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        <span aria-hidden="true">×</span>
                                        <span class="sr-only">Close</span>
                                    </button>
                                    <h4 class="modal-title text-center" id="myModalLabel">Dairy</h4>
                                </div>
                                <div class="modal-body">
                                    <!--<form action="">
                    <div class="form-group">
                        <div class="col-md-4">
                            <label>Title</label>
                        </div>
                        <div class="col-md-8">
                            <input placeholder="Enter Title" type="text" class="form-control">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="form-group">
                       <div class="col-md-4">
                            <label>Uplaod Image</label>
                        </div>
                        <div class="col-md-8">
                            <input  type="file" class="form-control">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="form-group">
                       <div class="col-md-4">
                            <label>Description</label>
                        </div>
                        <div class="col-md-8">
                           <textarea class="form-control" rows="6" placeholder="Enter Discription" ></textarea>
                        </div>
                        <div class="clearfix"></div>
                    </div>                 
                    <div class="form-group text-center">
                        <input type="submit" class="btn btn-success  btn-login-submit" value="Submit">
                    </div>
                </form>-->



                                    <img src="images/3.jpg" width="300" style="float: left; margin: 0 10px 0px 0; text-align: justify;" />
                                    <p style="text-align: justify;">
                                        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,
                                    </p>
                                    <div class="clearfix">&nbsp;</div>
                                    <div class="col-md-12">
                                        <a href="#" class="btn btn-success btn-lg" style="background: yellow"></a>Happy
                   
                            <a href="#" class="btn btn-primary btn-lg" style="padding"></a>Routine day
                   
                            <a href="#" class="btn btn-danger btn-lg" style="padding"></a>Sad
               
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--My Personal dairy Modal Popup starts -->

                    <!--My Calender Modal Popup starts -->
                    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background: #333; color: #000">
                                    <button type="button" class="close" data-dismiss="modal">
                                        <span aria-hidden="true" style="color: #fff; background: red; padding: 5px; border-radius: 3px;">×</span>
                                        <span class="sr-only">Close</span>
                                    </button>
                                    <h4 class="modal-title text-center" id="myModalLabel" style="background: #333; color: #fff">My Calender</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="cal1">
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <!--My Calender Modal Popup starts -->

                    <!--forgot password Modal Popup starts -->
                    
                    <!--forgot password Modal Popup starts -->


        <div class="modal fade" id="temp" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title text-center" id="myModalLabel">Create new tab</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row" id="div_uploaddocs">
                                       
                                 <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                    <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                         <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                         <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                         <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                         <div class="box" id="divbox_3754" style="cursor:pointer"><div class="boximage">
                                     <img src="http://cdn3.cdntrendin.com/img/p/18202-60949-large.jpg" />
                                                                                          </div>
                                     <div class="boxtext"> <textarea style="width:255px" placeholder="Add Title" cols="4"  class="form-control" rows="2" ></textarea>

                                     </div>
                                     </div>
                                          <%-- <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/1/" alt="...">
            </div>
        </div>
      
        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/2/" alt="...">
            </div>
        </div>
<div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/1/" alt="...">
            </div>
        </div>
      
        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/2/" alt="...">
            </div>
        </div>
      <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/1/" alt="...">
            </div>
        </div>
      
        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/2/" alt="...">
            </div>
        </div>
                                        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/1/" alt="...">
            </div>
        </div>
      
        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/2/" alt="...">
            </div>
        </div>
                                        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/1/" alt="...">
            </div>
        </div>
      
        <div class="col-xs-6 col-sm-4 col-md-3">            
            <div class="thumbnail">
               
                <img src="http://lorempixel.com/400/300/sports/2/" alt="...">
            </div>
        </div>--%>
    </div>
    
    
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <a type="button" >Save</a>
                                    <input type="button" class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade" id="mynewtab" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title text-center" id="myModalLabel">Create new tab</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-10 col-md-offset-1">
                                        <div class="form-group">
                                            <div class="col-md-4" style="margin-top: 5px">
                                                <label>Entar Tab Name</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="text" id="txt_newtab" maxlength="20" class="form-control" onkeyup="buttonsavetabenter(event);"   placeholder="Enter tab name" width="120px"/>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <a type="button" onclick="savetab();" class="btn btn-success btn-sm">Save</a>
                                    <input type="button" class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade" id="mymodalchangename" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title text-center" id="myModalLabel">Change tab name</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-10 col-md-offset-1">
                                        <div class="form-group">
                                            <div class="col-md-4" style="margin-top: 5px">
                                                <label>Tab Name</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="text" width: "120px;" maxlength="12" id="txt_newtabchange" onkeyup="buttontabenter(event);"  placeholder="Enter tab name" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <a type="button" onclick="savetabname();" class="btn btn-success btn-sm">Save</a>
                                    <input type="button" class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                                </div>
                            </div>
                        </div>
                    </div>
        <div class="modal fade" id="mymodalchangenameleft" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title text-center" id="myModalLabelleft">Change tab name</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-10 col-md-offset-1">
                                        <div class="form-group">
                                            <div class="col-md-4" style="margin-top: 5px">
                                                <label>Tab Name</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="text" width: "120px;" maxlength="12" id="txt_newtabchangeleft" onkeyup="buttontabenterleft(event);" style="width: 140px;"  placeholder="Enter tab name" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <a type="button" onclick="savetabnameleft();" class="btn btn-success btn-sm">Save</a>
                                    <input type="button" class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="mymodalsave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        <span aria-hidden="true">×</span>
                                        <span class="sr-only">Close</span>
                                    </button>
                                    <h4 class="modal-title text-center" id="myModalLabel" style="color: #000">Your URL Details</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-5" id="div_img">
                                        <img src="images/2.jpg" alt="" style="width: 218px; height: 135px; margin-top: 25px;">
                                    </div>
                                    <div class="col-md-7">
                                        <div class="form-group">
                                            <label for="comment">Title</label>
                                            <textarea id="txt_title"  class="form-control" rows="2" ></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label for="comment">Description</label>
                                            <textarea id="txt_desc"  class="form-control" rows="3" ></textarea>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <%--<asp:Button id="btn_sub" runat="server" class="btn btn-success btn-sm" OnClientClick="submitsave();return false;" Text="Submit" /> --%>
                                    <a type="button" id="btn_sub"  class="btn btn-success btn-sm" onclick="submitsave();">Submit</a>
                                    <input type="button" class="btn btn-danger btn-sm" onclick="clearsavecontrols();"  value="Close">
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
           <div class="modal fade" id="mymodalshare" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
             
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="myModalLabel">Share with Friends</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-10 col-md-offset-1" style="width:290px">
                            <div class="col-md-4" style="margin-top:6px;">
                                <label>Email ID</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="display:inline-flex">
                                    <input id="main" onkeypress="buttonshareenter(event);"  style="background-color:none"  type="text" class="form-control" placeholder="Enter EmailID">
                                      
                                 
                                </div>
                            </div>
                             <h5 style="">Enter EmailID's with comma(,) seperated</h5>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        
                       <%-- <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Reset Password</a>--%>
                         <a type="button" id="btn_sub"  class="btn btn-success btn-sm" onclick="submitshare();">Share</a>
                                    <input type="button" class="btn btn-danger btn-sm" onclick="clearsharecontrolscontrols();"  value="Close">
                        
                    </div>
                </div>
                    
            </div>
        </div>
        <div class="modal fade" id="mymodalmovelink" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
             
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center">Move or Copy Link</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-10 col-md-offset-1" style="width:390px">

                            <div class="col-md-4" style="margin-top:0px;">
                                   <select id="select_moveto" class="form-control" style="width:110px">
  <option value="1" selected="selected">Move To</option>
  <option value="2">Copy To</option>
  
</select>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="display:inline-flex">
                                    <select id="select_alltabs" class="form-control">
  <option value="Select Tab">Select Tab</option>
 
</select>
                                 
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        
                       <%-- <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Reset Password</a>--%>
                         <a type="button" id="btn_sub"  class="btn btn-success btn-sm" onclick="submitmovelink();">Submit</a>
                                    <input type="button" class="btn btn-danger btn-sm" onclick="clearmovelinks();"  value="Close">
                        
                    </div>
                </div>
                    
            </div>
        </div>
        <div class="modal fade" id="mymodalsharetab" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
             
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="h_sharetab">Share tab with Friends</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-10 col-md-offset-1" style="width:290px">
                            <div class="col-md-4" style="margin-top:6px;">
                                <label>Email ID</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="display:inline-flex">
                                    <input id="txt_sharetab" onkeypress="buttonsharetabenter(event);"  style="background-color:none"  type="text" class="form-control" placeholder="Enter EmailID">
                                      
                                 
                                </div>
                                
                            </div>
                            <h5 style="">Enter EmailID's with comma(,) seperated</h5>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        
                       <%-- <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Reset Password</a>--%>
                         <a type="button" id="btn_sub"  class="btn btn-success btn-sm" onclick="submitsharetab();">Share</a>
                                    <input type="button" data-dismiss="modal" class="btn btn-danger btn-sm" onclick="clearsharetabcontrols();"  value="Close">
                        
                    </div>
                </div>
                    
            </div>
        </div>
        <div class="modal fade" id="mymodalsharetableft" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
             
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="h_sharetableft">Share tab with Friends</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-10 col-md-offset-1" style="width:290px">
                            <div class="col-md-4" style="margin-top:6px;">
                                <label>Email ID</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="display:inline-flex">
                                    <input id="txt_sharetableft" onkeypress="buttonsharetabenterleft(event);"  style="background-color:none"  type="text" class="form-control" placeholder="Enter EmailID">
                                      
                                 
                                </div>
                                
                            </div>
                            <h5 style="">Enter EmailID's with comma(,) seperated</h5>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        
                       <%-- <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Reset Password</a>--%>
                         <a type="button" id="btn_sub"  class="btn btn-success btn-sm" onclick="submitsharetableft();">Share</a>
                                    <input type="button" data-dismiss="modal" class="btn btn-danger btn-sm" onclick="clearsharetabcontrolsleft();"  value="Close">
                        
                    </div>
                </div>
                    
            </div>
        </div>
           <div class="modal fade" id="mymodalchangepass" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
             
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="h_sharetab">Change Password</h4>
                    </div>
                      <div class="modal-body">
                                    <div class="col-md-10 col-md-offset-1">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <label>Old Password</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="password" id="txt_oldpass" name="email" placeholder="Enter Old Password" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <label>New Password</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="password" id="txt_newpass" name="email" placeholder="Enter New confirm Password" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                            <div class="form-group">
                                            <div class="col-md-4">
                                                <label>Confirm Password</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <input type="text" id="txt_confirmpass" name="email" placeholder="Enter confirm Password" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">
                                    <a type="button" class="btn btn-success btn-sm" onclick="changepassword();" >Submit</a>
                                    <input type="button" class="btn btn-danger btn-sm" onclick="clearpass();"  value="Close">
                                </div>
                </div>
                    
            </div>
        </div>

    <!-- Mainly scripts -->
  <%--  <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>--%>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <script>
        $(document).ready(function () {
            $('.file-box').each(function () {
                animationHover(this, 'pulse');
            });
        });
    </script>
        </form>
</body>
</html>
