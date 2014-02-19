
$(document).ready(function() {
    $('#get-password').click(function() {
        $.ajax({
            url: '__replace:serviceUrl.generate',
            success: function(s) {
                $('#Password').val(s);
            },
        });
    });
});
