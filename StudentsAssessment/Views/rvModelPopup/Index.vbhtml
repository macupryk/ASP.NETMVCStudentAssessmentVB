﻿
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>rv-vanilla-modal: a tiny modal module made with vanilla javascript - no jQuery dependency.</title>
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,500,800' rel='stylesheet' type='text/css'>
    
    <link href="~/Content/rvModel/css/styles.css" rel="stylesheet" />
    <script src="~/Content/rvModel/js/rv-vanilla-modal.js"></script>
</head>
<body>
    <div id="main">
        <header class="main-header">
            <h1>rv-vanilla-modal</h1>
            <h2>A tiny modal module made with vanilla javascript - no jQuery dependency.</h2>
        </header>
        <div class="main-content">
            <div class="center-button-wrapper">
                <button data-rv-vanilla-modal="#about-modal" class="button">Show modal</button>
                <button data-rv-vanilla-modal="#SomeOther-modal" class="button">Show another modal</button>
            </div>
        </div>
        <span id="spnName"></span>
    </div>
</body>
<div id="about-modal" class="rv-vanilla-modal">
    <div class="rv-vanilla-modal-header group">
        <button class="rv-vanilla-modal-close"><span class="text">×</span></button>
        <h2 class="rv-vanilla-modal-title">About</h2>
    </div>
    <div class="rv-vanilla-modal-body">
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ipsum sed, eveniet, odit recusandae facilis voluptate iste commodi eligendi amet molestiae corporis magnam facere et temporibus cumque molestias aperiam laborum tenetur.</p>
    </div>
</div>

<div id="SomeOther-modal" class="rv-vanilla-modal">
    <div class="rv-vanilla-modal-header group">
        <button class="rv-vanilla-modal-close"><span class="text">×</span></button>
        <h2 class="rv-vanilla-modal-title">Type your name and click OK</h2>
    </div>
    <div class="rv-vanilla-modal-body">
        <p>
            Enter your name:<br />
            <input id="txtName" type="text" /><br />
            <button id="btnOK" onclick="var strName=document.getElementById('txtName');spnName.innerText=strName.value;">OK</button>

    </div>
</div>
<script>
document.addEventListener('DOMContentLoaded', function() {
    /* global RvVanillaModal */
    'use strict';
    var modal = new RvVanillaModal({
        //showOverlay: true
    });

    // each method
    modal.each(function(element) {
       var target = element.getAttribute('data-rv-vanilla-modal');
       var targetElement = document.querySelector(target);
       var closeBtn = targetElement.querySelector(modal.settings.closeSelector);

       // close click listerner
       closeBtn.addEventListener('click', function(event) {
        event.preventDefault();
        modal.close(targetElement);
       });

       closeBtn.addEventListener('OK', function(event) {
        //event.preventDefault();
        alert("You clicked OK");
       });

       // open click listerner
       element.addEventListener('click', function(event) {
        event.preventDefault();
        modal.open(targetElement);
       });
    });
}, false);
</script>
</html>