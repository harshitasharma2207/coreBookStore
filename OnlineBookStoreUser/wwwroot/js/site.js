// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<Script language="JavaScript">
    $(document).ready(function() {
        $("#btndiv").click(function () {
            $("#button1").html('Deactivate').toggleClass('btn-success');
        });
    });
</Script>