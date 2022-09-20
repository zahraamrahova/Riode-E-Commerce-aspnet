 $.ajaxSetup({
        statucCode: {
            200: function (response) {
                if (response.error == false && response.message.length > 0) {
                    toastr.succcess(response.message, 'Successufully!');
                }

            },
            400: function (response) {
                toastr.error('Invalid Request', 'Error!');

            },
            401: function (response) {
                toastr.warning('Please sign in first', 'Attention!');
            },
            403: function (response) {
                toastr.warning('You are not authorized for this operation', 'Attention!');
            },
            404: function (response) {
                toastr.warning('No information found', 'Attention!');
            },
            500: function (response) {
                toastr.error('Server error', 'Attention!');
            }
        }
    });

