// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    $(document).ready(function(){
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

            $('#frmSubscribe').submit(function(e){
            e.preventDefault();
            if($(e.currentTarget).valid()!=true)
            return;
                const formData = new FormData(e.currentTarget);
                console.log(formData);
            $.ajax({
                url: '../Home/Subscribe',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log(response);
                        if(response.error==false){
                            e.currentTarget.reset();
                            toastr.success(response.message, "Success");
                        }
                        else {
                        toastr.error(response.message, "Error");
                        }
                    },
                error: function (response) {
                    console.log(response);
                        toastr.error("Unknown error happened", "Error");
                    }
            });
        }).validate({
            errorElement: 'span'
        });
     
    });
