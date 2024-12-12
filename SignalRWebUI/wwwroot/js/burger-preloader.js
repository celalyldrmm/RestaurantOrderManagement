document.addEventListener("DOMContentLoaded", function () {    
    document.body.classList.add("loaded");
   
    window.addEventListener("load", function () {
        clearTimeout(timeout);
        document.body.classList.add("loaded");
    });
});
