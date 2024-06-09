// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function responseAdded(id) {
    let element = document.getElementById(id);
    setTimeout(() => {
        element.style = "visibility: hidden; opacitity: 0;";
    }, 10000);
}